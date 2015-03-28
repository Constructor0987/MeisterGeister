using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using MeisterGeister.View.General;
using MeisterGeister.Logic.Einstellung;

namespace MeisterGeister.Model
{
        /// <summary>
        /// The ProcessModel class implements the IEnumerable interface
        /// which allows us to use the object in a foreach loop.
        /// To simulate a long-running process, the enumerator contains
        /// a call to Sleep() in the MoveNext() method.
        /// </summary>
        /// <example>
        ///   var model = new ProcessModel(100);
        ///   foreach (var item  in model)
        ///   {
        ///     Console.WriteLine(item.ToString());
        ///   }
        /// </example>

    public class MusikZeileModel : IEnumerable<int>
    {
        public Audio_Playlist_Titel APlaylistTitel;

        
        /// <value>
        /// Determines how many values are returned when the
        /// object is enumerated (such as in a foreach loop).
        /// </value>
        public int Iterations { get; set; }

        public MusikZeileModel(int iterations)
        {
            Iterations = iterations;
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MusikZeileEnum(Iterations);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new MusikZeileEnum(Iterations);
        }

        #endregion

        #region IEnumerator

        private class MusikZeileEnum : IEnumerator<int>
        {
            private int _iterations;
            private int position = 0;

            public MusikZeileEnum(int iterations)
            {
                _iterations = iterations;
            }

            public bool MoveNext()
            {
                // Simulate a long-running process
                Thread.Sleep(100);
                position++;
                return (position <= _iterations);
            }

            public void Reset()
            {
                position = 0;
            }

            public int Current
            {
                get { return position; }
            }

            object IEnumerator.Current
            {
                get { return position; }
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
        ///// <value>
        ///// Determines how many values are returned when the
        ///// object is enumerated (such as in a foreach loop).
        ///// </value>
        //public int Iterations { get; set; }
        //public Audio_Playlist_Titel APlaylistTitel { get; set; }
        //public List<string> StdPfad {get; set;}

        //public MusikZeileModel(Audio_Playlist_Titel aPlaylistTitel)
        //{
        //    APlaylistTitel = aPlaylistTitel;
        //}

        //#region IEnumerable Members

        //IEnumerator IEnumerable.GetEnumerator() 
        //{
        //    return new ProcessEnum(APlaylistTitel);
        //}

        //#endregion

        //#region IEnumerator

        //private class ProcessEnum : IEnumerator<Audio_Playlist_Titel>
        //{
        //    private Audio_Playlist_Titel _aPlaylistTitel;
        //    private int _iterations;
        //    private int position = 0;
        //    private List<string> _stdPfad { get; set; }

        //    public ProcessEnum(Audio_Playlist_Titel aPlaylistTitel)
        //    {
        //        _aPlaylistTitel = aPlaylistTitel;
        //    }

        //    public bool MoveNext()
        //    {
        //        // Simulate a long-running process

        //        if (!File.Exists(_aPlaylistTitel.Audio_Titel.Pfad + "\\" + _aPlaylistTitel.Audio_Titel.Datei))
        //            {
        //                Audio_Titel titel = setTitelStdPfad(_aPlaylistTitel.Audio_Titel);
        //                if (File.Exists(titel.Pfad + "\\" + titel.Datei))
        //                    Global.ContextAudio.Update<Audio_Titel>(titel);
        //            }

        //        //Thread.Sleep(100);
        //        position++;
        //        return (position <= _iterations);
        //    }

        //    public void Reset()
        //    {
        //        position = 0;
        //    }

        //    public Audio_Playlist_Titel Current
        //    {
        //        get { return _aPlaylistTitel; }
        //    }

        //    object IEnumerator.Current
        //    {
        //        get { return _aPlaylistTitel; }
        //    }

        //    public void Dispose()
        //    {
        //        GC.SuppressFinalize(this);
        //    }



        //    public Audio_Titel setTitelStdPfad(Audio_Titel aTitel)
        //    {
        //        char[] charsToTrim = { '\\' };
        //        //Check Titel -> Pfad vorhanden ansonsten Standard-Pfad hinzufügen
        //        if (//System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
        //            File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
        //        {
        //            foreach (string pfad in _stdPfad)
        //            {
        //                if (pfad == aTitel.Pfad)
        //                    return aTitel;

