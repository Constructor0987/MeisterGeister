#if NET10_COMPAT_TESTS
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class AudioUpdateAllTests
    {
        [Fact]
        public void FindsDirectAndNestedFilesAndCountsRepeatedTitlesOnce()
        {
            string root = Path.Combine(Path.GetTempPath(), "MeisterGeister-Audio-" + Guid.NewGuid());
            Directory.CreateDirectory(Path.Combine(root, "nested"));
            try
            {
                File.WriteAllText(Path.Combine(root, "direct.mp3"), "");
                File.WriteAllText(Path.Combine(root, "nested", "nested.mp3"), "");
                var directId = Guid.NewGuid();
                var entries = new[]
                {
                    new AudioUpdateAllEntry { Id = directId, Playlist = "A", Path = "missing", File = "direct.mp3" },
                    new AudioUpdateAllEntry { Id = directId, Playlist = "B", Path = "missing", File = "direct.mp3" },
                    new AudioUpdateAllEntry { Id = Guid.NewGuid(), Playlist = "A", Path = "missing", File = "nested.mp3" },
                    new AudioUpdateAllEntry { Id = Guid.NewGuid(), Playlist = "A", Path = "missing", File = "absent.mp3" }
                };
                var changes = new List<AudioUpdateAllEntry>();
                var progress = new List<int>();
                var searchCounts = new List<int>();
                var result = new AudioUpdateAllResult();
                new AudioUpdateAllWorker(new[] { root }, true).Run(entries, result, (batch, text, value) =>
                {
                    changes.AddRange(batch);
                    progress.Add(value);
                    if (text.StartsWith("Unterverzeichnisse")) searchCounts.Add(result.Checked);
                }, CancellationToken.None);

                Assert.Equal(4, result.Checked);
                Assert.Equal(2, result.Changed);
                Assert.Equal(1, result.NotFound);
                Assert.Equal(2, changes.Count);
                Assert.Equal("direct.mp3", changes[0].File);
                Assert.Equal(Path.Combine("nested", "nested.mp3"), changes[1].File);
                Assert.All(changes, change => Assert.Equal(root, change.Path));
                Assert.Equal(100, progress[progress.Count - 1]);
                Assert.Equal(new[] { 2, 3 }, searchCounts);
            }
            finally { Directory.Delete(root, true); }
        }

        [Fact]
        public void CancellationDuringSearchFlushesCompletedChanges()
        {
            string root = Path.Combine(Path.GetTempPath(), "MeisterGeister-Audio-" + Guid.NewGuid());
            Directory.CreateDirectory(root);
            try
            {
                File.WriteAllText(Path.Combine(root, "direct.mp3"), "");
                var entries = new[]
                {
                    new AudioUpdateAllEntry { Id = Guid.NewGuid(), Playlist = "A", Path = "missing", File = "direct.mp3" },
                    new AudioUpdateAllEntry { Id = Guid.NewGuid(), Playlist = "A", Path = "missing", File = "absent.mp3" }
                };
                using (var cancellation = new CancellationTokenSource())
                {
                    var changes = new List<AudioUpdateAllEntry>();
                    var result = new AudioUpdateAllResult();
                    Assert.Throws<OperationCanceledException>(() => new AudioUpdateAllWorker(new[] { root }, true).Run(entries, result, (batch, text, value) =>
                    {
                        changes.AddRange(batch);
                        if (text.StartsWith("Unterverzeichnisse")) cancellation.Cancel();
                    }, cancellation.Token));
                    Assert.Equal(1, result.Checked);
                    Assert.Single(changes);
                    Assert.Equal("direct.mp3", changes[0].File);
                }
            }
            finally { Directory.Delete(root, true); }
        }

        [Fact]
        public void DoesNotSearchSubdirectoriesWhenDisabled()
        {
            string root = Path.Combine(Path.GetTempPath(), "MeisterGeister-Audio-" + Guid.NewGuid());
            Directory.CreateDirectory(Path.Combine(root, "nested"));
            try
            {
                File.WriteAllText(Path.Combine(root, "nested", "song.mp3"), "");
                var result = new AudioUpdateAllResult();
                new AudioUpdateAllWorker(new[] { root }, false).Run(new[] { new AudioUpdateAllEntry { Id = Guid.NewGuid(), Path = "missing", File = "song.mp3" } }, result, (batch, text, value) => Assert.Empty(batch), CancellationToken.None);
                Assert.Equal(1, result.Checked);
                Assert.Equal(1, result.NotFound);
                Assert.Equal(0, result.Changed);
            }
            finally { Directory.Delete(root, true); }
        }
    }
}
#endif
