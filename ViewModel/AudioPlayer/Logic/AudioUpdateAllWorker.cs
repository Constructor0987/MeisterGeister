using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public sealed class AudioUpdateAllEntry
    {
        public Guid Id { get; set; }
        public string Playlist { get; set; }
        public string Path { get; set; }
        public string File { get; set; }
    }

    public sealed class AudioUpdateAllResult
    {
        public int Checked { get; internal set; }
        public int Changed { get; internal set; }
        public int NotFound { get; internal set; }
    }

    public sealed class AudioUpdateAllWorker
    {
        private readonly string[] roots;
        private readonly bool searchSubdirectories;
        private readonly Dictionary<string, SearchIndex> indexes = new Dictionary<string, SearchIndex>(StringComparer.OrdinalIgnoreCase);

        public AudioUpdateAllWorker(IEnumerable<string> roots, bool searchSubdirectories)
        {
            this.roots = roots.ToArray();
            this.searchSubdirectories = searchSubdirectories;
        }

        public void Run(IReadOnlyList<AudioUpdateAllEntry> entries, AudioUpdateAllResult result,
            Action<IReadOnlyList<AudioUpdateAllEntry>, string, int> report, CancellationToken cancellation)
        {
            var resolved = new Dictionary<Guid, AudioUpdateAllEntry>();
            var pending = new List<AudioUpdateAllEntry>();
            var clock = Stopwatch.StartNew();
            string status = "Playlisten werden überprüft...";
            Action<bool> flush = force =>
            {
                if (!force && pending.Count < 50 && clock.ElapsedMilliseconds < 150) return;
                var changes = pending.ToArray();
                pending.Clear();
                report(changes, status, entries.Count == 0 ? 100 : (int)(100L * result.Checked / entries.Count));
                clock.Restart();
            };

            try
            {
                foreach (var original in entries)
                {
                    cancellation.ThrowIfCancellationRequested();
                    AudioUpdateAllEntry entry;
                    if (!resolved.TryGetValue(original.Id, out entry)) entry = original;
                    status = "Playlist '" + original.Playlist + "' wird überprüft";
                    if (!File.Exists(entry.Path + "\\" + entry.File))
                    {
                        string file = entry.File ?? entry.Path;
                        string foundRoot = null;
                        string foundFile = null;
                        foreach (string root in roots)
                        {
                            cancellation.ThrowIfCancellationRequested();
                            string candidate = root.TrimEnd('\\') + "\\" + file;
                            string nameCandidate = root.TrimEnd('\\') + "\\" + Path.GetFileName(file);
                            if (File.Exists(candidate)) { foundRoot = root; foundFile = file; break; }
                            if (File.Exists(nameCandidate)) { foundRoot = root; foundFile = Path.GetFileName(file); break; }
                        }
                        if (foundRoot == null && searchSubdirectories && !string.IsNullOrEmpty(file))
                        {
                            foreach (string root in roots)
                            {
                                cancellation.ThrowIfCancellationRequested();
                                if (string.Equals(root, "C:\\", StringComparison.OrdinalIgnoreCase) || !Directory.Exists(root)) continue;
                                status = "Unterverzeichnisse werden durchsucht: " + root;
                                flush(true);
                                SearchIndex index;
                                if (!indexes.TryGetValue(root, out index))
                                {
                                    index = new SearchIndex(root, cancellation, () => flush(false));
                                    indexes.Add(root, index);
                                }
                                string path;
                                if (index.TryFind(Path.GetFileName(file), out path))
                                {
                                    foundRoot = root;
                                    foundFile = Path.GetFullPath(path).Substring(Path.GetFullPath(root).TrimEnd('\\').Length + 1);
                                    break;
                                }
                            }
                        }
                        if (foundRoot != null)
                        {
                            entry = new AudioUpdateAllEntry { Id = entry.Id, Path = foundRoot.TrimEnd('\\'), File = foundFile };
                            resolved[entry.Id] = entry;
                            pending.Add(entry);
                            result.Changed++;
                        }
                        else result.NotFound++;
                    }
                    result.Checked++;
                    status = "Playlist '" + original.Playlist + "' wird überprüft";
                    flush(false);
                }
            }
            finally
            {
                foreach (var index in indexes.Values) index.Dispose();
                indexes.Clear();
                flush(true);
            }
        }

        private sealed class SearchIndex : IDisposable
        {
            private readonly Dictionary<string, string> files = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            private readonly IEnumerator<string> enumerator;
            private bool exhausted;

            public SearchIndex(string root, CancellationToken cancellation, Action report)
            {
                enumerator = EnumerateFiles(root, cancellation, report).GetEnumerator();
            }

            public bool TryFind(string name, out string path)
            {
                if (files.TryGetValue(name, out path)) return true;
                while (!exhausted && enumerator.MoveNext())
                {
                    string candidate = enumerator.Current;
                    string candidateName = Path.GetFileName(candidate);
                    if (!files.ContainsKey(candidateName)) files.Add(candidateName, candidate);
                    if (string.Equals(name, candidateName, StringComparison.OrdinalIgnoreCase))
                    {
                        path = candidate;
                        return true;
                    }
                }
                exhausted = true;
                path = null;
                return false;
            }

            public void Dispose() { enumerator.Dispose(); }
        }

        private static IEnumerable<string> EnumerateFiles(string root, CancellationToken cancellation, Action report)
        {
            var directories = new Stack<string>();
            directories.Push(root);
            while (directories.Count > 0)
            {
                cancellation.ThrowIfCancellationRequested();
                string directory = directories.Pop();
                foreach (string file in Directory.EnumerateFiles(directory))
                {
                    cancellation.ThrowIfCancellationRequested();
                    report();
                    yield return file;
                }
                foreach (string child in Directory.EnumerateDirectories(directory))
                {
                    cancellation.ThrowIfCancellationRequested();
                    if ((File.GetAttributes(child) & FileAttributes.ReparsePoint) == 0) directories.Push(child);
                    report();
                }
            }
        }
    }
}