        //                if (aTitel.Pfad != null && (aTitel.Pfad + "\\" + aTitel.Datei).Contains(pfad))
        //                {
        //                    aTitel.Datei = (aTitel.Pfad.EndsWith("\\") ? aTitel.Pfad + aTitel.Datei : aTitel.Pfad + "\\" + aTitel.Datei).
        //                        Substring(pfad.EndsWith("\\") ? pfad.Length : pfad.Length + 1);
        //                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
        //                    return aTitel;
        //                }
        //            }
        //            // Pfad noch kein Standard-Pfad
        //            if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
        //                Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
        //                "Soll der Pfad mit in die Standard-Pfade integriert werden?" + Environment.NewLine + Environment.NewLine + "Neuer Pfad:     " + aTitel.Pfad))
        //            {
        //                MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
        //                    MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + aTitel.Pfad;
        //                setStdPfad();
        //            }
        //            return aTitel;
        //        }

        //        //Pfad+Titel nicht gefunden -> Check Titel in einem anderen Standard-Pfad
        //        foreach (string pfad in _stdPfad)
        //        {
        //            if (aTitel.Datei == null && aTitel.Pfad != null)
        //            {
        //                aTitel.Datei = aTitel.Pfad;
        //                aTitel.Pfad = "";
        //            }
        //            if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + aTitel.Datei))
        //            {
        //                aTitel.Pfad = pfad.TrimEnd(charsToTrim);
        //                return aTitel;
        //            }

        //            if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + System.IO.Path.GetFileName(aTitel.Datei)))
        //            {
        //                aTitel.Pfad = pfad.TrimEnd(charsToTrim);
        //                aTitel.Datei = System.IO.Path.GetFileName(aTitel.Datei);
        //                return aTitel;
        //            }
        //        }

        //        if (Einstellungen.AudioInAnderemPfadSuchen)
        //        {
        //            //ab hier: kein Std.-Pfad ist gültig -> Check in jedem Std.-Pfad mit Suche incl. Unterverzeichnisse nach dem Dateinamen
        //            string gesuchteDatei = System.IO.Path.GetFileName(_stdPfad[0].TrimEnd(charsToTrim) + "\\" + aTitel.Datei);
        //            foreach (string pfad in _stdPfad)
        //            {
        //                if (pfad != "C:\\" && Directory.Exists(pfad))
        //                {
        //                    string[] pfad_datei = Directory.GetFiles(pfad.TrimEnd(charsToTrim), gesuchteDatei, SearchOption.AllDirectories);
        //                    if (pfad_datei.Length > 0)
        //                    {
        //                        aTitel.Pfad = System.IO.Path.GetDirectoryName(pfad_datei[0]);
        //                        aTitel.Datei = System.IO.Path.GetFileName(pfad_datei[0]);
        //                        aTitel = setTitelStdPfad(aTitel);
        //                        return aTitel;
        //                    }
        //                }
        //            }
        //        }

        //        if (aTitel.Pfad == null) aTitel.Pfad = "";
        //        if (aTitel.Pfad == "" || aTitel.Datei == null)
        //        {
        //            string pfadDatei = aTitel.Pfad != null || aTitel.Pfad != "" ? aTitel.Pfad : "";
        //            if (pfadDatei != "" && !pfadDatei.EndsWith("\\"))
        //                pfadDatei = pfadDatei + "\\";
        //            if (aTitel.Datei != null)
        //                pfadDatei = pfadDatei + aTitel.Datei;

        //            aTitel.Pfad = System.IO.Path.GetDirectoryName(pfadDatei);
        //            aTitel.Datei = System.IO.Path.GetFileName(pfadDatei);
        //        }

        //        return aTitel;
        //    }

        //    public void setStdPfad()
        //    {
        //        char[] charsToTrim = { '\\' };
        //        if (_stdPfad.Count > 0) _stdPfad.RemoveRange(0, _stdPfad.Count);
        //        _stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        //    }

        //}

        //#endregion
    
}
