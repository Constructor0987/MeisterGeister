using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using MeisterGeister.Model.Extensions;
//eigene
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;
//using MeisterGeister.Model;

namespace MeisterGeister.View.AudioPlayer
{

    /// <summary>
    /// Interaktionslogik für AudioZeile.xaml
    /// </summary>
    public partial class AudioZeile : UserControl
    {
        public bool HintergrundMusik = false;



        public AudioZeile()
        {
            InitializeComponent();
        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) - 1 >= 0)
                sldPlaySpeed.Value = sldPlaySpeed.Ticks[sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) - 1];
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            if (sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) <= sldPlaySpeed.Ticks.Count - 2)
                sldPlaySpeed.Value = sldPlaySpeed.Ticks[sldPlaySpeed.Ticks.IndexOf(sldPlaySpeed.Value) + 1];
        }

        private void chkValidChange(object sender, TextChangedEventArgs e)
        {
            var oldIndex = ((TextBox)sender).CaretIndex;
            var help1 = Regex.Replace((((TextBox)sender).Text), @"\D", "");
            help1 = help1.Replace(" ", "");
            ((TextBox)sender).Text = help1;
            ((TextBox)sender).CaretIndex = help1.Length > oldIndex ? oldIndex : help1.Length;
        }

        private void tboxPauseMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            chkValidChange(sender, e);
            int wert = Convert.ToInt32(((TextBox)sender).Text);
            ((TextBox)sender).ToolTip = (wert < 1000) ? wert + " ms" : (wert < 60000) ? wert / 1000 + " sek." : wert / 60000 + " min.";
        }
               
        //protected override void OnMouseEnter(MouseEventArgs e)
        //{
        //    _isMouseOver = true;
        //    base.OnMouseEnter(e);
        //}

        //protected override void OnMouseLeave(MouseEventArgs e)
        //{
        //    _isMouseOver = false;
        //    base.OnMouseLeave(e);
        //}

        //private bool _isMouseOver = false;
        //public bool IsMouseOver
        //{
        //    get { return _isMouseOver; }
        //}

        //private void btnGewichtung_Click(object sender, RoutedEventArgs e)
        //{
        //    int gewichtung = Convert.ToInt32(((Button)sender).Tag);
        //    gewichtung++;
        //    if (gewichtung == 6) gewichtung = 0;
        //    foreach (var img in grdbtnGewichtung.Children)
        //        if (img.GetType() == typeof(Image))
        //            ((Image)img).Visibility = ((Convert.ToInt32(((Image)img).Tag)) <= gewichtung && gewichtung > 0)? Visibility.Visible : Visibility.Hidden;
        //    ((Button)sender).Tag = gewichtung;
        //}

        //private void _btnPauseMaxPlus_Click(object sender, RoutedEventArgs e)
        //{
        //((Button)sender).Tag = (Convert.ToInt32(tboxPauseMax.Text) >= 10000) ? 5000 :
        //                       (Convert.ToInt32(tboxPauseMax.Text) >= 2000) ? 1000 : 200;

        //int sollWert =  Convert.ToInt32(tboxPauseMax.Text) + Convert.ToInt32(((Button)sender).Tag);
        //int max = Convert.ToInt32(sldKlangPause.Maximum);
        //int pauseMax_wert = (sollWert <= max)? sollWert < 0 ? 0 : sollWert : max;

        //tboxPauseMax.Text = Convert.ToString(pauseMax_wert);

        //if (pauseMax_wert < Convert.ToInt32(tboxPauseMin.Text))
        //    tboxPauseMin.Text = tboxPauseMax.Text;

        //((Button)sender).Tag = (pauseMax_wert >= 10000)? 5000: 
        //                       (pauseMax_wert >= 2000)? 1000: 200;
        //}

        //private void _btnPauseMaxMinus_Click(object sender, RoutedEventArgs e)
        //{
        //((Button)sender).Tag = (Convert.ToInt32(tboxPauseMax.Text) >= 10000) ? -5000 :
        //                       (Convert.ToInt32(tboxPauseMax.Text) >= 2000) ? -1000 : -200;

        //int sollWert =  Convert.ToInt32(tboxPauseMax.Text) + Convert.ToInt32(((Button)sender).Tag);
        //int pauseMax_wert = (sollWert < 0) ? 0 : sollWert;

        //tboxPauseMax.Text = Convert.ToString(pauseMax_wert);

        //if (pauseMax_wert < Convert.ToInt32(tboxPauseMin.Text))
        //    tboxPauseMin.Text = tboxPauseMax.Text;

        //((Button)sender).Tag = (pauseMax_wert >= 10000) ? -5000 :
        //                       (pauseMax_wert >= 2000) ? -1000 : -200;
        //}

        //private void _btnPauseMinPlus_Click(object sender, RoutedEventArgs e)
        //{
        //((Button)sender).Tag = (Convert.ToInt32(tboxPauseMin.Text) >= 10000) ? 5000 :
        //                       (Convert.ToInt32(tboxPauseMin.Text) >= 2000) ? 1000 : 200;

        //int sollWert = Convert.ToInt32(tboxPauseMin.Text) + Convert.ToInt32(((Button)sender).Tag);
        //int max = Convert.ToInt32(sldKlangPause.Maximum);
        //int pauseMin_wert = (sollWert >= Convert.ToInt32(sldKlangPause.Minimum)) ? sollWert > max ? max : sollWert : max;

        //tboxPauseMin.Text = Convert.ToString(pauseMin_wert);

        //if (pauseMin_wert > Convert.ToInt32(tboxPauseMax.Text))
        //    tboxPauseMax.Text = tboxPauseMin.Text;

        //((Button)sender).Tag = (pauseMin_wert >= 10000) ? 5000 :
        //                       (pauseMin_wert >= 2000) ? 1000 : 200;
        //}

        //private void _btnPauseMinMinus_Click(object sender, RoutedEventArgs e)
        //{
        //((Button)sender).Tag = (Convert.ToInt32(tboxPauseMin.Text) >= 10000) ? -5000 :
        //                       (Convert.ToInt32(tboxPauseMin.Text) >= 2000) ? -1000 : -200;

        //int sollWert = Convert.ToInt32(tboxPauseMin.Text) + Convert.ToInt32(((Button)sender).Tag);
        //int pauseMin_wert = (sollWert < 0) ? 0 : sollWert;
        //tboxPauseMin.Text = Convert.ToString(pauseMin_wert);

        //if (pauseMin_wert > Convert.ToInt32(tboxPauseMax.Text))
        //    tboxPauseMax.Text = tboxPauseMin.Text;

        //((Button)sender).Tag = (pauseMin_wert >= 10000) ? -5000 :
        //                       (pauseMin_wert >= 2000) ? -1000 : -200;
        //}

        
        //private void lbiEditorRow_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    spReihenfolge.Visibility = Visibility.Visible;
        //    btnZeileLöschen.Visibility = Visibility.Visible;
        //}

        //private void lbiEditorRow_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    spReihenfolge.Visibility = Visibility.Hidden;
        //    btnZeileLöschen.Visibility = Visibility.Hidden;            
        //}

        //private void lblKlangZeileCMenuÄndern_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    int i = Convert.ToInt16(((Label)sender).Tag);
        //    try
        //    {
        //        MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile =
        //            //(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile)((ContextMenu)((Label)sender).Parent).Tag;

        //        switch (i)
        //        {
        //            case 1:                                                     // Geräusch/Musiktitel löschen
        //                imgTrash0_0_MouseUp(kZeile.audioZeile.imgTrash, e);
        //                break;
        //            case 2:                                                     // Datei-Bezug ändern                         
        //                kZeile.aPlaylistTitel.Audio_Titel = VM.setTitelStdPfad(kZeile.aPlaylistTitel.Audio_Titel);
        //                Global.ContextAudio.Update<Audio_Titel>(kZeile.aPlaylistTitel.Audio_Titel);

        //                FileInfo fi = new FileInfo(kZeile.aPlaylistTitel.Audio_Titel.Pfad + @"\" + kZeile.aPlaylistTitel.Audio_Titel.Datei);
        //                string aktDir = Environment.CurrentDirectory;
        //                Environment.CurrentDirectory = fi.DirectoryName;
        //                string datei = ViewHelper.ChooseFile("Datei auswählen", fi.Name, false, VM.validExt);
        //                //fi.Extension.Substring(1)+",.mp3");
        //                Environment.CurrentDirectory = aktDir;

        //                if (datei != null)
        //                {
        //                    kZeile.aPlaylistTitel.Audio_Titel.Pfad = System.IO.Path.GetDirectoryName(datei);
        //                    kZeile.aPlaylistTitel.Audio_Titel.Datei = System.IO.Path.GetFileName(datei);
        //                    kZeile.audioZeile.chkTitel.Content = System.IO.Path.GetFileNameWithoutExtension(datei);
        //                    kZeile.audioZeile.chkTitel.ToolTip = datei;
        //                    Global.ContextAudio.Update<Audio_Titel>(kZeile.aPlaylistTitel.Audio_Titel);
        //                }
        //                break;
        //            case 4:                                                 // Titel duplizieren    
        //                AudioZeileItemAblegen(kZeile.audioZeile, AktKlangPlaylist, null, sender);
        //                break;
        //            case 5:                                                 // Dateipfad öffnen
        //                if (Directory.Exists(kZeile.aPlaylistTitel.Audio_Titel.Pfad) &&
        //                    File.Exists(kZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + kZeile.aPlaylistTitel.Audio_Titel.Datei))
        //                {
        //                    FileInfo fi2 = new FileInfo(kZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + kZeile.aPlaylistTitel.Audio_Titel.Datei);
        //                    System.Diagnostics.Process.Start("explorer.exe", "/e,/select," + fi2.DirectoryName + "\\" + @"""" + fi2.Name + @"""");
        //                }
        //                else
        //                    ViewHelper.Popup("Die Datei bzw. das Verzeichnis konnte nicht gefunden werden");
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewHelper.ShowError("Fehler" + Environment.NewLine + "Beim Ausführen der Kontextfunktion " + i + " ist ein Fehler aufgetreten", ex);
        //    }
        //}
    }
}
