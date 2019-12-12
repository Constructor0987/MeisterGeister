using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MeisterGeister.Model;
using System.Windows.Data;
//Eigene usings
using MeisterGeister.ViewModel.Basar.Logic;
using Base = MeisterGeister.ViewModel.Base;
using Model = MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;
using MeisterGeister.Logic.Umrechner;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model.Extensions;
using System.Windows.Threading;
using System.Windows.Media;
using MeisterGeister.View.AudioPlayer;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MeisterGeister.View.General;
using System.Windows;
using System.Windows.Input;
using System.IO;
using MeisterGeister.Logic.Einstellung;
using System.Globalization;
using System.Xml;
using System.Threading;
//using NAudio.Wave;
using MeisterGeister.ViewModel.Base;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
using System.Windows.Interop;

namespace MeisterGeister.ViewModel.AudioPlayer
{
    public static class FocusExtension
    {
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool?), typeof(FocusExtension), new FrameworkPropertyMetadata(IsFocusedChanged));

        public static bool? GetIsFocused(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return (bool?)element.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject element, bool? value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element.SetValue(IsFocusedProperty, value);
        }

        private static void IsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = (FrameworkElement)d;
            if (e.OldValue == null)
            {
                fe.GotFocus += FrameworkElement_GotFocus;
                fe.LostFocus += FrameworkElement_LostFocus;
            }
        }

        private static void fe_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var fe = (FrameworkElement)sender;
            if (fe.IsVisible && (bool)((FrameworkElement)sender).GetValue(IsFocusedProperty))
            {
                fe.IsVisibleChanged -= fe_IsVisibleChanged;
                fe.Focus();
            }
        }

        private static void FrameworkElement_GotFocus(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(IsFocusedProperty, true);
        }

        private static void FrameworkElement_LostFocus(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(IsFocusedProperty, false);
        }
    }
    
    #region //---- Converters ----
    
    public class SliderRangeMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rootActualWidth = values[0] is double? (double)values[0]: 0;
            var LowerSliderMaximum = values[1] is double? (double)values[1]: 0;
            var LowerSliderValue = values[2] is double? (double)values[2]: 0;
            var UpperSliderValue = values[3] is double ? (double)values[3] : 0;

            return new Thickness(
                (rootActualWidth - 17) / (LowerSliderMaximum / LowerSliderValue) + 8,  
                0,
                rootActualWidth - 17 - (rootActualWidth - 17) / (LowerSliderMaximum / UpperSliderValue) + 8, 
                0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertedBoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
    
    public class MillisecondToMinuteSecondsTextConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new MillisecondToMinuteSecondsTextConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = (int)value >= 60000 ? (int)value - ((int)value / 60000) * 60000 : 0; 
            string s = ((int)value < 1000 ? value + "ms" : (int)value < 60000 ?
                    Math.Round((double)( System.Convert.ToDecimal((int)value) / 1000), 2).ToString() + "sek" :
                    (int)value / 60000 + "min" +
                        (i < 1000 ? i + "ms" : i < 60000 ?
                        Math.Round((double)( System.Convert.ToDecimal(i) / 1000), 2).ToString() + "sek":""));
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToTimespanHHMMSSConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new DoubleToTimespanHHMMSSConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (value is double && (double)value != 0)? TimeSpan.FromMilliseconds((double)value): TimeSpan.Zero;
            return (ts.TotalHours >= 1) ? ts.ToString(@"hh\:mm\:ss") : ts.ToString(@"mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if (((value is bool) && (bool)value == false) || value == null)
                {
                    return false;
                }
            }
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }

    public class MultiBooleanAndConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if (((value is bool) && (bool)value == false) || value == null)
                {
                    return false;
                }
            }
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }

    public class MultiBooleanORConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value)
                {
                    return true;
                }
            }
            return false;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }

    public class MultiBooleanToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                                Type targetType,
                                object parameter,
                                System.Globalization.CultureInfo culture)
        {
            bool visible = true;
            foreach (object value in values)
                if (value is bool)
                    visible = visible && (bool)value;

            if (visible)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class IsVisibleToBooleanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsVisibleToBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Visible) ? true: false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class MultiBooleanODERToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                                Type targetType,
                                object parameter,
                                System.Globalization.CultureInfo culture)
        {
            bool visible = false;
            foreach (object value in values)
                if (value is bool)
                {
                    visible = (bool)value;
                    if (visible) break;
                }

            if (visible)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsEqualOrGreaterThanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = ((value == null) ? 0 : System.Convert.ToInt32(value));
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue >= compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsEqualOrLowerThanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrLowerThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = ((value == null) ? 0 : System.Convert.ToInt32(value));
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue <= compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanInverseToVisibilityConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BooleanInverseToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool)value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanAndNotReihenfolgeConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BooleanAndNotReihenfolgeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanInverseToVisibilityHiddenConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BooleanInverseToVisibilityHiddenConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool)value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringNullOrEmptyToVisibilityConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string)
                ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    
    public class IsNullOrEmptyToBoolConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null? false : true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    

    #endregion

    public class AudioPlayerViewModel : Base.ToolViewModelBase
    {
        #region //---- AudioPlayer Close EVENT ----

        
        public void OnAudioTabClose(object sender, EventArgs e)
        {
            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
            {
                List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);

                if (KlangZeilenLaufend.Count != 0)
                    for (int durchlauf = 0; durchlauf < KlangZeilenLaufend.Count; durchlauf++)
                    {
                        if (KlangZeilenLaufend[durchlauf].aData != null)
                        {
                            KlangZeilenLaufend[durchlauf].aData.Stop();
                            Player_Ended(KlangZeilenLaufend[durchlauf].aData, null);
                            KlangZeilenLaufend[durchlauf].aData.Close();
                        }
                    }
            }
            if (BGPlayer != null)
            {
                for (int i = 0; i < BGPlayer.BG.Count; i++)
                    if (BGPlayer.BG[i].aData != null && BGPlayer.BG[i].aData.audioStream != 0)
                    {
                        BGPlayer.BG[i].aData.Stop();
                        BGPlayer.BG[i].aData.Close();
                    }
            }

            foreach (DispatcherTimer dispTmr in lstKlangPlayEndetimer)
                if (dispTmr != null) dispTmr.Stop();

            AlleKlangSongsAus(null, true, true, false, true);
                        
            _GrpObjecte.Clear();
            lstKlangPlayEndetimer.Clear();
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public AudioPlayerViewModel()
        {
            BGPlayer = new MusikView();
            BGPlayer.BG.Add(new Musik());
            setStdPfad();
            StdPadAufC = stdPfad.Contains("c:\\") || stdPfad.Contains("C:\\");
            Init();
            this.RequestClose += OnAudioTabClose;  
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            workerGetLength.WorkerReportsProgress = true;
            workerGetLength.WorkerSupportsCancellation = true;
            workerGetLength.DoWork += new DoWorkEventHandler(workerGetLength_DoWork);
            workerGetLength.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerGetLength_RunWorkerCompleted);
            
            fadingTime = MeisterGeister.Logic.Einstellung.Einstellungen.Fading;
            AktualisiereHotKeys();

            UpdateHotkeyUsed();
            UpdateAlleListen();
            Refresh();

            LbEditorMitGeräusche = true;
            LbEditorMitMusik = true;
            
            _timerFadingIn.Tick += new EventHandler(_timerFadingIn_Tick);
            _timerFadingOut.Tick += new EventHandler(_timerFadingOut_Tick);
            _timerFadingOutGeräusche.Tick += new EventHandler(_timerFadingOutGeräusche_Tick);
            _timerFadingInGeräusche.Tick += new EventHandler(_timerFadingInGeräusche_Tick);

            KlangProgBarTimer.Tick += new EventHandler(KlangProgBarTimer_Tick);
            KlangProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            KlangProgBarTimer.Tag = 0;

            MusikProgBarTimer.Tick += new EventHandler(MusikProgBarTimer_Tick);
            MusikProgBarTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            VisualGrpObj();
            if (AktKlangPlaylist == null)
            {
                if (EditorListBoxItemListe.Count == 0)
                {
                    NeueKlangPlaylistInDB(NeuerPlaylistName);
                    EditorListBoxItemListe = lbiPlaylistListNeuErstellen();
                    FilterEditorPlaylistListe();
                }

                _GrpObjecte.FirstOrDefault(t => t.visuell).aPlaylist = FilteredEditorListBoxItemListe[0].APlaylist;
                
                AktKlangPlaylist = _GrpObjecte.FirstOrDefault(t => t.visuell).aPlaylist;
            }
            SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
            firstshot = false;        
        }

        public void Refresh()
        {
            OnChanged("AktKlangPlaylist");
            OnChanged("AktKlangTheme");
        }
               
        #endregion

        #region //---- Klassen ----
        public class AudioData
        {
            public int audioStream = 0;
            public int tempostream = 0;

            public bool Stop()
            {
                if (tempostream != 0)
                    Bass.BASS_ChannelStop(audioStream);
                if (audioStream != 0)
                    return Bass.BASS_ChannelStop(audioStream);
                else
                    return false;
            }
            
            public bool isStopped()
            {
                return (tempostream == 0) ?
                    Bass.BASS_ChannelIsActive(audioStream) == BASSActive.BASS_ACTIVE_STOPPED:
                    Bass.BASS_ChannelIsActive(tempostream) == BASSActive.BASS_ACTIVE_STOPPED;
            }
            public bool isPlaying()
            {
                return (tempostream == 0) ?
                    Bass.BASS_ChannelIsActive(audioStream) == BASSActive.BASS_ACTIVE_PLAYING :
                    Bass.BASS_ChannelIsActive(tempostream) == BASSActive.BASS_ACTIVE_PLAYING;
            }
            public bool isPaused()
            {
                return Bass.BASS_ChannelIsActive(audioStream) == BASSActive.BASS_ACTIVE_PAUSED;
            }

            private SYNCPROC _mySync;
            public void Play()
            {
                if (_mySync == null)
                    _mySync = new SYNCPROC(EndSync);
                Bass.BASS_ChannelSetSync(audioStream, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME, 0, _mySync, IntPtr.Zero);
                     
                if (tempostream == 0)
                    Bass.BASS_ChannelPlay(audioStream, false);                
                else
                    Bass.BASS_ChannelPlay(tempostream, false);
            }
            private void EndSync(int handle, int channel, int data, IntPtr user)
            {
           //     Stop();
           //     Close();
            }

            public bool Pause()
            {
                return (tempostream == 0)? 
                    Bass.BASS_ChannelPause(audioStream): 
                    Bass.BASS_ChannelPause(tempostream);
            }
            public bool Close()
            {
                if (tempostream != 0)
                {
                    if (!Bass.BASS_StreamFree(tempostream))
                    {
                        BASSError berr = Bass.BASS_ErrorGetCode();
                        if (berr != 0)
                        { }
                    }
                    tempostream = 0;
                }
                if (!Bass.BASS_StreamFree(audioStream))
                {
                    BASSError berr = Bass.BASS_ErrorGetCode();
                    if (berr != 0)
                    { }
                    audioStream = 0;
                    return false;
                }
                else
                {
                    audioStream = 0;
                    return true;
                }
            }

            public void setEcho(int Echo)
            {
                if (Echo == 0) return;
                BASS_BFX_ECHO4 echo = new BASS_BFX_ECHO4();
                if (Echo == 1) echo.Preset_SmallEcho();
                if (Echo == 2) echo.Preset_LongEcho();
                int fxEchoHandle = Bass.BASS_ChannelSetFX(tempostream, BASSFXType.BASS_FX_BFX_ECHO4, 1);
                Bass.BASS_FXSetParameters(fxEchoHandle, echo);
            }
                
            public void setPitch(double pitch)
            {
                Bass.BASS_ChannelSetAttribute(tempostream, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, Convert.ToInt32(Math.Round(pitch))); 
            }
               
            public void setSpeed(double speed)
            {                
                Bass.BASS_ChannelSetAttribute(tempostream, BASSAttribute.BASS_ATTRIB_TEMPO, Convert.ToInt32(Math.Round(speed))); // increase the tempo/speed by speed in%
                                                                                                 //  Bass.BASS_ChannelSetAttribute(tempostream, BASSAttribute.BASS_ATTRIB_TEMPO, 10); // increase the tempo/speed by 10%

                //    Single s = 255;// Convert.ToSingle(speed * 10);
                //    object par;

                //   BASS_DX8_COMPRESSOR comp = new BASS_DX8_COMPRESSOR();
                //   comp.Preset_Hard();

                //    BASS_DX8_DISTORTION dist = new BASS_DX8_DISTORTION();
                //    dist.fEdge = 80;
                //     dist.fPostEQCenterFrequency = 7000;
                //     dist.fPostEQBandwidth = 7000;

                //BASS_BFX_ECHO4 e4 = new BASS_BFX_ECHO4();
                //e4.Preset_LongEcho(); 
                //BASS_DX8_ECHO echo = new BASS_DX8_ECHO();

                //BASS_DX8_PARAMEQ eq = new BASS_DX8_PARAMEQ();
                //int[] _fxEQ = {0, 0, 0};
                //_fxEQ[0] = Bass.BASS_ChannelSetFX(audioStream, BASSFXType.BASS_FX_DX8_PARAMEQ, 0);
                //_fxEQ[1] = Bass.BASS_ChannelSetFX(audioStream, BASSFXType.BASS_FX_DX8_PARAMEQ, 0);
                //_fxEQ[2] = Bass.BASS_ChannelSetFX(audioStream, BASSFXType.BASS_FX_DX8_PARAMEQ, 0);
                //eq.fBandwidth = 18f;
                //eq.fCenter = 100f;
                //eq.fGain = 0f;
                //Bass.BASS_FXSetParameters(_fxEQ[0], eq);
                //eq.fCenter = 1000f;
                //Bass.BASS_FXSetParameters(_fxEQ[1], eq);
                //eq.fCenter = 8000f;
                //Bass.BASS_FXSetParameters(_fxEQ[2], eq);

                //  if (Bass.BASS_FXGetParameters(_fxEQ[0], eq))
                //  {
                //    eq.fGain = 100;// .fGain = gain;

                //      if (!Bass.BASS_FXSetParameters(_fxEQ[0], eq)) 
                //        s = 20;
                //    else
                //        s = 0;
                //  }


                //int fxHandle = Bass.BASS_ChannelSetFX(audioStream, BASSFXType.BASS_FX_BFX_PEAKEQ, 0);

                //  comp.Preset_Hard();
                //comp.Preset_Soft();

                //  int fxHandle = Bass.BASS_ChannelSetFX(audioStream, BASSFXType.BASS_FX_DX8_ECHO, 0);
                //    echo.Preset_Long();

                //if (!Bass.BASS_FXSetParameters(fxHandle, echo))
                //if (!Bass.BASS_FXSetParameters(fxHandle, comp))
                //if (!Bass.BASS_FXSetParameters(fxHandle, dist))
                //    s = 20;
                //else
                //    s = 0;
                //    int tempostream = fx .BASS_FX_TempoCreate(decoder, BASS_FX_FREESOURCE); // create a tempo stream from it
                //    BASS_ChannelSetAttribute(tempostream, BASS_ATTRIB_TEMPO, 10); // increase the tempo/speed by 10%

                //if (Bass.BASS_ChannelSetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_TEMPO, 10f))
                //    //Bass.BASS_ChannelSetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_FREQ, 200000))
                //    s = 0f;
                //else
                //    s = 1f;

                //if (Bass.BASS_ChannelSetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_MUSIC_SPEED, 0.5f))
                //    s = 0f;
                //else
                //    s = 1f;
            }

            public double getSpeed()
            {
                float s = 0f;
                if (Bass.BASS_ChannelGetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_MUSIC_SPEED, ref s))
                    return Convert.ToDouble(s);
                else
                    return 0;
            }

            public float lastVolume;
            
            public bool muted;
            /// <summary>
            /// Setzt den Song auf MUTE oder setzt das MUTING des Songs zurück
            /// </summary>
            /// <param name="mute"></param>
            /// <returns></returns>
            public bool mute(bool mute)
            {
                if (mute == muted)
                {
                    return (muted)? setVolume(0.0f): true;
                }

                bool rtn; // returnvalue

                if (mute) // mute
                {
                    lastVolume = getVolume(); // save current volume
                    rtn = setVolume(0.0f); // set volume to 0.0f (= mute)
                    muted = true; // set mute-state
                }
                else // unmute
                {
                    rtn = setVolume(lastVolume); // restore volume
                    muted = false; // set mute-state
                }

                return rtn; // returnvalue
            }

            /// <summary>
            /// Gibt das aktuelle Volume zurück
            /// </summary>
            /// <returns></returns>
            public float getVolume()
            {
                float vol = 0f;
                return (tempostream == 0) ? 
                    (Bass.BASS_ChannelGetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_VOL, ref vol)) ? vol : 0f:
                    (Bass.BASS_ChannelGetAttribute(tempostream, BASSAttribute.BASS_ATTRIB_VOL, ref vol)) ? vol : 0f;
            }
            /// <summary>
            /// Setzt das Volume des Songs
            /// </summary>
            /// <param name="vol"></param>
            /// <returns></returns>
            public bool setVolume(double vol)
            {
                return (tempostream == 0) ? 
                    Bass.BASS_ChannelSetAttribute(audioStream, BASSAttribute.BASS_ATTRIB_VOL, Convert.ToSingle(vol)):
                    Bass.BASS_ChannelSetAttribute(tempostream, BASSAttribute.BASS_ATTRIB_VOL, Convert.ToSingle(vol));

            }
            /// <summary>
            /// Gibt die Länge des Songs in Millisekunden zurück
            /// </summary>
            /// <returns></returns>
            public double getLength()
            {
                return Bass.BASS_ChannelBytes2Seconds(audioStream, Bass.BASS_ChannelGetLength(audioStream) * 1000);
            }
            /// <summary>
            /// Gibt die aktuelle Position in Millisekunden im Song zurück
            /// </summary>
            /// <returns></returns>
            public double getPosition()
            {
                return Bass.BASS_ChannelBytes2Seconds(audioStream, Bass.BASS_ChannelGetPosition(audioStream)*1000);
            }
            /// <summary>
            /// Setzt die aktuelle Position des Songs
            /// </summary>
            /// <param name="milliSec"></param>
            /// <returns></returns>
            public bool setPosition(double milliSec)
            {
                return Bass.BASS_ChannelSetPosition(audioStream, milliSec/1000);
            }
            /// <summary>
            /// Gibt den Absoluten Dateinamen des Songs zurück
            /// </summary>
            /// <returns></returns>
            public string getFilename()
            {
                return (audioStream != 0) ? Bass.BASS_ChannelGetInfo(audioStream).filename : null;
            }

            public bool setFilename(string file, bool musik)
            {
                if (musik)
                    audioStream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);// BASS_DEFAULT);
                else
                    audioStream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_DECODE);// BASS_DEFAULT);

                //  audioStream = Bass.BASS_MusicLoad(file, 0, 0, BASSFlag.BASS_MUSIC_RAMP | BASSFlag.BASS_MUSIC_PRESCAN | BASSFlag.BASS_STREAM_DECODE, 0);

                //audioStream = Bass.BASS_StreamCreateFile(FALSE, filename, 0, 0, BASSFlag.BASS_STREAM_DECODE); // create a "decoding channel" for a file
                if (!musik && tempostream == 0)
                    tempostream = BassFx.BASS_FX_TempoCreate(audioStream, BASSFlag.BASS_FX_FREESOURCE);
                
                // int tempostream = Bass.BASS_FXSetParameters(audioStream, BassFx.BASS_FX_TempoCreate( BASS_ATTRIB_MUSIC_SPEED);// .BASS_FX_TempoCreate(audioStream, BASS_FX_FREESOURCE); // create a tempo stream from it
                
                //BASS_ChannelSetAttribute(tempostream, BASS_ATTRIB_TEMPO, 10); // increase the tempo/speed by 10%
                //BASS_ChannelPlay(tempostream, FALSE); // start playing


                return (audioStream != 0);
            }

        }
        public class Musik: Base.ViewModelBase
        {
            public bool FadingOutStarted = false;

            private bool _isPaused = false;
            public bool isPaused
            {
                get { return _isPaused;}
                set { Set(ref _isPaused, value); }
            }

            public Audio_Playlist aPlaylist = null;
            public Audio_Titel aTitel = null;

            public AudioData aData = null;

        }
        
        public class group
        {
            public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
            public MusikZeile mZeile = null;
            public double zielProzent;
            public double startProzent;
            public DateTime StartZeit;
            public double _vergangeneZeit;
            /// <summary>
            /// Start- bzw. Ziel-Volume beim Starten des Fadings zum Check des Offsets
            /// </summary>
            public double lookupVol;
            /// <summary>
            /// Falls das Volume während des Fadings geändert wird
            /// </summary>
            public double Offset = 0;  
        }

        public class FadingInGeräusche
        {
            public List<group> gruppenIn = new List<group>();
        }

        public class FadingOutGeräusche
        {
            public List<group> gruppenOut = new List<group>();
            public bool fadingOutSofort = false;
        }

        public class Fading
        {
            public AudioData aData;// = new AudioData();

            public DateTime Start;

            /// <summary>
            /// Start- bzw. Ziel-Volume beim Starten des Fadings zum Check des Offsets
            /// </summary>
            public double lookupVol;

            public double fadingTime = 0;
            /// <summary>
            /// Falls das Volume während des Fadings geändert wird
            /// </summary>
            public double Offset = 0;  
            public double zielVol;
            public double startVol = 0;
            public bool fadingOutSofort;
            public bool mPlayerStoppen;
            public Musik BG;
            public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile;
            public GruppenObjekt grpobj = null;

            public DateTime fadingInStartet = DateTime.MinValue;
            public DateTime fadingOutStartet = DateTime.MinValue;
        }

        [DependentProperty("AktKlangPlaylist")]
        public string imgListboxPlaylist
        {
            get
            {
                if (AktKlangPlaylist != null)
                    return (AktKlangPlaylist.Hintergrundmusik ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png" :
                                                         "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/speaker.png");
                else
                    return "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/copy.png";
            }
        }

        public class GruppenObjekt 
        {
            public MusikZeileVM mZeileVM = null;
            public double totalTimePlylist = 0;
            public double Vol_PlaylistMod = 0;
            public DateTime LastVolUpdate = DateTime.Now;
            public uint sollBtnGedrueckt = 0;
            public Audio_Playlist aPlaylist = null;
            public List<Audio_Playlist_Titel> aPlaylistTitel;
            public int objGruppe;
            public UInt16 anzVolChange = 0;
            public UInt16 anzPauseChange = 0;            
            public List<KlangZeile> _listZeile = new List<KlangZeile>();

            private string _listTitelLaufend = null;
            public string ListTitelLaufend
            {
                get { return _listTitelLaufend; }
                set { _listTitelLaufend = value; }
            }


            private bool _wirdAbgespielt = false;
            public bool wirdAbgespielt
            {
                get { return _wirdAbgespielt; }
                set { _wirdAbgespielt = value; }
            }
            public List<Guid> NochZuSpielen = new List<Guid>();

            public List<UInt16> Gespielt = new List<UInt16>();

            public bool DoForceVolume = false;
            public Nullable<double> force_Volume = null;

            public bool visuell = false;
            public DispatcherTimer wartezeitTimer = new DispatcherTimer();

            public TabItem tiEditor = null;
            public ToggleButton tbtnKlangPause = null;
            public ListBox lbEditorListe = null;
        }

        public class KlangZeile
        {
            public int playMediaFailed = 0;
            public UInt16 ID_Zeile;
            public AudioData aData = new AudioData();
            public Audio_Playlist_Titel aPlaylistTitel = new Audio_Playlist_Titel();
            public int mediaHashCode = 0;
            public bool FadingOutStarted = false;
            public bool istPause = false;

            private string _titelLaufend = null;
            public string TitelLaufend
            {
                get { return _titelLaufend; }
                set { _titelLaufend = value; }
            }

            private bool _istLaufend = false;
            public bool istLaufend
            {
                get { return _istLaufend; }
                set
                {
                    _istLaufend = value;
                    TitelLaufend = value? aPlaylistTitel.Audio_Titel.Name: null;
                }
            }
            public bool istWartezeit = false;
            public bool istStandby = false;
            public bool playable = true;
            public int pauseMax_wert = 9000;
            public int pauseMin_wert = 0;
            public double volMax_wert = 100;
            public double volMin_wert = 0;
            public double volZiel = 50;
            public double Vol_jump = 1;            // Volumesprung bei variabler Lautstärkenänderung

            public double Aktuell_Volume = 50;

            //public double playspeed = 1;
            public UInt16 UpdateZyklusVol = 3;  //Sekunden neuen Zielwert ermitteln
            public DateTime dtLiedLastCheck = DateTime.MinValue;

            private AudioZeileVM _audioZeileVM = null;
            public AudioZeileVM audioZeileVM
            {
                get { return _audioZeileVM; }
                set
                {
                    _audioZeileVM = value;

                    if (value != null)
                    {
                        value.PropertyChanged += value_PropertyChanged;
                    }
                }
            }

            void value_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "aPlayTitelVolume" && !FadingOutStarted) Aktuell_Volume = ((AudioZeileVM)sender).aPlayTitelVolume;

                if (sender is AudioZeileVM && e.PropertyName == "ShowHotkeyPanel")
                {
                    if (volMin_wert != ((AudioZeileVM)sender).aPlayTitel.VolumeMin) volMin_wert = ((AudioZeileVM)sender).aPlayTitel.VolumeMin;
                    if (volMax_wert != ((AudioZeileVM)sender).aPlayTitel.VolumeMax) volMax_wert = ((AudioZeileVM)sender).aPlayTitel.VolumeMax;

                    if (pauseMin_wert != ((AudioZeileVM)sender).aPlayTitelPauseMin) pauseMin_wert = ((AudioZeileVM)sender).aPlayTitelPauseMin;
                    if (pauseMax_wert != ((AudioZeileVM)sender).aPlayTitelPauseMax) pauseMax_wert = ((AudioZeileVM)sender).aPlayTitelPauseMax;
                }
                if (e.PropertyName == "aPlayTitelSpeed")
                {
                    ((AudioZeileVM)sender).aPlayTitel.Speed = ((AudioZeileVM)sender).aPlayTitelSpeed;
                    aData.setSpeed(((AudioZeileVM)sender).aPlayTitel.Speed);
                }
                if (e.PropertyName == "aPlayTitelPitch")
                {
                    ((AudioZeileVM)sender).aPlayTitel.Pitch = ((AudioZeileVM)sender).aPlayTitelPitch;
                    aData.setPitch(((AudioZeileVM)sender).aPlayTitelPitch);
                }
                if (e.PropertyName == "aPlayTitelEcho")
                {
                    //((AudioZeileVM)sender).aPlayTitel.Echo = ((AudioZeileVM)sender).aPlayTitelEcho;
                    aData.setEcho(((AudioZeileVM)sender).aPlayTitelEcho);
                }
            }

            public KlangZeile(UInt16 id)
            {
                ID_Zeile = id;
            }
        }

        public class chkeckAnzDateien
        {
            public BackgroundWorker _bkworker = new BackgroundWorker();
            public GruppenObjekt grpobj = null;
            public Audio_Playlist aPlaylist = null;
            public List<Audio_Titel> titelliste;
            public string titelRef = null;
            public List<string> allFilesMP3 = new List<string>();
            public List<string> allFilesWAV = new List<string>();
            public List<string> allFilesOGG = new List<string>();
            public List<string> allFilesWMA = new List<string>();
        }

        private Int16 _wiederholungenLeft;
            public Int16 wiederholungenLeft
            {
                get { return _wiederholungenLeft; }
                set { Set(ref _wiederholungenLeft , value); }
            }

        public class MusikView
        {
            private bool _isMuted;
            public bool isMuted
            {
                get { return _isMuted; }
                set { _isMuted = value; }
            }
            private bool _isPaused;
            public bool isPaused
            {
                get { return _isPaused; }
                set { _isPaused = value; }
            }
            public List<Musik> BG = new List<Musik>();
            public string[] s = new string[2];
            public double totalLength;
            public List<Guid> NochZuSpielen = new List<Guid>();
            public List<Guid> Gespielt = new List<Guid>();
            public List<Guid> MusikNOK = new List<Guid>();
            public Audio_Playlist AktPlaylist;
            public Audio_Playlist_Titel AktPlaylistTitel;
            
            public List<Audio_Titel> AktTitel = new List<Audio_Titel>();
        }

        public class TitelInfo : Base.ViewModelBase
        {
            private Audio_Playlist_Titel _playlistTitel;

            public Audio_Playlist_Titel PlaylistTitel
            {
                get { return _playlistTitel; }
            }

            public TitelInfo(Audio_Playlist_Titel aPlaylistTitel)
            {
                _playlistTitel = aPlaylistTitel;
            }

            public event EventHandler OnRemoveTitel;
            void RemoveTitel(object sender)
            {
                Global.ContextAudio.RemoveTitelFromPlaylist(PlaylistTitel);
                if (OnRemoveTitel != null)
                {
                    OnRemoveTitel(this, new EventArgs());
                }
            }

        }
        
        #endregion

        #region //---- FELDER & EIGENSCHAFTEN  + Get/Set ----
        public BackgroundWorker workerGetLength = new BackgroundWorker();
        public bool AudioInAnderemPfadSuchen = Einstellungen.AudioInAnderemPfadSuchen;
        public bool AudioSpieldauerBerechnen = Einstellungen.AudioSpieldauerBerechnen;
        private bool firstshot = true;
        public Musik MusikAktiv = new Musik();
                        
        public static RoutedCommand ThemeCommandCheck = new RoutedCommand();
        System.Timers.Timer BGSongTimer = new System.Timers.Timer();
        public List<DispatcherTimer> lstKlangPlayEndetimer = new List<DispatcherTimer>();
        DispatcherTimer KlangPlayEndetimer;

        public List<DispatcherTimer> _listMusikFadingOut = new List<DispatcherTimer>();
        public DispatcherTimer _timerFadingIn = new DispatcherTimer();
        public DispatcherTimer _timerFadingOut = new DispatcherTimer();
        public DispatcherTimer _timerFadingOutGeräusche = new DispatcherTimer();
        public DispatcherTimer _timerFadingInGeräusche = new DispatcherTimer();
        public DispatcherTimer KlangProgBarTimer = new DispatcherTimer();
        public DispatcherTimer MusikProgBarTimer = new DispatcherTimer();
        
        public AudioData FadingIn_Started = new AudioData();
        public AudioData FadingOut_Started = new AudioData();
        
        public double fadingIntervall = 10;
        public double fadingTime = 600;    // * fadingIntervall = Übergang in ms
        public List<string> stdPfad = new List<string>();

        public string[] validExt = new String[6] { "mp3", "wav", "wma", "ogg", "m3u8", "wpl" };

        private double Zeitüberlauf = 1000;   // in ms
        public UInt16 tiErstellt = 0;
        public UInt16 rowErstellt = 0;

        public bool stopFadingIn = false;
        public bool isDeleting = false;

        public int SliderTeile = 25;

        public Nullable<Point> pointerZeileDragDrop = null;
        public Nullable<Point> pointerPlaylistDragDrop = null;
        public lbEditorItem lbiEditorPlaylistStartDnD = null;
        public lbEditorThemeItem lbiEditorThemeStartDnD = null;

        public List<btnHotkey> hotkeyListe = new List<btnHotkey>();
        private List<KlangZeile> _klangzeilen;
        
        private List<GruppenObjekt> _grpObjecte = new List<GruppenObjekt>();
        public List<GruppenObjekt> _GrpObjecte
        {
            get { return _grpObjecte; }
            set { Set(ref _grpObjecte, value); }
        }

        private Audio_Playlist _dropZielPlaylist = null;
        public Audio_Playlist DropZielPlaylist
        {
            get { return _dropZielPlaylist; }
            set { Set(ref _dropZielPlaylist, value); }
        }

        private int _audioZeileMouseOverDropped = 0;
        public int audioZeileMouseOverDropped
        { 
            get { return _audioZeileMouseOverDropped; }
            set { Set(ref _audioZeileMouseOverDropped, value); }
        }

        public int _lbiPlaylistMouseOverDropped = 0;
        public int lbiPlaylistMouseOverDropped
        {
            get { return _lbiPlaylistMouseOverDropped; }
            set { Set(ref _lbiPlaylistMouseOverDropped, value); }
        }
        
        private bool _playlistListeNichtUpdaten = false;
        public bool PlaylistListeNichtUpdaten
        {
            get { return _playlistListeNichtUpdaten; }
            set { Set(ref _playlistListeNichtUpdaten, value);}
        }

        private bool _editReihenfolgeVis;
        public bool editReihenfolgeVis
        {
            get { return _editReihenfolgeVis; }
            set
            {
                _editReihenfolgeVis = PlaylistAZ;                    
                OnChanged(nameof(editReihenfolgeVis));
            }
        }
                        
        private bool _rbEditorEditPlaylist = true;
        public bool rbEditorEditPlaylist
        {
            get { return _rbEditorEditPlaylist; }
            set
            {
                Set(ref _rbEditorEditPlaylist, value);

                EditorListeVisible = rbEditorEditPlaylist ? true : false;                
                SelectedEditorItem = null;
                SelectedEditorThemeItem = null;
                FilterEditorPlaylistListe();
                FilterThemeEditorPlaylistListe();
            }
        }


        private bool _showHotkeyPanel = false;
        [DependentProperty("hotkeyListUsed")]
        public bool ShowHotkeyPanel
        {
            get { return _showHotkeyPanel; }
            set
            {
                _showHotkeyPanel = !value && hotkeyListUsed.Count > 0;
                OnChanged(nameof(ShowHotkeyPanel));
            }
        }
        
        private chkeckAnzDateien _chkAnzDateien = new chkeckAnzDateien();
        public chkeckAnzDateien ChkAnzDateien
        {
            get { return _chkAnzDateien; }
            set { Set(ref _chkAnzDateien, value); }
        }

        private string _chkAnzDateienResult = null;
        public string ChkAnzDateienResult
        {
            get { return _chkAnzDateienResult; }
            set { Set(ref _chkAnzDateienResult, value); }
        }

        private bool _chkAnzDateienVerfügbar = false;
        public bool ChkAnzDateienVerfügbar
        {
            get { return _chkAnzDateienVerfügbar; }
            set { Set(ref _chkAnzDateienVerfügbar, value); }
        }

        private bool _musikAktivIsPaused;
        public bool MusikAktivIsPaused
        {
            get { return _musikAktivIsPaused; }
            set
            {
                Set(ref _musikAktivIsPaused, value);
                BGPlayer.isPaused = value;
                if (MusikAktiv == null)
                {
                    MusikAktiv = new Musik();
                    MusikAktiv.aPlaylist = BGPlayer.AktPlaylist;
                }
                MusikAktiv.isPaused = value;
            }
        }

        private int _bgPlayerGespieltCount;
        public int BGPlayerGespieltCount
        {
            get { return _bgPlayerGespieltCount; }
            set { Set(ref _bgPlayerGespieltCount, value); }
        }
        
        private List<btnHotkey> _hotkeyListUsed = new List<btnHotkey>();
        public List<btnHotkey> hotkeyListUsed
        {
            get { return _hotkeyListUsed; }
            set
            {
                Set(ref _hotkeyListUsed, value);
                ShowHotkeyPanel = ShowHotkeyPanel;                
            }
        }
        private bool _setHintergrundmusik;
        public bool SetHintergrundmusik
        {
            get { return _setHintergrundmusik; }
            set
            {
                Set(ref _setHintergrundmusik, value);
                if (AktKlangPlaylist != null)
                    AktKlangPlaylist.Hintergrundmusik = value;
                OnChanged("AktKlangPlaylist");
            }
        }

        private bool _setGeräusch;
        public bool SetGeräusch
        {
            get { return _setGeräusch; }
            set
            {
                Set(ref _setGeräusch, value); 
                if (AktKlangPlaylist != null)
                    AktKlangPlaylist.Hintergrundmusik = !value;
                OnChanged("AktKlangPlaylist");
            }
        }

        private bool _editorGroßeAnsicht;
        public bool EditorGroßeAnsicht
        {
            get { return _editorGroßeAnsicht; }
            set
            {
                Set(ref _editorGroßeAnsicht, value);
                FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM) { aZeileVM.EditorGroßeAnsicht = value; });
            }
        }

        private bool _pListGroßeAnsicht = true;
        public bool PListGroßeAnsicht
        {
            get { return _pListGroßeAnsicht; }
            set
            {
                Set(ref _pListGroßeAnsicht, value);
                ErwPlayerMusikListItemListe.ForEach(delegate(MusikZeile mZeile) { mZeile.VM.GroßeAnsicht = PListGroßeAnsicht; });
                ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile) { mZeile.VM.GroßeAnsicht = PListGroßeAnsicht; });
            }
        }

        private bool _themeGroßeAnsicht = false;
        public bool ThemeGroßeAnsicht
        {
            get { return _themeGroßeAnsicht; }
            set
            {
                Set(ref _themeGroßeAnsicht, value);
                ErwPlayerThemeListe.ForEach(delegate(grdThemeButton grdThBtn) { grdThBtn.VM.GroßeAnsicht = ThemeGroßeAnsicht; });
            }
        }

        private int _hotkeyVolume = Einstellungen.GeneralHotkeyVolume;
        public int HotkeyVolume
        {
            get { return _hotkeyVolume; }
            set
            {
                Set(ref _hotkeyVolume, value);
                Einstellungen.SetEinstellung<int>("GeneralHotkeyVolume", _hotkeyVolume); 
            }
        }
        
        private Audio_Playlist _aktKlangPlaylist;
        public Audio_Playlist AktKlangPlaylist
        {
            get { return _aktKlangPlaylist; }
            set
            {
                Set(ref _aktKlangPlaylist, value);
                if (value != null)
                {
                    LadeAudioZeilen();
                    LadeFilteredAudioZeilen();
                }
            }
        }
        
        private Audio_Theme _aktErwPlayerTheme = null;
        public Audio_Theme AktErwPlayerTheme
        {
            get { return _aktErwPlayerTheme; }
            set { Set(ref _aktErwPlayerTheme, value); }
        }
        private Audio_Theme _aktKlangTheme;
        public Audio_Theme AktKlangTheme
        {
            get { return _aktKlangTheme; }
            set
            {
                Set(ref _aktKlangTheme, value);
                LadeAudioZeilen();
            }
        }
        
        private string _aktEditorName;
        public string AktEditorName
        {
            get { return _aktEditorName; }
            set
            {
                if (AktKlangPlaylist != null)
                {
                    AktKlangPlaylist.Name = value;
                    OnChanged("AktKlangPlaylist");
                }
                if (AktKlangTheme != null)
                {
                    AktKlangTheme.Name = value;
                    OnChanged("AktKlangTheme");
                }
                Set(ref _aktEditorName, value);
            }
        }
        
        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikPlaylistItem"), DependentProperty("BGPlayerAktPlaylistTitelTeilAbspielen")]
        public double BGPlayerAktPlaylistTitelLänge
        {
            get
            {
                if (_bgPlayerAktPlaylistTitel != null &&
                    _bgPlayerAktPlaylistTitel.Audio_Titel != null &&
                    (_bgPlayerAktPlaylistTitel.Audio_Titel.Länge == 0 || _bgPlayerAktPlaylistTitel.Audio_Titel.Länge == null) &&
                    _bgPlayerAktPlaylistTitel.Länge != 0)
                    _bgPlayerAktPlaylistTitel.Audio_Titel.Länge = _bgPlayerAktPlaylistTitel.Länge;
                return ((
                    _bgPlayerAktPlaylistTitel != null &&  
                    _bgPlayerAktPlaylistTitel.Audio_Titel.Länge != null &&
                    _bgPlayerAktPlaylistTitel.Audio_Titel.Länge != 0) ? _bgPlayerAktPlaylistTitel.Audio_Titel.Länge.Value :
                    BGPlayer != null && 
                    BGPlayer.AktPlaylistTitel != null && 
                    BGPlayer.AktPlaylistTitel.Audio_Titel.Länge != null? 
                    BGPlayer.AktPlaylistTitel.Audio_Titel.Länge.Value: 10000000);
            }
            set { OnChanged(nameof(BGPlayerAktPlaylistTitelLänge)); }
        }

        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikPlaylistItem"), DependentProperty("BGPlayerAktPlaylistTitelTeilAbspielen")]
        public double BGPlayerAktPlaylistTitelTeilStart
        {
            get { return (_bgPlayerAktPlaylistTitel != null && _bgPlayerAktPlaylistTitel.TeilStart != null ? 
                _bgPlayerAktPlaylistTitel.TeilStart.Value : 0); }
            set 
            {
                BGPlayer.AktPlaylistTitel.TeilStart = value;
                OnChanged(nameof(BGPlayerAktPlaylistTitelTeilStart));
            }
        }

        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikPlaylistItem")]
        public bool BGPlayerAktPlaylistTitelTeilAbspielen
        {
            get { return _bgPlayerAktPlaylistTitel != null ?
                    _bgPlayerAktPlaylistTitel.TeilAbspielen : false; }
            set
            {
                BGPlayer.AktPlaylistTitel.TeilAbspielen = value;
                if (value)
                {
                    if (BGPlayer.AktPlaylistTitel.TeilEnde == null || BGPlayer.AktPlaylistTitel.TeilEnde == 10000000 && BGPlayer.AktPlaylistTitel.Audio_Titel.Länge.Value != 10000000)
                        BGPlayerAktPlaylistTitelTeilEnde = BGPlayer.AktPlaylistTitel.Audio_Titel.Länge.Value;
                    if (BGPlayer.AktPlaylistTitel.TeilStart == null)
                        BGPlayerAktPlaylistTitelTeilStart = 0;
                 //   BGPlayerAktPlaylistTitelTeilStart = BGPlayer.AktPlaylistTitel.TeilStart.Value;
                //    BGPlayerAktPlaylistTitelTeilEnde = BGPlayer.AktPlaylistTitel.TeilEnde.Value;
                }
                OnChanged(nameof(BGPlayerAktPlaylistTitelTeilAbspielen));
            }
        }

        [DependentProperty("SelectedMusikTitelItem"), DependentProperty("SelectedMusikPlaylistItem"), DependentProperty("BGPlayerAktPlaylistTitelTeilAbspielen")]
        public double BGPlayerAktPlaylistTitelTeilEnde
        {
            get { return (_bgPlayerAktPlaylistTitel != null && _bgPlayerAktPlaylistTitel.TeilEnde != null ? 
                _bgPlayerAktPlaylistTitel.TeilEnde.Value : BGPlayerAktPlaylistTitelLänge); }
            set
            {
                if (BGPlayerAktPlaylistTitelLänge < value)
                    BGPlayerAktPlaylistTitelLänge = BGPlayer.AktPlaylistTitel.Länge;
                BGPlayer.AktPlaylistTitel.TeilEnde = value;
                OnChanged(nameof(BGPlayerAktPlaylistTitelTeilEnde));
            }
        }


        private Audio_Playlist_Titel _bgPlayerAktPlaylistTitel;
        public Audio_Playlist_Titel BGPlayerAktPlaylistTitel
        {
            get { return _bgPlayerAktPlaylistTitel; }
            set
            {
                if (_bgPlayerAktPlaylistTitel != value && value != null)
                {
                    if (value.Wiederholungen != null) wiederholungenLeft = value.Wiederholungen.Value;
                    else
                        wiederholungenLeft = 1;
                }
                else
                    if (value == null && BGPlayer !=null) wiederholungenLeft = 0;

                Set(ref _bgPlayerAktPlaylistTitel, value);
                BGPlayer.AktPlaylistTitel = value;                
            }
        }

        private Audio_Playlist _bgPlayerAktPlaylist;
        public Audio_Playlist BGPlayerAktPlaylist
        {
            get { return _bgPlayerAktPlaylist; }
            set { Set(ref _bgPlayerAktPlaylist, value); }
        }

        private MusikView _bgPlayer;
        public MusikView BGPlayer
        {
            get { return _bgPlayer; }
            set
            {
                if (Set(ref _bgPlayer, value))
                {
                    LoadMusikTitelListe();
                    BGPlayerAktPlaylist = BGPlayer.AktPlaylist;
                }
            }
        }

        private bool _hotkeyVisible;
        public bool HotkeyVisible
        {
            get { return _hotkeyVisible; }
            set { Set(ref _hotkeyVisible, value); }
        }
        
        private bool _lbEditorMitGeräusche;
        public bool LbEditorMitGeräusche
        {
            get { return _lbEditorMitGeräusche; }
            set
            {
                if(Set(ref _lbEditorMitGeräusche, value))
                    FilterEditorPlaylistListe();
            }
        }

        private bool _lbEditorMitMusik;
        public bool LbEditorMitMusik
        {
            get { return _lbEditorMitMusik; }
            set
            {
                if(Set(ref _lbEditorMitMusik, value))
                    FilterEditorPlaylistListe();
            }
        }

        private lbEditorThemeItemVM _selectedEditorThemeItem;
        public lbEditorThemeItemVM SelectedEditorThemeItem
        {
            get { return _selectedEditorThemeItem; }
            set
            {
                if (Set(ref _selectedEditorThemeItem, value))
                {
                    AktKlangTheme = value == null ? null : value.ATheme;
                    if (AktKlangTheme != null)
                        LadeBoxThemeList();
                    EditorThemeÜbrigThemeListe = FilterThemeÜbrigListBoxItemListe();
                    FilterEditorPlaylistListe();
                }
            }
        }

        private lbEditorThemeItemVM _lbEditorThemeIconSelected;
        public lbEditorThemeItemVM lbEditorThemeIconSelected
        {
            get { return _lbEditorThemeIconSelected; }
            set
            {
                if (Set(ref _lbEditorThemeIconSelected, value))
                    if (value != null && !AktKlangTheme.Audio_Theme1.Contains(value.ATheme))
                    {
                        AktKlangTheme.Audio_Theme1.Add(value.ATheme);
                        Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
                        lbEditorThemeItemVM lbTheme = SelectedEditorThemeItem;
                        SelectedEditorThemeItem = null;
                        SelectedEditorThemeItem = lbTheme;
                        _lbEditorThemeIconSelected = null;
                    }              
            }
        }

        private lbEditorItemVM _selectedEditorItem;
        public lbEditorItemVM SelectedEditorItem
        {
            get { return _selectedEditorItem; }
            set
            {
                //In der Theme-Editor-Ansicht nichts unternehmen, sodass das D&D im View sauber läuft
                if ((!rbEditorEditPlaylist || PlaylistListeNichtUpdaten) && _selectedEditorItem == null)
                    return;

                //MouseOnSubObject = Pfeile f. Reihenfolge, Export, Löschen (so wird die Playlist nicht vorher geladen)
                if (value != null && value.MouseOnSubObject) 
                    return;

                AktKlangPlaylist = null;
                Set(ref _selectedEditorItem, value);

                if (value == null)
                    EditorListeVisible = false;
                else
                {
                    AktKlangPlaylist = value.APlaylist;
                    
                    //Check Reihenfolge okay
                    bool neuaufbau = false;
                    foreach (Audio_Playlist_Titel aPlayTitel in AktKlangPlaylist.Audio_Playlist_Titel) 
                    { 
                        //Doppelte Reihenfolgenummern
                        if (AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Reihenfolge == aPlayTitel.Reihenfolge) > 1)
                        {
                            sortPlaylist(AktKlangPlaylist, -1);
                            neuaufbau = true;
                            break;
                        }
                    }
                    if (!neuaufbau)
                    {
                        // Nummern richtig vergeben (0 ....)
                        List<Audio_Playlist_Titel> lstPlaylist_Titel = AktKlangPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge).ToList();
                        for (int cnt = 0; cnt < lstPlaylist_Titel.Count - 1; cnt++)
                        {
                            if (lstPlaylist_Titel[cnt].Reihenfolge != cnt)
                            {
                                sortPlaylist(AktKlangPlaylist, -1);
                                break;
                            }
                        }
                    }
                    
                    EditorListeVisible = rbEditorEditPlaylist ? true : false;

                    //Altbestand löschen
                    GruppenObjekt alt_grpobj = _GrpObjecte.FirstOrDefault(t => t.visuell);
                    if (alt_grpobj != null)
                    {
                        AlleKlangSongsAus(alt_grpobj, false, false, false, true);
                        alt_grpobj.wirdAbgespielt = false;
                        _GrpObjecte.Remove(alt_grpobj);
                    }

                    VisualGrpObj();
                    GruppenObjekt grpobj = _GrpObjecte.First(t => t.visuell);
                    grpobj.aPlaylist = value.APlaylist;
                    foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
                    {
                        if (AudioInAnderemPfadSuchen &&
                            !File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + (aPlaylistTitel.Audio_Titel.Datei == null ? "" : aPlaylistTitel.Audio_Titel.Datei)))
                        {
                            aPlaylistTitel.Audio_Titel = setTitelStdPfad(aPlaylistTitel.Audio_Titel);
                            setTitelStdPfad_AufrufeHintereinander = 0;
                            if (File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei))
                                Global.ContextAudio.Update<Audio_Titel>(aPlaylistTitel.Audio_Titel);
                        }
                        KlangNewRow(grpobj, aPlaylistTitel);

                        if (aPlaylistTitel.Aktiv &&
                            !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                        }
                    } 
                    OnChanged("AllTitelAktiv");                    
                    OnChanged("IsVolumeChangeChecked");
                    OnChanged("IsPausenZeitChangeChecked");
                    OnChanged("AktKlangPlaylistWarteZeit");
                    OnChanged("AktKlangPlaylistWarteZeitMin");
                    OnChanged("AktKlangPlaylistWarteZeitMax");
                }

                //Muss nach dem OnChange durchgeführt werden, damit der BackgroundWorker nicht das Speichern der DB beeinflusst 
                if (value != null && !firstshot && MeisterGeister.Logic.Einstellung.Einstellungen.AudioSpieldauerBerechnen)                    
                    GetTotalLength(AktKlangPlaylist, true);
            }
        }  
        

        private MusikZeile _SelectedMusikPlaylistItem;
        public MusikZeile SelectedMusikPlaylistItem
        {
            get { return _SelectedMusikPlaylistItem; }
            set
            {
                if (value == null || value == _SelectedMusikPlaylistItem) 
                    return;     

                Set(ref _SelectedMusikPlaylistItem, value);

                SuchTextMusikTitel = "";
                BGPlayer.AktPlaylist = (value == null || value.VM.aPlaylist == null) ? null : value.VM.aPlaylist;
                BGPlayerAktPlaylistTitel = null;
                BGPlayerAktPlaylist = BGPlayer.AktPlaylist;

                BGPlayer.MusikNOK.Clear();
                BGPlayer.NochZuSpielen.Clear();

                BGPlayer.Gespielt.Clear();
                BGPlayerGespieltCount = 0;
                RenewMusikNochZuSpielen();
                LoadMusikTitelListe();
                                
                if (HintergrundMusikListe.Count > 0)//Filtered
                    SelectedMusikTitelItem = GetNextMusikTitel();
                if (MusikAktiv == null) MusikAktiv = new Musik();
                MusikAktiv.aPlaylist = BGPlayer.AktPlaylist;
                if (!firstshot)
                    GetTotalLength(BGPlayerAktPlaylist, false);
            }
        }
        
        private Brush _background;
        public Brush Background
        {
            get { return _background; }
            set { Set(ref _background, value); }
        }
        
        private bool _musikStern1;
        public bool MusikStern1
        {
            get { return _musikStern1;}
            set { Set(ref _musikStern1, value); }
        }

        private bool _musikStern2;
        public bool MusikStern2
        {
            get { return _musikStern2; }
            set { Set(ref _musikStern2, value); }
        }

        private bool _musikStern3;
        public bool MusikStern3
        {
            get { return _musikStern3; }
            set { Set(ref _musikStern3, value); }
        }

        private bool _musikStern4;
        public bool MusikStern4
        {
            get { return _musikStern4; }
            set { Set(ref _musikStern4, value); }
        }

        private bool _musikStern5;
        public bool MusikStern5
        {
            get { return _musikStern5; }
            set { Set(ref _musikStern5, value); }
        }
        
        private ListBoxItem _selectedMusikTitelItem;
        public ListBoxItem SelectedMusikTitelItem
        {
            get { return _selectedMusikTitelItem; }
            set
            {
                Set(ref _selectedMusikTitelItem, value);
                if (value != null)
                {
                    MusikAktivIsPaused = false;
                    
                    MusikAktiv.aPlaylist = BGPlayer.AktPlaylist;
                    BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID);

                    MusikAktiv.aPlaylist = BGPlayer.AktPlaylist;

                    BGPosition = 0;
                    if (BGPlayerAktPlaylistTitel == null)
                        //Falls nicht gefunden, neuen Titel abspielen
                        SpieleNeuenMusikTitel(Guid.Empty);
                    else
                        SpieleNeuenMusikTitel((Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID);

                    if (BGPlayer.MusikNOK.Contains((Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID))
                    {
                        value.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                        Audio_Playlist_Titel aPlayTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)((Audio_Playlist_Titel)value.Tag).Audio_TitelGUID);
                        value.ToolTip = "Datei nicht gefunden. -> " + aPlayTitel.Audio_Titel.Pfad + "\\" + aPlayTitel.Audio_Titel.Datei;

                        SelectedMusikTitelItem = GetNextMusikTitel();
                    }
                }
                else
                    BGPlayerAktPlaylistTitel = null;
                
                OnChanged("BGPlayerAktPlaylistTitelTeilAbspielen");
                if (BGPlayerAktPlaylistTitel != null)
                {
                    BGPlayerAktPlaylistTitelLänge = BGPlayerAktPlaylistTitel.Länge;
                    if (BGPlayerAktPlaylistTitel.TeilAbspielen)
                    {
                        BGPlayerAktPlaylistTitelTeilStart = BGPlayerAktPlaylistTitel.TeilStart != null ? BGPlayerAktPlaylistTitel.TeilStart.Value : 0;
                        BGPlayerAktPlaylistTitelTeilEnde = BGPlayerAktPlaylistTitel.TeilEnde != null ? BGPlayerAktPlaylistTitel.TeilEnde.Value : BGPlayerAktPlaylistTitel.Länge;
                    }
                }
            }
        }

        private int _onWarteZeitMinChange;
        public int OnWarteZeitMinChange
        {
            get { return _onWarteZeitMinChange; }
            set
            {
                _onWarteZeitMinChange = value;
                OnChanged(nameof(OnWarteZeitMinChange));
            }
        }



        #region //---- Volume-Modifikation ----
        
        private double _fadingGeräuscheVolProzent = 100;
        public double FadingGeräuscheVolProzent
        {
            get { return _fadingGeräuscheVolProzent; }
            set
            {
                double oldVal = _fadingGeräuscheVolProzent;
                Set(ref _fadingGeräuscheVolProzent, value);
                ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile)
                {
                    if (mZeile.VM.grpobj != null)
                        mZeile.VM.grpobj.Vol_PlaylistMod = Convert.ToInt32(value);

                    mZeile.VM.ListZeile.Where(t => !t.aData.muted).All(t => t.aData.setVolume((t.aData.getVolume() / oldVal) * value));
                });

                if (Einstellungen.GeneralGeräuscheVolume != (int)Math.Round(value))
                    Einstellungen.SetEinstellung<int>("GeneralGeräuscheVolume", (int)Math.Round(value));
            }
        }
        
        private Base.CommandBase _onBtnAllUpdateClick = null;
        public Base.CommandBase OnBtnAllUpdateClick
        {
            get
            {
                if (_onBtnAllUpdateClick == null)
                    _onBtnAllUpdateClick = new Base.CommandBase(BtnAllUpdateClick, null);
                return _onBtnAllUpdateClick;
            }
        }
        void BtnAllUpdateClick(object obj)
        {
            Global.SetIsBusy(true, string.Format("Update aller Listen ..."));
            UpdateAlleListen();
            Global.SetIsBusy(false);
        }

        private Base.CommandBase _allVol0 = null;
        public Base.CommandBase OnAllVol0
        {
            get
            {
                if (_allVol0 == null)
                    _allVol0 = new Base.CommandBase(AllVol0, null);
                return _allVol0;
            }
        }
        void AllVol0(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM) { aZeileVM.aPlayTitelVolume = 0; });
        }

        private Base.CommandBase _allVol100 = null;
        public Base.CommandBase OnAllVol100
        {
            get
            {
                if (_allVol100 == null)
                    _allVol100 = new Base.CommandBase(AllVol100, null);
                return _allVol100;
            }
        }
        void AllVol100(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM) { aZeileVM.aPlayTitelVolume = 100; });
        }

        private Base.CommandBase _allVolDown = null;
        public Base.CommandBase OnAllVolDown
        {
            get
            {
                if (_allVolDown == null)
                    _allVolDown = new Base.CommandBase(AllVolDown, null);
                return _allVolDown;
            }
        }
        void AllVolDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelVolume = aZeileVM.aPlayTitel.Volume - 3 >= 0 ? aZeileVM.aPlayTitel.Volume - 3 : 0; });

            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allVolUp = null;
        public Base.CommandBase OnAllVolUp
        {
            get
            {
                if (_allVolUp == null)
                    _allVolUp = new Base.CommandBase(AllVolUp, null);
                return _allVolUp;
            }
        }
        void AllVolUp(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelVolume = aZeileVM.aPlayTitel.Volume + 3 <= 100 ? aZeileVM.aPlayTitel.Volume + 3 : 100; });
        }

        private Base.CommandBase _allVolMinDown = null;
        public Base.CommandBase OnAllVolMinDown
        {
            get
            {
                if (_allVolMinDown == null)
                    _allVolMinDown = new Base.CommandBase(AllVolMinDown, null);
                return _allVolMinDown;
            }
        }
        void AllVolMinDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelVolumeMin = aZeileVM.aPlayTitel.VolumeMin - 3 >= 0 ? aZeileVM.aPlayTitel.VolumeMin - 3 : 0; });
        }

        private Base.CommandBase _allVolMaxDown = null;
        public Base.CommandBase OnAllVolMaxDown
        {
            get
            {
                if (_allVolMaxDown == null)
                    _allVolMaxDown = new Base.CommandBase(AllVolMaxDown, null);
                return _allVolMaxDown;
            }
        }
        void AllVolMaxDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelVolumeMax = aZeileVM.aPlayTitel.VolumeMax - 3 >= 0 ? aZeileVM.aPlayTitel.VolumeMax - 3 : 0; });
        }

        private Base.CommandBase _allVolMinUp = null;
        public Base.CommandBase OnAllVolMinUp
        {
            get
            {
                if (_allVolMinUp == null)
                    _allVolMinUp = new Base.CommandBase(AllVolMinUp, null);
                return _allVolMinUp;
            }
        }
        void AllVolMinUp(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelVolumeMin = aZeileVM.aPlayTitel.VolumeMin + 3 <= 100 ? aZeileVM.aPlayTitel.VolumeMin + 3 : 100; });
        }

        private Base.CommandBase _allVolMaxUp = null;
        public Base.CommandBase OnAllVolMaxUp
        {
            get
            {
                if (_allVolMaxUp == null)
                    _allVolMaxUp = new Base.CommandBase(AllVolMaxUp, null);
                return _allVolMaxUp;
            }
        }
        void AllVolMaxUp(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelVolumeMax = aZeileVM.aPlayTitel.VolumeMax + 3 <= 100 ? aZeileVM.aPlayTitel.VolumeMax + 3 : 100; });
        }

        [DependentProperty("SelectedEditorItem")]
        public bool IsVolumeChangeChecked
        {
            get
            {
                bool _isVolumeChangeChecked = true;
                if (AktKlangPlaylist != null && !AktKlangPlaylist.Hintergrundmusik)
                {
                    foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
                    {
                        if (!aZeileVM.aPlayTitelVolumeChange)
                        {
                            _isVolumeChangeChecked = false;
                            break;
                        }
                    }
                }
                return _isVolumeChangeChecked;
            }
            set {
                      OnChanged("IsVolumeChangeChecked");}
        }

        private Base.CommandBase _onAllVolumeChange = null;
        public Base.CommandBase OnAllVolumeChange
        {
            get
            {
                if (_onAllVolumeChange == null)
                    _onAllVolumeChange = new Base.CommandBase(AllVolumeChange, null);
                return _onAllVolumeChange;
            }
        }
        void AllVolumeChange(object obj)
        {
            bool ziel = !IsVolumeChangeChecked;
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            {
                aZeileVM.aPlayTitel.VolumeChange = ziel;
                aZeileVM.aPlayTitelVolumeChange = ziel;
            });
            OnChanged("IsVolumeChangeChecked");
        }

        #endregion

        #region //---- Pausenzeit-Modifikation ----

        private Base.CommandBase _onPausenZeitMaxPlus = null;
        public Base.CommandBase OnPausenZeitMaxPlus
        {
            get
            {
                if (_onPausenZeitMaxPlus == null)
                    _onPausenZeitMaxPlus = new Base.CommandBase(PausenZeitMaxPlus, null);
                return _onPausenZeitMaxPlus;
            }
        }
        void PausenZeitMaxPlus(object obj)
        {
            int max = 900000;
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {
                int sollWert = (aZeileVM.aPlayTitelPauseMax >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMax >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMax = sollWert > max ? max : aZeileVM.aPlayTitelPauseMax + sollWert;
            }
            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onPausenZeitMaxMinus = null;
        public Base.CommandBase OnPausenZeitMaxMinus
        {
            get
            {
                if (_onPausenZeitMaxMinus == null)
                    _onPausenZeitMaxMinus = new Base.CommandBase(PausenZeitMaxMinus, null);
                return _onPausenZeitMaxMinus;
            }
        }
        void PausenZeitMaxMinus(object obj)
        {
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {
                int sollWert = (aZeileVM.aPlayTitelPauseMax >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMax >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMax = aZeileVM.aPlayTitelPauseMax - sollWert < 0 ? 0 : aZeileVM.aPlayTitelPauseMax - sollWert;

                if (aZeileVM.aPlayTitelPauseMin > aZeileVM.aPlayTitelPauseMax)
                    aZeileVM.aPlayTitelPauseMin = aZeileVM.aPlayTitelPauseMax;
            }
            OnChanged("AktKlangPlaylist");
        }



        private Base.CommandBase _onPausenZeitMinPlus = null;
        public Base.CommandBase OnPausenZeitMinPlus
        {
            get
            {
                if (_onPausenZeitMinPlus == null)
                    _onPausenZeitMinPlus = new Base.CommandBase(PausenZeitMinPlus, null);
                return _onPausenZeitMinPlus;
            }
        }
        void PausenZeitMinPlus(object obj)
        {
            int max = 900000;

            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {
                int sollWert = (aZeileVM.aPlayTitelPauseMin >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMin >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMin = sollWert > max ? max : aZeileVM.aPlayTitelPauseMin + sollWert;

                if (aZeileVM.aPlayTitelPauseMax < aZeileVM.aPlayTitelPauseMin)
                    aZeileVM.aPlayTitelPauseMax = aZeileVM.aPlayTitelPauseMin;
            }
            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onPausenZeitMinMinus = null;
        public Base.CommandBase OnPausenZeitMinMinus
        {
            get
            {
                if (_onPausenZeitMinMinus == null)
                    _onPausenZeitMinMinus = new Base.CommandBase(PausenZeitMinMinus, null);
                return _onPausenZeitMinMinus;
            }
        }
        void PausenZeitMinMinus(object obj)
        {
            foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
            {
                int sollWert = (aZeileVM.aPlayTitelPauseMin >= 10000) ? 5000 :
                               (aZeileVM.aPlayTitelPauseMin >= 2000) ? 1000 : 200;
                aZeileVM.aPlayTitelPauseMin = aZeileVM.aPlayTitelPauseMin - sollWert < 0 ? 0 : aZeileVM.aPlayTitelPauseMin - sollWert;
            }
            OnChanged("AktKlangPlaylist");
        }

        public bool IsPausenZeitChangeChecked
        {
            get
            {
                bool _isPausenZeitChangeChecked = true;
                if (AktKlangPlaylist != null && !AktKlangPlaylist.Hintergrundmusik)
                {
                    foreach (AudioZeileVM aZeileVM in FilteredLbEditorAudioZeilenListe)
                    {
                        if (!aZeileVM.aPlayTitelPauseChange)
                        {
                            _isPausenZeitChangeChecked = false;
                            break;
                        }
                    }
                }
                return _isPausenZeitChangeChecked;
            }
            set { OnChanged(nameof(IsPausenZeitChangeChecked)); }
        }

        private Base.CommandBase _onAllPausenZeitChange = null;
        public Base.CommandBase OnAllPausenZeitChange
        {
            get
            {
                if (_onAllPausenZeitChange == null)
                    _onAllPausenZeitChange = new Base.CommandBase(AllPausenZeitChange, null);
                return _onAllPausenZeitChange;
            }
        }
        void AllPausenZeitChange(object obj)
        {
            bool ziel = !IsPausenZeitChangeChecked;
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitel.PauseChange = ziel; aZeileVM.aPlayTitelPauseChange = ziel; });
            OnChanged(nameof(IsPausenZeitChangeChecked));
        }


        private Base.CommandBase _allPause0 = null;
        public Base.CommandBase OnAllPause0
        {
            get
            {
                if (_allPause0 == null)
                    _allPause0 = new Base.CommandBase(AllPause0, null);
                return _allPause0;
            }
        }
        void AllPause0(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelPause = 0; });
            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allPause100 = null;
        public Base.CommandBase OnAllPause100
        {
            get
            {
                if (_allPause100 == null)
                    _allPause100 = new Base.CommandBase(AllPause100, null);
                return _allPause100;
            }
        }
        void AllPause100(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            { aZeileVM.aPlayTitelPause = 9000000; });
            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allPauseDown = null;
        public Base.CommandBase OnAllPauseDown
        {
            get
            {
                if (_allPauseDown == null)
                    _allPauseDown = new Base.CommandBase(AllPauseDown, null);
                return _allPauseDown;
            }
        }
        void AllPauseDown(object obj)
        {
            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            {
                if (aZeileVM.aPlayTitelPause > SliderTicks.Min())
                {
                    if (!SliderTicks.Contains(aZeileVM.aPlayTitelPause))
                    {
                        int i = 0;
                        while (SliderTicks[i] != SliderTicks.Max() && SliderTicks[i] < aZeileVM.aPlayTitelPause &&
                            i < SliderTicks.Count - 1) i++;
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[i];
                    }
                    else
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[SliderTicks.IndexOf(aZeileVM.aPlayTitelPause) - 1];

                }
            });
            OnChanged("AktKlangPlaylist");
        }

        private Base.CommandBase _allPauseUp = null;
        public Base.CommandBase OnAllPauseUp
        {
            get
            {
                if (_allPauseUp == null)
                    _allPauseUp = new Base.CommandBase(AllPauseUp, null);
                return _allPauseUp;
            }
        }
        void AllPauseUp(object obj)
        {

            FilteredLbEditorAudioZeilenListe.ForEach(delegate(AudioZeileVM aZeileVM)
            {
                if (aZeileVM.aPlayTitelPause < SliderTicks.Max())
                {
                    if (!SliderTicks.Contains(aZeileVM.aPlayTitelPause))
                    {
                        int i = SliderTicks.Count - 1;
                        while (SliderTicks[i] != SliderTicks.Min() && SliderTicks[i] > aZeileVM.aPlayTitelPause &&
                            i < SliderTicks.Count - 1) i++;
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[i];
                    }
                    else
                        aZeileVM.aPlayTitelPause = (long)SliderTicks[SliderTicks.IndexOf(aZeileVM.aPlayTitelPause) + 1];
                }
            });
            OnChanged("AktKlangPlaylist");
        }


        /// <summary>
        /// Sucht den nächsten verfügbaren Listen-Namen
        /// 0 = PlaylistListe
        /// 1 = ThemeListe
        /// </summary>
        public string GetNeuenNamen(string titel, int liste)
        {
            string NeuerName = titel;
            int ver = 0;
            if (liste == 0)
            {
                Audio_Playlist playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuerName));

                while (playlistlist != null)
                {
                    NeuerName = titel + "-" + ver;
                    ver++;
                    playlistlist = Global.ContextAudio.PlaylistListe.Find(t => t.Name.Equals(NeuerName));
                }
            }
            else
            {
                if (liste == 1)
                {
                    Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuerName));

                    while (themelist != null &&
                        (themelist.Audio_Playlist.Count != 0 ||
                        themelist.Audio_Theme1.Count != 0 ||
                        themelist.Audio_Theme2.Count != 0))
                    {
                        NeuerName = titel + "-" + ver;
                        ver++;
                        themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(NeuerName));
                    }
                }
            }
            return NeuerName;
        }

        #endregion

        #region //---- Wartezeit-Modifikation ----


        private Base.CommandBase _onWarteZeitMaxPlus = null;
        public Base.CommandBase OnWarteZeitMaxPlus
        {
            get
            {
                if (_onWarteZeitMaxPlus == null)
                    _onWarteZeitMaxPlus = new Base.CommandBase(WarteZeitMaxPlus, null);
                return _onWarteZeitMaxPlus;
            }
        }
        void WarteZeitMaxPlus(object obj)
        {
            int max = 900000;

            int sollWert = (AktKlangPlaylist.WarteZeitMax >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMax >= 2000) ? 1000 : 200;

            AktKlangPlaylist.WarteZeitMax = sollWert > max ? max : AktKlangPlaylist.WarteZeitMax + sollWert;

            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onWarteZeitMaxMinus = null;
        public Base.CommandBase OnWarteZeitMaxMinus
        {
            get
            {
                if (_onWarteZeitMaxMinus == null)
                    _onWarteZeitMaxMinus = new Base.CommandBase(WarteZeitMaxMinus, null);
                return _onWarteZeitMaxMinus;
            }
        }
        void WarteZeitMaxMinus(object obj)
        {
            int sollWert = (AktKlangPlaylist.WarteZeitMax >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMax >= 2000) ? 1000 : 200;

            AktKlangPlaylist.WarteZeitMax = AktKlangPlaylist.WarteZeitMax - sollWert < 0 ? 0 : AktKlangPlaylist.WarteZeitMax - sollWert;

            if (AktKlangPlaylist.WarteZeitMin > AktKlangPlaylist.WarteZeitMax)
                AktKlangPlaylist.WarteZeitMin = AktKlangPlaylist.WarteZeitMax;

            OnChanged("AktKlangPlaylist");
        }


        private Base.CommandBase _onWarteZeitMinPlus = null;
        public Base.CommandBase OnWarteZeitMinPlus
        {
            get
            {
                if (_onWarteZeitMinPlus == null)
                    _onWarteZeitMinPlus = new Base.CommandBase(WarteZeitMinPlus, null);
                return _onWarteZeitMinPlus;
            }
        }
        void WarteZeitMinPlus(object obj)
        {
            int max = 900000;

            int sollWert = (AktKlangPlaylist.WarteZeitMin >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMin >= 2000) ? 1000 : 200;
            AktKlangPlaylist.WarteZeitMin = sollWert > max ? max : AktKlangPlaylist.WarteZeitMin + sollWert;

            if (AktKlangPlaylist.WarteZeitMax < AktKlangPlaylist.WarteZeitMin)
                AktKlangPlaylist.WarteZeitMax = AktKlangPlaylist.WarteZeitMin;

            OnChanged("AktKlangPlaylist");
        }
        private Base.CommandBase _onWarteZeitMinMinus = null;
        public Base.CommandBase OnWarteZeitMinMinus
        {
            get
            {
                if (_onWarteZeitMinMinus == null)
                    _onWarteZeitMinMinus = new Base.CommandBase(WarteZeitMinMinus, null);
                return _onWarteZeitMinMinus;
            }
        }

        void WarteZeitMinMinus(object obj)
        {
            int sollWert = (AktKlangPlaylist.WarteZeitMin >= 10000) ? 5000 :
                           (AktKlangPlaylist.WarteZeitMin >= 2000) ? 1000 : 200;

            AktKlangPlaylist.WarteZeitMin = AktKlangPlaylist.WarteZeitMin - sollWert < 0 ? 0 : AktKlangPlaylist.WarteZeitMin - sollWert;

            OnChanged("AktKlangPlaylist");
        }

        #endregion
             

        #endregion

        #region //---- Listen ----
                        
        private List<boxThemeTheme> _boxThemeThemeHintergrundList;
        public List<boxThemeTheme> boxThemeThemeHintergrundList
        {
            get { return _boxThemeThemeHintergrundList; }
            set { Set(ref _boxThemeThemeHintergrundList, value); }
        }

        private List<boxThemeTheme> _boxThemeThemeGeräuscheList;
        public List<boxThemeTheme> boxThemeThemeGeräuscheList
        {
            get { return _boxThemeThemeGeräuscheList; }
            set { Set(ref _boxThemeThemeGeräuscheList, value); }
        }

        private List<boxThemeTheme> _boxThemeThemeList;
        public List<boxThemeTheme> boxThemeThemeList
        {
            get { return _boxThemeThemeList; }
            set { Set(ref _boxThemeThemeList, value); }
        }

        public List<KlangZeile> KlangZeilen
        {
            get { return _klangzeilen; }
            set { Set(ref _klangzeilen, value); }
        }

        private List<ListBoxItem> _hintergrundMusikListe;
        public List<ListBoxItem> HintergrundMusikListe
        {
            get { return _hintergrundMusikListe; }
            set { Set(ref _hintergrundMusikListe, value); }
        }

        private List<ListBoxItem> _filterehintergrundMusikListe;
        public List<ListBoxItem> FilteredHintergrundMusikListe
        {
            get { return _filterehintergrundMusikListe; }
            set { Set(ref _filterehintergrundMusikListe, value); }
        }

        private List<MusikZeile> _musikListItemListe;
        public List<MusikZeile> MusikListItemListe
        {
            get { return _musikListItemListe; }
            set
            {
                Set(ref _musikListItemListe, value);

                FilterMusikPlaylistListe();
                ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
                FilterErwPlayerMusikPlaylistListe();
            }
        }

        private List<MusikZeile> _filteredMusikPlaylistItemListe;
        public List<MusikZeile> FilteredMusikPlaylistItemListe
        {
            get { return _filteredMusikPlaylistItemListe; }
            set { Set(ref _filteredMusikPlaylistItemListe, value); }
        }

        private List<MusikZeile> _filteredErwPlayerGeräuscheListItemListe;
        public List<MusikZeile> FilteredErwPlayerGeräuscheListItemListe
        {
            get { return _filteredErwPlayerGeräuscheListItemListe; }
            set { Set(ref _filteredErwPlayerGeräuscheListItemListe, value); }
        }

        private List<MusikZeile> _erwPlayerGeräuscheListItemListe;
        public List<MusikZeile> ErwPlayerGeräuscheListItemListe
        {
            get { return _erwPlayerGeräuscheListItemListe; }
            set
            {
                Set(ref _erwPlayerGeräuscheListItemListe, value);
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        private List<MusikZeile> _filteredErwPlayerMusikListItemListe;
        public List<MusikZeile> FilteredErwPlayerMusikListItemListe
        {
            get { return _filteredErwPlayerMusikListItemListe; }
            set { Set(ref _filteredErwPlayerMusikListItemListe, value); }
        }

        private List<MusikZeile> _erwPlayerMusikListItemListe;
        public List<MusikZeile> ErwPlayerMusikListItemListe
        {
            get { return _erwPlayerMusikListItemListe; }
            set
            {
                value.ForEach(delegate(MusikZeile mZeile) { mZeile.VM.GroßeAnsicht = PListGroßeAnsicht; });
                Set(ref _erwPlayerMusikListItemListe, value);
                OnChanged("FilteredErwPlayerMusikListItemListe");
            }
        }

        private List<grdThemeButton> _filteredErwPlayerThemeListe;
        public List<grdThemeButton> FilteredErwPlayerThemeListe
        {
            get { return _filteredErwPlayerThemeListe; }
            set { Set(ref _filteredErwPlayerThemeListe, value); }
        }

        private List<grdThemeButton> _erwPlayerThemeListe;
        public List<grdThemeButton> ErwPlayerThemeListe
        {
            get { return _erwPlayerThemeListe; }
            set { Set(ref _erwPlayerThemeListe, value); }
        }

        private List<lbEditorThemeItemVM> _editorThemeÜbrigThemeListe;
        public List<lbEditorThemeItemVM> EditorThemeÜbrigThemeListe
        {
            get { return _editorThemeÜbrigThemeListe; }
            set { Set(ref _editorThemeÜbrigThemeListe, value); }
        }

        private List<lbEditorThemeItemVM> _editorThemeListBoxItemListe;
        public List<lbEditorThemeItemVM> EditorThemeListBoxItemListe
        {
            get { return _editorThemeListBoxItemListe; }
            set { Set(ref _editorThemeListBoxItemListe, value); }
        }

        private List<lbEditorThemeItemVM> _filteredEditorThemeListBoxItemListe;
        public List<lbEditorThemeItemVM> FilteredEditorThemeListBoxItemListe
        {
            get { return _filteredEditorThemeListBoxItemListe; }
            set { Set(ref _filteredEditorThemeListBoxItemListe, value); }
        }

        private List<lbEditorItemVM> _editorListBoxItemListe;
        public List<lbEditorItemVM> EditorListBoxItemListe
        {
            get { return _editorListBoxItemListe; }
            set
            {
                Set(ref _editorListBoxItemListe, value);
                FilterEditorPlaylistListe();
            }
        }

        private List<lbEditorItemVM> _filteredEditorListBoxItemListe;
        public List<lbEditorItemVM> FilteredEditorListBoxItemListe
        {
            get { return _filteredEditorListBoxItemListe; }
            set
            {
                Set(ref _filteredEditorListBoxItemListe, value);

                FilterMusikPlaylistListe();
                FilterErwPlayerMusikPlaylistListe();
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        private List<AudioZeileVM> _filteredLbEditorAudioZeilenListe;
        public List<AudioZeileVM> FilteredLbEditorAudioZeilenListe
        {
            get { return _filteredLbEditorAudioZeilenListe; }
            set { Set(ref _filteredLbEditorAudioZeilenListe, value); }
        }

        private List<AudioZeileVM> _lbEditorIAudioZeilenListe = new List<AudioZeileVM>();
        public List<AudioZeileVM> LbEditorAudioZeilenListe
        {
            get { return _lbEditorIAudioZeilenListe; }
            set { Set(ref _lbEditorIAudioZeilenListe, value); }
        }

        private AudioZeileVM _lbEditorAudioZeilenSelected = new AudioZeileVM();
        public AudioZeileVM LbEditorAudioZeilenSelected
        {
            get { return _lbEditorAudioZeilenSelected; }
            set { Set(ref _lbEditorAudioZeilenSelected, value); }
        }

        private List<string> _hotkeysAvailable = new List<string>();
        public List<string> HotkeysAvailable
        {
            get { return _hotkeysAvailable; }
            set
            {
                Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null).ForEach(ti => _hotkeysAvailable.Add(ti.Key));
                Set(ref _hotkeysAvailable, value); 
            }
        }
        
        private List<string> getWinampFilesFromPlaylist(string filename)
        {
            List<string> lstFiles = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        if (line.Substring(1, 2) == ":\\")
                            lstFiles.Add(line);
                        else
                            if (!line.StartsWith("#"))
                            {
                                string s = System.IO.Path.GetDirectoryName(filename);
                                lstFiles.Add(s.EndsWith("\\") ? s + line : s + "\\" + line);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Lesefehler" + Environment.NewLine + "Beim Auslesen der Playliste '" + filename + "' ist ein Fehler aufgetreten.", ex);
            }
            return lstFiles;
        }

        private List<string> getMPlayerFilesFromPlaylist(string filename)
        {
            List<string> lstFiles = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine().Trim();

                        if (line.StartsWith("<media src="))
                        {
                            line = line.Substring(line.IndexOf("\"") + 1);
                            line = line.Substring(0, line.IndexOf("\""));
                            lstFiles.Add(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Lesefehler" + Environment.NewLine + "Beim Auslesen der Playliste '" + filename + "' ist ein Fehler aufgetreten.", ex);
            }
            return lstFiles;
        }
                
        private ObservableCollection<Button> _hotkeyButtons = null;
        public ObservableCollection<Button> HotkeyButtons 
        {
            get { return _hotkeyButtons; }
 
            set
            {
                hotkeyListUsed.ForEach(delegate(btnHotkey hkey)            
                {
                    Audio_Playlist aplylist = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID == hkey.VM.aPlaylistGuid);                    
                    hkey.VM.aPlaylist = aplylist;
                    hkey.VM.TitelPlayList.ForEach(t => t.mp.Stop());
                });
                Set(ref _hotkeyButtons, value); 
            } 
        }
          

        public List<MusikZeile> mZeileErwPlayerGeräuscheNeuErstellen()
        {
            bool vorhanden = false;
            List<MusikZeile> mZeileList = new List<MusikZeile>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe.FindAll(t => !t.Hintergrundmusik))
            {
                //Gruppenobjekte ertsellen
                GruppenObjekt grpobj = _GrpObjecte.Where(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == aplylist.Audio_PlaylistGUID);
                vorhanden = grpobj != null;

                if (!vorhanden)
                {
                    grpobj = new GruppenObjekt();
                }
                    grpobj.aPlaylist = aplylist;

                // Geräusche-Playlisten
                //Erweiterter-Player GeräuscheMusikZeilen - Items erstellen
                MusikZeile mZeileErw = ErwPlayerGeräuscheListItemListe != null ? ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aplylist) : null;
                if (mZeileErw == null)
                {
                    mZeileErw = new MusikZeile();
                    mZeileErw.VM.grpobj = grpobj;
                    mZeileErw.VM.grpobj.wartezeitTimer.Tick += new EventHandler(mZeileErw.VM.wartezeitTimer_Tick);
                }
                    mZeileErw.VM.aPlayerVM = this;
                    if (mZeileErw.VM.aPlaylist != aplylist) mZeileErw.VM.aPlaylist = aplylist;
                

                Grid.SetRow(mZeileErw.grdForceVol, !PListGroßeAnsicht ? 0 : 1);
                Grid.SetColumn(mZeileErw.grdForceVol, !PListGroßeAnsicht ? 2 : 0);
                mZeileErw.Tag = aplylist.Audio_PlaylistGUID;
          //      mZeileErw.chkbxForceVol.Tag = aplylist;
                mZeileErw.sldForceVolume.Tag = aplylist;
                mZeileErw.tboxKategorie.Tag = aplylist.Audio_PlaylistGUID;
                
                mZeileList.Add(mZeileErw);
            }
            return mZeileList;
        }
                
        public List<MusikZeile> mZeileEditorMusikNeuErstellen()
        {           
            List<MusikZeile> mZeileList = new List<MusikZeile>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik))
            {
                //Hintergrund MusikZeilen - Items erstellen
                MusikZeile mZeile = new MusikZeile();

                mZeile.tbtnCheck.Visibility = Visibility.Collapsed;
                mZeile.tboxKategorie.Tag = mZeile.Tag;
                mZeile.Cursor = Cursors.Hand;
                mZeile.Tag = aplylist.Audio_PlaylistGUID;
                mZeile.VM.aPlaylist = aplylist;
                mZeile.VM.GroßeAnsicht = PListGroßeAnsicht;
                mZeile.VM.StdPfad = stdPfad;
                mZeile.VM.Iterations = aplylist.Audio_Playlist_Titel.Count;
                
                mZeileList.Add(mZeile);
            }
            return mZeileList;
        }

        public List<MusikZeile> mZeileErwPlayerMusikNeuErstellen()
        {
            List<MusikZeile> mZeileList = new List<MusikZeile>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Hintergrundmusik))
            {                            
                //Erweiterter-Player HintergrundMusikZeilen - Items erstellen
                MusikZeile mZeileErw = new MusikZeile();
                mZeileErw.VM.GroßeAnsicht = PListGroßeAnsicht;
                mZeileErw.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                mZeileErw.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                mZeileErw.Cursor = Cursors.Hand; 
                mZeileErw.Tag = aplylist.Audio_PlaylistGUID;
                mZeileErw.VM.aPlaylist = aplylist;
                mZeileErw.tboxKategorie.Tag = aplylist.Audio_PlaylistGUID;

                mZeileList.Add(mZeileErw);
            }
            return mZeileList;
        }
        

        public List<lbEditorItemVM> lbiPlaylistListNeuErstellen()
        {
            List<lbEditorItemVM> lbiPlaylistList = new List<lbEditorItemVM>();
            foreach (Audio_Playlist aplylist in Global.ContextAudio.PlaylistListe)
            {
                //Playlist - Items erstellen
                lbEditorItemVM lbi = new lbEditorItemVM();
                lbi.APlaylist = aplylist;
                lbi.Name = aplylist.Name;
                //lbi.IstMusik = aplylist.Hintergrundmusik;
                //lbi.PPlaylistName = lbi.Name;
                lbi.PlayerVM = this;
                lbi.Item = lbi.Item;
                
                lbiPlaylistList.Add(lbi);
            }
            return lbiPlaylistList;
        }

        public List<lbEditorThemeItemVM> lbiThemeListNeuErstellen()
        {
            List<lbEditorThemeItemVM> lbiThemeList = new List<lbEditorThemeItemVM>();
            foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                //Theme - Items erstellen        
                lbEditorThemeItemVM lbi = new lbEditorThemeItemVM();
                lbi.ATheme = aTheme;
                lbi.Name = aTheme.Name;
                //lbi.PPlaylistName = aTheme.Name;
                lbi.PlayerVM = this;
                lbi.Item = lbi.Item;
                lbiThemeList.Add(lbi);
            }
            return lbiThemeList;
        }

        private List<grdThemeButton> ThemeErwPlayerListeNeuErstellen()
        {
            List<grdThemeButton> grdThemeBtnList = new List<grdThemeButton>();
            
            foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.FindAll(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                grdThemeButton grdThButton = new grdThemeButton();
                grdThButton.Tag = aTheme.Audio_ThemeGUID;
                grdThButton.VM.Theme = aTheme;
                grdThButton.VM.GroßeAnsicht = ThemeGroßeAnsicht;
                grdThButton.tbtnTheme.Tag = aTheme.Audio_ThemeGUID;
                grdThButton.chkbxPlus.Tag = aTheme;
                grdThButton.chkbxPlus.IsChecked = aTheme.NurGeräusche;
                grdThButton.tbtnTheme.CommandBindings.Add(new CommandBinding(ThemeCommandCheck, ThemeButton_Checked));
                grdThButton.tbtnTheme.Command = ThemeCommandCheck;
                grdThemeBtnList.Add(grdThButton);
            }
            return grdThemeBtnList;
        }

        private List<object> _favPlaylist = new List<object>();
        public List<object> FavPlaylist
        {
            get { return _favPlaylist; }
            set { Set(ref _favPlaylist, value); }
        }
       
        #endregion        
        
        #region //---- View Komponeten ----

        public DoubleCollection SliderTicks
        {
            get
            {
                return new DoubleCollection 
                    {0, 100, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 3000, 4000, 5000, 7500, 8000, 9000, 10000, 15000, 
                     20000, 25000, 30000, 40000, 50000, 60000, 90000, 120000, 180000, 240000, 300000, 450000, 600000, 900000};
            }
        }
        
        public Cursor _dndZeilenCursor = null;
        public Cursor _dndZeilenCursorPlus = null;

        public object DnDZielObject = null;

        private bool _erwPlayerGeräuscheAktiv = false;
        public bool ErwPlayerGeräuscheAktiv
        {
            get { return _erwPlayerGeräuscheAktiv; }
            set
            {
                int wirdAbgespielt = ErwPlayerGeräuscheListItemListe.FindAll(t => t.VM.grpobj.wirdAbgespielt).Count;
                int angehakt = ErwPlayerGeräuscheListItemListe.FindAll(t => t.tbtnCheck.IsChecked.Value).Count;
                
                _erwPlayerGeräuscheAktiv = angehakt > 0;

                if (angehakt >= 1 && wirdAbgespielt == 1 && !ErwPlayerGeräuscheLaufen)
                    ErwPlayerGeräuscheLaufen = true;

                OnChanged(nameof(ErwPlayerGeräuscheAktiv));
            }
        }


        private bool _stdPadAufC = false;
        public bool StdPadAufC
        {
            get { return _stdPadAufC; }
            set { Set(ref _stdPadAufC, value); }
        }

        private bool _editorListeVisible = false;
        public bool EditorListeVisible
        {
            get { return _editorListeVisible; }
            set { Set(ref _editorListeVisible, value); }
        }
                
        private bool _erwPlayerGeräuscheLaufen = true;
        public bool ErwPlayerGeräuscheLaufen
        {
            get { return _erwPlayerGeräuscheLaufen; }
            set { Set(ref _erwPlayerGeräuscheLaufen, value); }
        }

        private bool _berechneSpieldauer = false;
        public bool BerechneSpieldauer
        {
            get { return _berechneSpieldauer; }
            set { Set(ref _berechneSpieldauer, value); }
        }

        private bool _themeGeräuscheFilterAktiv = false;
        public bool ThemeGeräuscheFilterAktiv
        {
            get { return _themeGeräuscheFilterAktiv; }
            set { Set(ref _themeGeräuscheFilterAktiv, value); }
        }


        private string _info_BGTitel;
        public string Info_BGTitel
        {
            get { return _info_BGTitel; }
            set {Set(ref _info_BGTitel, value); }
        }

        private string _info_BGArtist;
        public string Info_BGArtist
        {
            get { return _info_BGArtist; }
            set {Set(ref _info_BGArtist, value); }
        }
        
        private string _info_BGAlbum;
        public string Info_BGAlbum
        {
            get { return _info_BGAlbum; }
            set { Set(ref _info_BGAlbum, value); }
        }

        private string _info_BGJahr;
        public string Info_BGJahr
        {
            get { return _info_BGJahr; }
            set { Set(ref _info_BGJahr, value); }
        }
        
        private string _info_BGGenre;
        public string Info_BGGenre
        {
            get { return _info_BGGenre; }
            set { Set(ref _info_BGGenre, value); }
        }
        
        private double _bgPosition;        
        public double BGPosition
        {
            get { return _bgPosition; }
            set
            {
                Set(ref _bgPosition, value); 
                OnChanged("sBGPosition");
            }
        }

        public bool AllTitelAktiv
        {
            get
            {
                return (AktKlangPlaylist != null) ?
                  (AktKlangPlaylist.Audio_Playlist_Titel.Count(t => t.Aktiv) == AktKlangPlaylist.Audio_Playlist_Titel.Count) : false; 
            }
            set { OnChanged(nameof(AllTitelAktiv)); }
        }

        private bool _isAuswahlHotkey = false;
        public bool IsAuswahlHotkey
        {
            get { return _isAuswahlHotkey; }
            set { Set(ref _isAuswahlHotkey, value); }
        }

        public string sBGPosition
        {
            get
            {
                return MusikAktiv.aData.isStopped() ? "--:--" :
                    TimeSpan.FromMilliseconds(
                    Bass.BASS_ChannelBytes2Seconds(MusikAktiv.aData.audioStream, 
                    Bass.BASS_ChannelGetPosition(MusikAktiv.aData.audioStream)) * 1000).ToString(@"mm\:ss"); 
            }
        }

        private double _musikTeilStart;
        public double MusikTeilStart
        {
            get { return _musikTeilStart; }
            set
            {
                _musikTeilStart = value;
                BGPlayerAktPlaylistTitelTeilStart = value;
                OnChanged(nameof(MusikTeilStart));
            }
        }

        private double _musikTeilEnde;
        public double MusikTeilEnde
        {
            get { return _musikTeilEnde; }
            set
            {
                _musikTeilEnde = value;
                BGPlayerAktPlaylistTitelTeilEnde = value;
                OnChanged(nameof(MusikTeilEnde));
            }
        }

        private double _musikTeilMax = 10000000;
        public double MusikTeilMax
        {
            get { return _musikTeilMax; }
            set { Set(ref _musikTeilMax, value); }
        }
        
        #endregion
 
        #region //---- Commands ----

        private Base.CommandBase _onFavClick = null;
        public Base.CommandBase OnFavClick
        {
            get
            {
                if (_onFavClick == null)
                {
                    _onFavClick = new Base.CommandBase(FavClick, null);
                }
                return _onFavClick;                
            }
        }
        void FavClick(object obj)
        {
            if (obj == null) return;
            if ((obj as ToggleButton).Tag is Audio_Playlist)
            {
                Audio_Playlist aPlay = (obj as ToggleButton).Tag as Audio_Playlist;                
                if (aPlay.Favorite == null || !aPlay.Favorite.Value)
                    FavPlaylist.Remove(aPlay);
                else
                    FavPlaylist.Add(aPlay);
            }
            else
                if ((obj as ToggleButton).Tag is Audio_Theme)
                {
                    Audio_Theme aTheme = (obj as ToggleButton).Tag as Audio_Theme;
                    if (aTheme.Favorite == null || !aTheme.Favorite.Value)
                        FavPlaylist.Remove(aTheme);
                    else
                        FavPlaylist.Add(aTheme);
                }
            
            if (Einstellungen.ShowPlaylistFavorite)
                MainViewModel.Instance.UpdateFavorites();
        }
        

        private Base.CommandBase _onLöschenThemeVonErwPlayer = null;
        public Base.CommandBase OnLöschenThemeVonErwPlayer
        {
            get
            {
                if (_onLöschenThemeVonErwPlayer == null)
                    _onLöschenThemeVonErwPlayer = new Base.CommandBase(LöschenThemeVonErwPlayer, null);
                return _onLöschenThemeVonErwPlayer;
            }
        }
        void LöschenThemeVonErwPlayer(object obj)
        {
            if (ViewHelper.Confirm("Löschen des aktuellen Themes '" + AktErwPlayerTheme.Name + "'", "Wollen Sie wirklich das aktuelle Theme '" +
                AktErwPlayerTheme.Name + "' löschen?"))
            {
                if (!Global.ContextAudio.Delete<Audio_Theme>(AktErwPlayerTheme))
                    Global.ContextAudio.ThemeListe.Remove(AktErwPlayerTheme);

                EditorThemeListBoxItemListe = lbiThemeListNeuErstellen();
                ErwPlayerThemeListe = ThemeErwPlayerListeNeuErstellen();
                Refresh();
                FilterThemeEditorPlaylistListe();
                FilterErwPlayerThemeListe();

            }
        }

        private Base.CommandBase _onNewThemeVonErwPlayer = null;
        public Base.CommandBase OnNewThemeVonErwPlayer
        {
            get
            {
                if (_onNewThemeVonErwPlayer == null)
                    _onNewThemeVonErwPlayer = new Base.CommandBase(NewThemeVonErwPlayer, null);
                return _onNewThemeVonErwPlayer;
            }
        }
        void NewThemeVonErwPlayer(object obj)
        {
            string themeName = ViewHelper.InputDialog("Erstellen eines neuen Themes", 
                "Es wird aus der aktuellen Playlisten-Konstellation ein Theme erstellt." + Environment.NewLine + Environment.NewLine + 
                "Bitte geben Sie den Namen des neuen Themes ein",GetNeuenNamen("Neues Theme",1));
            if (themeName == null) return;

            Audio_Theme aNewTheme = NeuesKlangThemeInDB(themeName);
            aNewTheme.Audio_Playlist.Add(BGPlayerAktPlaylist);

            foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe.FindAll(t => t.tbtnCheck.IsChecked.Value))
                aNewTheme.Audio_Playlist.Add(mZeile.VM.aPlaylist);

            Global.ContextAudio.Update<Audio_Theme>(aNewTheme);
            
            EditorThemeListBoxItemListe = lbiThemeListNeuErstellen();
            ErwPlayerThemeListe = ThemeErwPlayerListeNeuErstellen();
            Refresh();
            FilterThemeEditorPlaylistListe();
            FilterErwPlayerThemeListe();
        }

        private Base.CommandBase _onRbtnPlaylistAlsHintergrundmusik = null;
        public Base.CommandBase OnRbtnPlaylistAlsHintergrundmusik
        {
            get
            {
                if (_onRbtnPlaylistAlsHintergrundmusik == null)
                    _onRbtnPlaylistAlsHintergrundmusik = new Base.CommandBase(RbtnPlaylistAlsHintergrundmusik, null);
                return _onRbtnPlaylistAlsHintergrundmusik;
            }
        }
        void RbtnPlaylistAlsHintergrundmusik(object obj)
        {
            AktKlangPlaylist = _GrpObjecte.FirstOrDefault(t => t.visuell).aPlaylist;

            if (AktKlangPlaylist != null)
            {
                if (AktKlangPlaylist.Hintergrundmusik)
                {
                    MusikZeile mZeile = ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => t.VM.aPlaylist.Audio_PlaylistGUID == AktKlangPlaylist.Audio_PlaylistGUID);
                    if (mZeile != null && mZeile.tbtnCheck.IsChecked.Value)
                        mZeile.tbtnCheck.IsChecked = false;
                }
                else
                {
                    if (AktKlangPlaylist == BGPlayerAktPlaylist)
                        btnBGStoppen(null);
                }
            }

            SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
            Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);

            //Liste Musik-Leiste und ErweiterterPlayer-Leiste aktualisieren
            if (AktKlangPlaylist.Hintergrundmusik)
            {
                MusikListItemListe.Add(einzelneMusikZeileHinzu(AktKlangPlaylist));
                ErwPlayerMusikListItemListe.Add(einzelneErwPlayerMusikZeileHinzu(AktKlangPlaylist));
                ErwPlayerGeräuscheListItemListe.Remove(ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => t.VM.aPlaylist == AktKlangPlaylist));
            }
            else
            {
                MusikListItemListe.Remove(MusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == AktKlangPlaylist));
                ErwPlayerMusikListItemListe.Remove(ErwPlayerMusikListItemListe.FirstOrDefault(t => t.VM.aPlaylist == AktKlangPlaylist));
                ErwPlayerGeräuscheListItemListe.Add(einzelneErwPlayerGeräuschZeileHinzu(AktKlangPlaylist));
            }

            FilterMusikPlaylistListe();
            //FilterThemeEditorPlaylistListe();
            FilterErwPlayerMusikPlaylistListe();
            FilterErwPlayerGeräuschePlaylistListe();
            //FilterErwPlayerThemeListe();
        }

        private MusikZeile einzelneErwPlayerGeräuschZeileHinzu(Audio_Playlist aPlaylist)
        {
            bool vorhanden = false;
            List<MusikZeile> mZeileList = new List<MusikZeile>();

            //Gruppenobjekte ertsellen
            GruppenObjekt grpobj = _GrpObjecte.Where(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID);
            vorhanden = grpobj != null;

            if (!vorhanden)
            {
                grpobj = new GruppenObjekt();
            }
            grpobj.aPlaylist = aPlaylist;

            // Geräusche-Playlisten
            //Erweiterter-Player GeräuscheMusikZeilen - Items erstellen
            MusikZeile mZeileErw = ErwPlayerGeräuscheListItemListe != null ? ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => t.VM.aPlaylist == aPlaylist) : null;
            if (mZeileErw == null)
            {
                mZeileErw = new MusikZeile();
                mZeileErw.VM.grpobj = grpobj;
                mZeileErw.VM.grpobj.wartezeitTimer.Tick += new EventHandler(mZeileErw.VM.wartezeitTimer_Tick);
            }
            mZeileErw.VM.aPlayerVM = this;
            if (mZeileErw.VM.aPlaylist != aPlaylist) mZeileErw.VM.aPlaylist = aPlaylist;

            Grid.SetRow(mZeileErw.grdForceVol, !PListGroßeAnsicht ? 0 : 1);
            Grid.SetColumn(mZeileErw.grdForceVol, !PListGroßeAnsicht ? 2 : 0);
            mZeileErw.Tag = aPlaylist.Audio_PlaylistGUID;
            mZeileErw.sldForceVolume.Tag = aPlaylist;
            mZeileErw.tboxKategorie.Tag = aPlaylist.Audio_PlaylistGUID;

            return mZeileErw;
        }
        
        private MusikZeile einzelneErwPlayerMusikZeileHinzu(Audio_Playlist aPlaylist)
        {                            
            //Erweiterter-Player HintergrundMusikZeilen - Items erstellen
            MusikZeile mZeileErw = new MusikZeile();
            mZeileErw.VM.GroßeAnsicht = PListGroßeAnsicht;
            mZeileErw.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            mZeileErw.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            mZeileErw.Cursor = Cursors.Hand;
            mZeileErw.Tag = aPlaylist.Audio_PlaylistGUID;
            mZeileErw.VM.aPlaylist = aPlaylist;
            mZeileErw.tboxKategorie.Tag = aPlaylist.Audio_PlaylistGUID;

            return mZeileErw;
        }

        private MusikZeile einzelneMusikZeileHinzu(Audio_Playlist aPlaylist)
        {            
            //Hintergrund MusikZeilen - Items erstellen
            MusikZeile mZeile = new MusikZeile();
            mZeile.tbtnCheck.Visibility = Visibility.Collapsed;
            mZeile.tboxKategorie.Tag = mZeile.Tag;
            mZeile.Cursor = Cursors.Hand;
            mZeile.Tag = aPlaylist.Audio_PlaylistGUID;
            mZeile.VM.aPlaylist = aPlaylist;
            mZeile.VM.GroßeAnsicht = PListGroßeAnsicht;
            mZeile.VM.StdPfad = stdPfad;
            mZeile.VM.Iterations = aPlaylist.Audio_Playlist_Titel.Count;

            return mZeile;
        }

        private Base.CommandBase _onBtnUpdateAll = null;
        public Base.CommandBase OnBtnUpdateAll
        {
            get
            {
                if (_onBtnUpdateAll == null)
                    _onBtnUpdateAll = new Base.CommandBase(BtnUpdateAll, null);
                return _onBtnUpdateAll;
            }
        }
        void BtnUpdateAll(object obj)
        {
            if (ViewHelper.ConfirmYesNoCancel("Überprüfung aller Titel der gesamten Musiksammlung",
                "Wollen Sie alle Titel der gesammten Playlisten-Liste überprüfen lassen?" + Environment.NewLine + Environment.NewLine +
                "Es kann unter umständen mehrere Minuten dauern,"+ Environment.NewLine +"bis der Forgang abgeschlossen ist.") != 2)
                return;
            Global.SetIsBusy(true, string.Format("Alle Playlisten werden überprüft ..."));
            int pListAnz = 1;
            int titelAnz = 1;
            int titelChanged = 0;
            int titelCheckedTotal = 0;
            int titelNotChanged = 0;

            foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe)
            {
                string s = aPlaylist.Name.Substring(0, aPlaylist.Name.Length > 12 ? 12 : aPlaylist.Name.Length) + (aPlaylist.Name.Length > 12 ? ".." : "");
                Global.SetIsBusy(true, string.Format("(" + pListAnz + "/" + Global.ContextAudio.PlaylistListe.Count + ") " + s + " wird überprüft"));

                titelAnz = 1;
                foreach (Audio_Playlist_Titel aPlaylistTitel in aPlaylist.Audio_Playlist_Titel)
                {
                    Global.SetIsBusy(true, string.Format("(" + pListAnz + "/" + Global.ContextAudio.PlaylistListe.Count + ") " + s + " (" + titelAnz + "/" + aPlaylist.Audio_Playlist_Titel.Count + ") wird überprüft"));
                    if (!File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + (aPlaylistTitel.Audio_Titel.Datei == null ? "" : aPlaylistTitel.Audio_Titel.Datei)))
                    {
                        aPlaylistTitel.Audio_Titel = setTitelStdPfad(aPlaylistTitel.Audio_Titel);
                        setTitelStdPfad_AufrufeHintereinander = 0;
                        if (File.Exists(aPlaylistTitel.Audio_Titel.Pfad + "\\" + aPlaylistTitel.Audio_Titel.Datei))
                        {
                            Global.ContextAudio.Update<Audio_Titel>(aPlaylistTitel.Audio_Titel);
                            titelChanged++;
                        }
                        else
                            titelNotChanged++;
                    }
                    titelAnz++;
                    titelCheckedTotal++;
                }
                pListAnz++;
            }

            Global.SetIsBusy(false);
            ViewHelper.Popup("Der Vorgang wurde abgeschlossen." + Environment.NewLine + Environment.NewLine +
                "Es wurde insgesamt " + Global.ContextAudio.PlaylistListe.Count + " Playlisten mit " + titelCheckedTotal + " Titel überprüft." +
                Environment.NewLine + Environment.NewLine +
                "Davon wurden " + titelChanged + " Titel auf den korrenten Pfad verwiesen." + Environment.NewLine +
                titelNotChanged + " Titel konnten nicht gefunden werden.");
        }

        private Base.CommandBase _onBtnDeleteAll = null;
        public Base.CommandBase OnBtnDeleteAll
        {
            get
            {
                if (_onBtnDeleteAll == null)
                    _onBtnDeleteAll = new Base.CommandBase(BtnDeleteAll, null);
                return _onBtnDeleteAll;
            }
        }
        void BtnDeleteAll(object obj)
        {
            try
            {
                if (ViewHelper.ConfirmYesNoCancel("Löschen ALLER bestehender Audio-Daten", "Soll die komplette Audio-Datenbank gelöscht werden?" +
                        Environment.NewLine + Environment.NewLine + "Achtung! Alle Daten gehen unwiderruflich verloren.") == 2)
                    DeleteAll(2);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }
                
        private Base.CommandBase _onBtnImportAll = null;
        public Base.CommandBase OnBtnImportAll
        {
            get
            {
                if (_onBtnImportAll == null)
                    _onBtnImportAll = new Base.CommandBase(BtnImportAll, null);
                return _onBtnImportAll;
            }
        }
        void BtnImportAll(object obj)
        {            
            try
            {
                int mrRes = (Global.ContextAudio.PlaylistListe.Count == 0 && Global.ContextAudio.ThemeListe.Count == 0)? 2:
                    ViewHelper.ConfirmYesNoCancel("Löschen bestehender Daten", "Soll die aktuelle Datenbank erweitert werden?" + 
                    Environment.NewLine + Environment.NewLine + "Wählen sie 'Ja' damit die Datenbank erweitert wird." +
                    Environment.NewLine + "Wählen Sie 'Nein' um die bestehende Datenbank zu ersetzten. Achtung! Alle Daten gehen verloren.");                
                if (mrRes == 2 || mrRes == 1)
                {
                    Global.SetIsBusy(true);
                    
                    //Importieren aller Playlisten und danach aller Themelisten
                    
                    System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                    folderDlg.SelectedPath = Environment.CurrentDirectory;
                    folderDlg.Description = "Wählen Sie ein Verzeichnis das alle Dateien der Sicherung enthält";
                    List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();

                    if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (mrRes == 1)
                            DeleteAll(2);

                        Global.SetIsBusy(true, string.Format("Alle Playlisten werden importiert ..."));
                        string pfad = folderDlg.SelectedPath;

                        DirectoryInfo d = new DirectoryInfo(pfad);
                        List<string> listXML = new List<string>();
                        foreach (FileInfo f in d.GetFiles("*.xml").Where(t => t.FullName.ToLower().EndsWith(".xml")).Where(t => t.Length != 0))
                            listXML.Add(f.DirectoryName + "\\" + f.Name);

                        PlaylistenImportieren(listXML);
                        ThemeImportieren(listXML, true);

                        Global.SetIsBusy(true, string.Format("Update aller Listen..."));
                        UpdateAlleListen();
                        if (EditorThemeListBoxItemListe.Count > 0 && AktKlangTheme != null)
                            SelectedEditorThemeItem = EditorThemeListBoxItemListe.First(t => t.ATheme == AktKlangTheme);
                        rbEditorEditPlaylist = false;
                        rbEditorEditPlaylist = true;
                    }
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Komplett-Import ist ein Fehler aufgetreten. Schließen Sie das Audio-Tool und wiederholen Sie den Vorgang.", ex);
            }
        }
        
        private Base.CommandBase _onBtnThemeImport = null;
        public Base.CommandBase OnBtnThemeImport
        {
            get
            {
                if (_onBtnThemeImport == null)
                    _onBtnThemeImport = new Base.CommandBase(BtnThemeImport, null);
                return _onBtnThemeImport;
            }
        }
        void BtnThemeImport(object obj)
        {
            try
            {
                List<string> dateien = ViewHelper.ChooseFiles("Theme importieren", "", false, "xml");
                if (dateien != null)
                {
                    try
                    {
                        ThemeImportieren(dateien);

                        Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
                        Global.ContextAudio.Save();
                        Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
                        AktualisiereHotKeys();
                        UpdateAlleListen();

                        SelectedEditorThemeItem = EditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
                        Global.SetIsBusy(false);
                    }
                    catch (Exception ex)
                    {
                        Global.SetIsBusy(false);
                        ViewHelper.ShowError("Beim Import des Themes ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Importieren der Theme-Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }
        
        private Base.CommandBase _onThemeExportAll = null;
        public Base.CommandBase OnThemeExportAll
        {
            get
            {
                if (_onThemeExportAll == null)
                    _onThemeExportAll = new Base.CommandBase(ThemeExportAll, null);
                return _onThemeExportAll;
            }
        }
        void ThemeExportAll(object obj)
        {
            Global.ContextAudio.Save();
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                folderDlg.SelectedPath = Environment.CurrentDirectory;
                folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen." + Environment.NewLine;
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AlleThemesExportieren(folderDlg.SelectedPath);

                    ViewHelper.Popup("Der Export wurde erfolgreich beendet" + Environment.NewLine + "Alle Themelisten wurden folgednes Verzeichnis exportiert" +
                        Environment.NewLine + Environment.NewLine + folderDlg.SelectedPath + Environment.NewLine +
                        Environment.NewLine + "!!! Bitte beachten Sie, dass Die PLAYLISTEN SEPARAT gesichert werden müssen !!!");
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        private Base.CommandBase _onPlaylistenExportAll = null;
        public Base.CommandBase OnPlaylistenExportAll
        {
            get
            {
                if (_onPlaylistenExportAll == null)
                    _onPlaylistenExportAll = new Base.CommandBase(PlaylistenExportAll, null);
                return _onPlaylistenExportAll;
            }
        }
        void PlaylistenExportAll(object obj)
        {
            Global.ContextAudio.Save();
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                folderDlg.SelectedPath = Environment.CurrentDirectory;
                folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen";
                List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Global.SetIsBusy(true, string.Format("Export aller Playlisten wird vorbereitet..."));
                    string pfad = folderDlg.SelectedPath;

                    Audio_Playlist.Export(Global.ContextAudio.PlaylistListe, pfad);
                                        
                    AlleThemesExportieren(pfad);
                    ViewHelper.Popup("Der Export aller Audio-Daten wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
                        "Alle Audio-Playlisten und Themes wurden in folgendes Verzeichnis exportiert." + Environment.NewLine +
                        Environment.NewLine + Environment.NewLine + pfad + Environment.NewLine);
                    
                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Playlisten ist ein Fehler aufgetreten.", ex);
            }
        }

        private Base.CommandBase _onExportAllPlaylisten = null;
        public Base.CommandBase OnExportAllPlaylisten
        {
            get
            {
                if (_onExportAllPlaylisten == null)
                    _onExportAllPlaylisten = new Base.CommandBase(ExportAllPlaylisten, null);
                return _onExportAllPlaylisten;
            }
        }
        void ExportAllPlaylisten(object obj)
        {
            Global.ContextAudio.Save();
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                folderDlg.SelectedPath = Environment.CurrentDirectory;
                folderDlg.Description = "Wählen Sie ein Verzeichnis aus in das alle Dateien gespeichert werden sollen";
                List<Audio_Playlist> lstAPlayList = new List<Audio_Playlist>();
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Global.SetIsBusy(true, string.Format("Export aller Playlisten wird vorbereitet..."));
                    string pfad = folderDlg.SelectedPath;

                    Audio_Playlist.Export(Global.ContextAudio.PlaylistListe, pfad);

                    ViewHelper.Popup("Der Export wurde erfolgreich beendet." + Environment.NewLine + Environment.NewLine +
                        "Alle Playlisten wurden in folgendes Verzeichnis exportiert" + Environment.NewLine +
                        Environment.NewLine + Environment.NewLine + pfad + Environment.NewLine);

                    Global.SetIsBusy(false);
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Bei dem Speichern der kompletten Audio-Playlisten ist ein Fehler aufgetreten.", ex);
            }
        }
        
        private Base.CommandBase _onbtnKlangUpdateFiles = null;
        public Base.CommandBase OnbtnKlangUpdateFiles
        {
            get
            {
                if (_onbtnKlangUpdateFiles == null)
                    _onbtnKlangUpdateFiles = new Base.CommandBase(btnKlangUpdateFiles, null);
                return _onbtnKlangUpdateFiles;
            }
        }
        void btnKlangUpdateFiles(object obj)
        {            
            string titelRef = "";
            try
            {
                Global.SetIsBusy(true, string.Format("Neue Dateien werden integriert..."));
                List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);                
                List<string> allFiles = new List<string>();

                allFiles.AddRange(_chkAnzDateien.allFilesMP3);
                allFiles.AddRange(_chkAnzDateien.allFilesOGG);
                allFiles.AddRange(_chkAnzDateien.allFilesWAV);
                allFiles.AddRange(_chkAnzDateien.allFilesWMA);
                
                if (allFiles.Count > 0)
                {
                    if (ViewHelper.ConfirmYesNoCancel("Hinzufügen von Musiktitel aus dem Verzeichnis", "Es wurden insgesamt " + allFiles.Count +
                        " Dateien gefunden, die noch nicht in der Playliste eingetragen sind." + Environment.NewLine +
                        "Sollen diese integriert werden?") == 2)
                    {
                        Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine + titelRef + "werden eingebunden"));
                        
                        foreach (string newFile in allFiles)
                        {
                            Global.SetIsBusy(true, string.Format(allFiles.Count + " Titel im Verzeichnis: " + Environment.NewLine +
                                titelRef + "werden eingebunden" +
                                Environment.NewLine + System.IO.Path.GetFileName(newFile)));
                            KlangDateiHinzu(newFile, null, null, AktKlangPlaylist, 0);
                        }
                        SelectedEditorItem = SelectedEditorItem;
                    }
                }
                OnChanged("ChkAnzDateienResult");
                _chkAnzDateienInDir(AktKlangPlaylist);  
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {
                ChkAnzDateienResult = null;
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Ungültiger Pfad" + Environment.NewLine + "Bitte überprüfen Sie das Verzeichnis:" + Environment.NewLine + titelRef, ex);
            }
        }
        
        private Base.CommandBase _onbtnBGStoppen = null;
        public Base.CommandBase OnbtnBGStoppen 
        {
            get
            {
                if (_onbtnBGStoppen == null)
                    _onbtnBGStoppen = new Base.CommandBase(btnBGStoppen, null);
                return _onbtnBGStoppen;
            }
        }
        void btnBGStoppen(object obj)
        {
            if (!MusikAktiv.FadingOutStarted)
            {
                MusikAktivIsPaused = false;                
                if (MusikAktiv.aData != null && MusikAktiv.aData.isPlaying() && !MusikAktiv.FadingOutStarted)
                    BGFadingOut(MusikAktiv, true, true);
            }
            MusikProgBarTimer.Stop();
            MusikAktivIsPaused = true;
            MusikAktiv.aPlaylist = null;
            BGPlayer.Gespielt.Clear();
            BGPlayerGespieltCount = 0;
            RenewMusikNochZuSpielen();

            BGPlayer.NochZuSpielen.RemoveRange(0, BGPlayer.NochZuSpielen.Count);                     
            SelectedMusikTitelItem = null;     
        }
        
        private Base.CommandBase _onbtnBGApspielen = null;
        public Base.CommandBase OnbtnBGApspielen
        {
            get
            {
                if (_onbtnBGApspielen == null)
                    _onbtnBGApspielen = new Base.CommandBase(btnBGApspielen, null);
                return _onbtnBGApspielen;
            }
        }
        void btnBGApspielen(object obj)
        {
            if (MusikAktivIsPaused || MusikAktiv.aTitel == null )//MusikAktivIsPaused 
            {
                MusikAktivIsPaused = false;

                //Gepausten Titel wieder anstarten
                if (MusikAktiv != null && MusikAktiv.aData !=  null &&  MusikAktiv.aData.getFilename() != null) 
                {
                    if ((!MusikAktivIsPaused || SelectedMusikTitelItem == null) &&
                        MusikAktiv.aPlaylist != BGPlayer.AktPlaylist)
                        SelectedMusikTitelItem = GetNextMusikTitel();

                    MusikAktiv.aPlaylist = BGPlayer.AktPlaylist;
                    MusikAktiv.FadingOutStarted = false;

                    MusikAktivIsPaused = false;   
                    if (FadingIn_Started == null || FadingIn_Started != MusikAktiv.aData)
                    FadingIn(MusikAktiv, null,
                        MusikAktiv.aData, 
                        BGPlayer.AktPlaylistTitel != null? (double)Einstellungen.GeneralMusikVolume/100: 1);
                }
                else
                    //Neuen Titel starten
                    SpieleNeuenMusikTitel(Guid.Empty);
            }
            else
            {
                MusikAktivIsPaused = true;
                MusikAktiv.FadingOutStarted = true;
                BGFadingOut(MusikAktiv, false, true);
            }
        }
        
        private Base.CommandBase _onbtnBGPrev = null;
        public Base.CommandBase OnbtnBGPrev
        {
            get
            {
                if (_onbtnBGPrev == null)
                    _onbtnBGPrev = new Base.CommandBase(btnBGPrevClick, null);
                return _onbtnBGPrev;
            }
        }
        void btnBGPrevClick(object obj)
        {
            MusikAktivIsPaused = false;
            if (BGPlayer.Gespielt.Count == 0)
                SpieleNeuenMusikTitel(SelectedMusikTitelItem != null ? (Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID : Guid.Empty);
            else
            {
                BGPlayer.Gespielt.RemoveAll(t => t.Equals((Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID));
                Guid vorher = BGPlayer.Gespielt.Count >= 1?
                    BGPlayer.Gespielt.ElementAt(BGPlayer.Gespielt.Count - 1):
                    (Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID;
                BGPlayerGespieltCount = BGPlayer.Gespielt.Count;

                SelectedMusikTitelItem = FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == vorher);                
            }           
            OnChanged();
            OnChanged("BGPlayer");
            OnChanged("SelectedMusikTitelItem");
        }
        
        private Base.CommandBase _onbtnBGNext = null;
        public Base.CommandBase OnbtnBGNext
        {
            get
            {
                if (_onbtnBGNext == null)
                    _onbtnBGNext = new Base.CommandBase(btnBGNextClick, null);
                return _onbtnBGNext;
            }
        }
        void btnBGNextClick(object obj)
        {
            if (HintergrundMusikListe == null || HintergrundMusikListe.Count == 0) return;
            //MusikAktivIsPaused = false;
            
            Info_BGTitel = null;
            SelectedMusikTitelItem = GetNextMusikTitel();
            if (SelectedMusikTitelItem == null && (BGPlayer.AktPlaylist.Repeat == null || BGPlayer.AktPlaylist.Repeat.Value)) 
                RenewMusikNochZuSpielen();
            OnChanged("BGPlayer");
            OnChanged("SelectedMusikTitelItem");
        }
        
        private Base.CommandBase _onMusikStern1 = null;
        public Base.CommandBase OnMusikStern1
        {
            get
            {
                if (_onMusikStern1 == null)
                    _onMusikStern1 = new Base.CommandBase(MusikStern1Click, null);
                return _onMusikStern1;
            }
        }
        void MusikStern1Click(object obj)
        {
            MusikStern1 = MusikStern1 && !MusikStern2 ? false : true;
            MusikStern2 = false;
            MusikStern3 = false;
            MusikStern4 = false;
            MusikStern5 = false;
            RatingUpdate(1,BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern2 = null;
        public Base.CommandBase OnMusikStern2
        {
            get
            {
                if (_onMusikStern2 == null)
                    _onMusikStern2 = new Base.CommandBase(MusikStern2Click, null);
                return _onMusikStern2;
            }
        }        
        void MusikStern2Click(object obj)
        {
            MusikStern2 = MusikStern2 && !MusikStern3 ? false : true;
            MusikStern1 = MusikStern2;
            MusikStern3 = false;
            MusikStern4 = false;
            MusikStern5 = false;
            RatingUpdate(2, BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern3 = null;
        public Base.CommandBase OnMusikStern3
        {
            get
            {
                if (_onMusikStern3 == null)
                    _onMusikStern3 = new Base.CommandBase(MusikStern3Click, null);
                return _onMusikStern3;
            }
        }
        void MusikStern3Click(object obj)
        {
            MusikStern3 = MusikStern3 && !MusikStern4 ? false : true;
            MusikStern1 = MusikStern3;
            MusikStern2 = MusikStern3;
            MusikStern4 = false;
            MusikStern5 = false;
            RatingUpdate(3, BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern4 = null;
        public Base.CommandBase OnMusikStern4
        {
            get
            {
                if (_onMusikStern4 == null)
                    _onMusikStern4 = new Base.CommandBase(MusikStern4Click, null);
                return _onMusikStern4;
            }
        }
        void MusikStern4Click(object obj)
        {
            MusikStern4 = MusikStern4 && !MusikStern5 ? false : true;
            MusikStern1 = MusikStern4;
            MusikStern2 = MusikStern4;
            MusikStern3 = MusikStern4;
            MusikStern5 = false;
            RatingUpdate(4, BGPlayer.AktPlaylistTitel);
        }

        private Base.CommandBase _onMusikStern5 = null;
        public Base.CommandBase OnMusikStern5
        {
            get
            {
                if (_onMusikStern5 == null)
                    _onMusikStern5 = new Base.CommandBase(MusikStern5Click, null);
                return _onMusikStern5;
            }
        }
        void MusikStern5Click(object obj)
        {
            MusikStern5 = MusikStern5 ? false : true;
            MusikStern1 = MusikStern5;
            MusikStern2 = MusikStern5;
            MusikStern3 = MusikStern5;
            MusikStern4 = MusikStern5;
            RatingUpdate(5, BGPlayer.AktPlaylistTitel);
        }
        
        private Base.CommandBase _onAllHotkeysStop = null;
        public Base.CommandBase OnAllHotkeysStop
        {
            get
            {
                if (_onAllHotkeysStop == null)
                    _onAllHotkeysStop = new Base.CommandBase(AllHotkeysStop, null);
                return _onAllHotkeysStop;
            }
        }
        void AllHotkeysStop(object obj)
        {
            hotkeyListUsed.ForEach(delegate(btnHotkey hkey)
            {
                hkey.VM.TitelPlayList.FindAll(t => t.mp != null && t.mp.audioStream != 0).ForEach(delegate(btnHotkeyVM.TitelPlay titelPlay)
                { titelPlay.mp.Stop(); });
            });        
        }
        
        private Base.CommandBase _onAlleHotkeysEntfenrnen = null;
        public Base.CommandBase OnAlleHotkeysEntfenrnen
        {
            get
            {
                if (_onAlleHotkeysEntfenrnen == null)
                    _onAlleHotkeysEntfenrnen = new Base.CommandBase(AlleHotkeysEntfenrnen, null);
                return _onAlleHotkeysEntfenrnen;
            }
        }
        void AlleHotkeysEntfenrnen(object obj)
        {
            if (ViewHelper.ConfirmYesNoCancel("Löschen aller hotkeyListe", "Klicken Sie 'Ja' um alle hotkeyListe aus sämtlichen Plalyisten zu entfernen") == 2)
            {
                Global.ContextAudio.PlaylistListe.ForEach(delegate(Audio_Playlist aPlaylist) { aPlaylist.Key = null; });
                UpdateHotkeyUsed();
            }
        }
         
        private Base.CommandBase _onHotkeyEntfernen = null;
        public Base.CommandBase OnHotkeyEntfernen
        {
            get
            {
                if (_onHotkeyEntfernen == null)
                    _onHotkeyEntfernen = new Base.CommandBase(HotkeyEntfernen, null);
                return _onHotkeyEntfernen;
            }
        }
        void HotkeyEntfernen(object obj)
        {
            if (AktKlangPlaylist != null) 
                AktKlangPlaylist.Key = null;
            UpdateHotkeyUsed();
        }
        
        private Base.CommandBase _onHotkeyBtnDefine = null;
        public Base.CommandBase OnHotkeyBtnDefine
        {
            get
            {
                if (_onHotkeyBtnDefine == null)
                    _onHotkeyBtnDefine = new Base.CommandBase(HotkeyBtnDefine, null);
                return _onHotkeyBtnDefine;
            }
        }
        void HotkeyBtnDefine(object obj)
        {
            try
            {
                hotkeyListe.ForEach(delegate(btnHotkey hkey)
                {
                    bool found = false;
                    hotkeyListUsed.ForEach(delegate(btnHotkey hkeyUsed) {
                        if (hkey.VM.taste == hkeyUsed.VM.taste)
                            found = true;});
                    if (!found)
                        HotkeysAvailable.Add(Convert.ToChar(hkey.VM.taste).ToString());
                });
                IsAuswahlHotkey = true;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Setzen des Hotkey-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }
        
        private Base.CommandBase _geräuscheSpeakerMuting = null;
        public Base.CommandBase OnGeräuscheSpeakerMuting
        {
            get
            {
                if (_geräuscheSpeakerMuting == null)
                    _geräuscheSpeakerMuting = new Base.CommandBase(GeräuscheSpeakerMuting, null);
                return _geräuscheSpeakerMuting;
            }
        }
        void GeräuscheSpeakerMuting(object obj)
        {            
            GeräuscheIsMuted =! GeräuscheIsMuted;

            ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile)
            {                
                mZeile.VM.grpobj._listZeile.ForEach(delegate(KlangZeile klZeile)
                {
                    klZeile.aData.mute(GeräuscheIsMuted);
                });
            });

            //_GrpObjecte.ForEach(delegate(GruppenObjekt grpObj)
            //{
            //    grpObj._listZeile.FindAll(t => t.aData != null).ForEach(delegate(KlangZeile kZeile)
            //    {
            //        kZeile.aData.mute(GeräuscheIsMuted);
            //    });
            //});
        }
        
        private Base.CommandBase _bgSpeakerMuting = null;
        public Base.CommandBase OnBGSpeakerMuting
        {
            get
            {
                if (_bgSpeakerMuting == null)
                    _bgSpeakerMuting = new Base.CommandBase(BGSpeakerMuting, null);
                return _bgSpeakerMuting;
            }
        }
        void BGSpeakerMuting(object obj)
        {
            BGPlayer.isMuted =! BGPlayer.isMuted;
            BGPlayer.BG.ForEach(delegate(Musik m) { if (m.aData.audioStream != 0) m.aData.mute(BGPlayer.isMuted); }); // .mPlayer.IsMuted = BGPlayer.isMuted
            //if (BGPlayer.BG[0].audioStream != 0)
            //    BGPlayer.BG[0].mPlayer.IsMuted = BGPlayer.isMuted;
            //if (BGPlayer.BG[1].audioStream != 0)
            //    BGPlayer.BG[1].mPlayer.IsMuted = BGPlayer.BG[0].mPlayer.IsMuted;
            BGPlayerIsMuted = BGPlayer.isMuted;
            OnChanged("BGPlayer");
        }

        private Base.CommandBase _onSuchTextEditorAudioZeilenLöschen;
        public Base.CommandBase OnSuchTextEditorAudioZeilenLöschen
        {
            get
            {
                if (_onSuchTextEditorAudioZeilenLöschen == null)
                    _onSuchTextEditorAudioZeilenLöschen = new Base.CommandBase(OnSuchTextEditorAudioZeilenLöschenClick, null);
                return _onSuchTextEditorAudioZeilenLöschen;
            }
        }
        void OnSuchTextEditorAudioZeilenLöschenClick(object obj)
        {
            SuchTextEditorAudioZeilen = "";
        }

        private Base.CommandBase _onSuchTextEditorThemeLöschen;
        public Base.CommandBase OnSuchTextEditorThemeLöschen
        {
            get
            {
                if (_onSuchTextEditorThemeLöschen == null)
                    _onSuchTextEditorThemeLöschen = new Base.CommandBase(OnSuchTextEditorThemeLöschenClick, null);
                return _onSuchTextEditorThemeLöschen;
            }
        }
        void OnSuchTextEditorThemeLöschenClick(object obj)
        {
            SuchTextEditorTheme = "";
        }

        private Base.CommandBase _onSuchTextEditorPlaylistLöschen;
        public Base.CommandBase OnSuchTextEditorPlaylistLöschen
        {
            get
            {
                if (_onSuchTextEditorPlaylistLöschen == null)
                    _onSuchTextEditorPlaylistLöschen = new Base.CommandBase(OnSuchTextEditorPlaylistLöschenClick, null);
                return _onSuchTextEditorPlaylistLöschen;
            }
        }
        void OnSuchTextEditorPlaylistLöschenClick(object obj)
        {
            SuchTextEditor = "";
        }

        private Base.CommandBase _onSuchTextErwPlayerThemesLöschen;
        public Base.CommandBase OnSuchTextErwPlayerThemesLöschen
        {
            get
            {
                if (_onSuchTextErwPlayerThemesLöschen == null)
                    _onSuchTextErwPlayerThemesLöschen = new Base.CommandBase(OnSuchTextErwPlayerThemesLöschenClick, null);
                return _onSuchTextErwPlayerThemesLöschen;
            }
        }
        void OnSuchTextErwPlayerThemesLöschenClick(object obj)
        {
            SuchTextErwPlayerTheme = "";
        }

        private Base.CommandBase _onSuchTextMusikLöschen;
        public Base.CommandBase OnSuchTextMusikLöschen
        {
            get
            {
                if (_onSuchTextMusikLöschen == null)
                    _onSuchTextMusikLöschen = new Base.CommandBase(OnSuchTextMusikLöschenClick, null);
                return _onSuchTextMusikLöschen;
            }
        }
        void OnSuchTextMusikLöschenClick(object obj)
        {
            SuchTextMusik = "";
        }

        private Base.CommandBase _onSuchTextMusikTitelLöschen;
        public Base.CommandBase OnSuchTextMusikTitelLöschen
        {
            get
            {
                if (_onSuchTextMusikTitelLöschen == null)
                    _onSuchTextMusikTitelLöschen = new Base.CommandBase(OnSuchTextMusikTitelLöschenClick, null);
                return _onSuchTextMusikTitelLöschen;
            }
        }
        void OnSuchTextMusikTitelLöschenClick(object obj)
        {
            SuchTextMusikTitel = "";
        }
                
        private Base.CommandBase _onSuchTextErwPlayerMusikLöschen;
        public Base.CommandBase OnSuchTextErwPlayerMusikLöschen
        {
            get
            {
                if (_onSuchTextErwPlayerMusikLöschen == null)
                    _onSuchTextErwPlayerMusikLöschen = new Base.CommandBase(OnSuchTextErwPlayerMusikLöschenClick, null);
                return _onSuchTextErwPlayerMusikLöschen;
            }
        }
        void OnSuchTextErwPlayerMusikLöschenClick(object obj)
        {
            SuchTextMusik = "";
        }
        
        private Base.CommandBase _onSuchTextErwPlayerGeräuscheLöschen;
        public Base.CommandBase OnSuchTextErwPlayerGeräuscheLöschen
        {
            get
            {
                if (_onSuchTextErwPlayerGeräuscheLöschen == null)
                    _onSuchTextErwPlayerGeräuscheLöschen = new Base.CommandBase(OnSuchTextErwPlayerGeräuscheLöschenClick, null);
                return _onSuchTextErwPlayerGeräuscheLöschen;
            }
        }
        void OnSuchTextErwPlayerGeräuscheLöschenClick(object obj)
        {
            SuchTextErwPlayerGeräusche = "";
        }
        
        private Base.CommandBase _onThemeGeräuscheFilterNichtAktiv;
        public Base.CommandBase OnThemeGeräuscheFilterNichtAktiv
        {
            get
            {
                if (_onThemeGeräuscheFilterNichtAktiv == null)
                    _onThemeGeräuscheFilterNichtAktiv = new Base.CommandBase(OnThemeGeräuscheFilterNichtAktivClick, null);
                return _onThemeGeräuscheFilterNichtAktiv;
            }
        }
        void OnThemeGeräuscheFilterNichtAktivClick(object obj)
        {
            string s = SuchTextErwPlayerGeräusche;
            SuchTextErwPlayerGeräusche = "";
            SuchTextErwPlayerGeräusche = s;
            ThemeGeräuscheFilterAktiv = false;
        }
        
        private Base.CommandBase _onThemeGeräuscheFilterAktiv;
        public Base.CommandBase OnThemeGeräuscheFilterAktiv
        {
            get
            {
                if (_onThemeGeräuscheFilterAktiv == null)
                    _onThemeGeräuscheFilterAktiv = new Base.CommandBase(OnThemeGeräuscheFilterAktivClick, null);
                return _onThemeGeräuscheFilterAktiv;
            }
        }
        void OnThemeGeräuscheFilterAktivClick(object obj)
        {
            SuchTextErwPlayerGeräusche = "";

            FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.
                FindAll(s => s.tbtnCheck.IsChecked.Value).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            ThemeGeräuscheFilterAktiv = true;
        }


        private Base.CommandBase _onThemeGeräuscheAus;
        public Base.CommandBase OnThemeGeräuscheAus
        {
            get
            {
                if (_onThemeGeräuscheAus == null)
                    _onThemeGeräuscheAus = new Base.CommandBase(OnThemeGeräuscheAusClick, null);
                return _onThemeGeräuscheAus;
            }
        }
        void OnThemeGeräuscheAusClick(object obj)
        {
            ErwPlayerGeräuscheListItemListe.FindAll(s => s.tbtnCheck.IsChecked.Value).ForEach(delegate(MusikZeile mZeile) { 
                mZeile.tbtnCheck.IsChecked = false;
                mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                });
        }

        private Base.CommandBase _onBtnCloseTheme;
        public Base.CommandBase OnBtnCloseTheme
        {
            get
            {
                if (_onBtnCloseTheme == null)
                    _onBtnCloseTheme = new Base.CommandBase(BtnCloseTheme, null);
                return _onBtnCloseTheme;
            }
        }
        void BtnCloseTheme(object obj)
        {
            AktKlangTheme.Audio_Playlist.Remove(((obj as Button).Tag) as Audio_Playlist);
            Global.ContextAudio.Update<Audio_Theme>(AktKlangTheme);
        }
        
        private Base.CommandBase _onBtnErwPlayerPListAbspielen;
        public Base.CommandBase OnBtnErwPlayerPListAbspielen
        {
            get
            {
                if (_onBtnErwPlayerPListAbspielen == null)
                    _onBtnErwPlayerPListAbspielen = new Base.CommandBase(BtnErwPlayerPListAbspielen, null);
                return _onBtnErwPlayerPListAbspielen;
            }
        }            
        private void BtnErwPlayerPListAbspielen(object obj)
        {
            try
            {
                ErwPlayerGeräuscheLaufen = !ErwPlayerGeräuscheLaufen;
                foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                {
                    if (mZeile.VM.grpobj != null && mZeile.VM.grpobj.aPlaylist != null && mZeile.tbtnCheck.IsChecked.Value)
                    {
                        if (ErwPlayerGeräuscheLaufen)
                            mZeile.VM.tbtnCheckChecked(mZeile.tbtnCheck);
                        else
                        {
                            mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                            if (mZeile.VM.grpobj.aPlaylist.Fading)
                                FadingInGeräusch(mZeile.VM.grpobj);
                        }
                    }
                    if (mZeile.VM.grpobj != null)
                    {
                        mZeile.VM.grpobj.wirdAbgespielt = ErwPlayerGeräuscheLaufen;
                        mZeile.VM.grpobj.totalTimePlylist = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Fehler bei der Funktion btnPListPListAbspielen_Click", ex);
            }
        }
        
        private Base.CommandBase _onNeuePlaylist;
        public Base.CommandBase OnNeuePlaylist
        {
            get
            {
                if (_onNeuePlaylist == null)
                    _onNeuePlaylist = new Base.CommandBase(NeuePlaylist, null);
                return _onNeuePlaylist;
            }
        }
        void NeuePlaylist(object obj)
        {
            try
            {
                SuchTextEditor = "";
                OnChanged("SuchTextEditor");
                string NeuePlaylist = GetNeuenNamen("NeuePlayliste", 0);
               
                Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
                playlist.Name = NeuePlaylist.ToString();
                playlist.Hintergrundmusik = !LbEditorMitGeräusche? true: false;

                playlist.WarteZeitAktiv = true;
                playlist.WarteZeit = 500;
                playlist.WarteZeitChange = true;
                playlist.WarteZeitMin = 500;
                playlist.WarteZeitMax = 2500;

                //zur datenbank hinzufügen
                if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                {
                    playlist.MaxSongsParallel = 1;                    
                    EditorListBoxItemListe = lbiPlaylistListNeuErstellen();
                    FilterEditorPlaylistListe();
                    AktKlangPlaylist = playlist;
                }
                OnChanged("SelectedEditorItem");
                OnChanged();
                EditorListeVisible = true;
                SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
                
                //aktualisieren der ErwPlayer-Liste
                if (!playlist.Hintergrundmusik)
                    ErwPlayerGeräuscheListItemListe = mZeileErwPlayerGeräuscheNeuErstellen();
                else
                    ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Erstellen einer neuen Playlist ist ein Fehler aufgetreten.", ex);
            }
        }
        
        private Base.CommandBase _onNeuesTheme = null;
        public Base.CommandBase OnNeuesTheme
        {
            get
            {
                if (_onNeuesTheme == null)
                    _onNeuesTheme = new Base.CommandBase(NeuesTheme, null);
                return _onNeuesTheme;
            }
        }
        void NeuesTheme(object obj)
        { 
            Audio_Theme aktTh = NeuesKlangThemeInDB("");
            if (aktTh == null)
                return;
            
            //Nur die notwendigsten Listen neu laden
            EditorThemeListBoxItemListe = lbiThemeListNeuErstellen();
            ErwPlayerThemeListe = ThemeErwPlayerListeNeuErstellen();
            Refresh();
            FilterThemeEditorPlaylistListe();
            FilterErwPlayerThemeListe();

            AktKlangTheme = aktTh;
            OnChanged();
            SelectedEditorThemeItem = FilteredEditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
        }
                
        private Base.CommandBase _onTopKlangOpen = null;
        public Base.CommandBase OnTopKlangOpen
        {
            get
            {
                if (_onTopKlangOpen == null)
                    _onTopKlangOpen = new Base.CommandBase(TopKlangOpen, null);
                return _onTopKlangOpen;
            }
        }
        void TopKlangOpen(object obj)
        {
            try
            {
                string bezugsDir = stdPfad[0];
                if (AktKlangPlaylist != null)
                {
                    List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(AktKlangPlaylist);
                    if (titelliste.Count > 0)
                    {
                        if (AudioInAnderemPfadSuchen)
                        {
                            titelliste[0] = setTitelStdPfad(titelliste[0]);
                            setTitelStdPfad_AufrufeHintereinander = 0;
                        }

                            bezugsDir = (titelliste[0].Pfad + @"\" + titelliste[0].Datei).LastIndexOf(@"\") != -1 ?
                            (titelliste[0].Pfad + @"\" + titelliste[0].Datei).Substring(0, (titelliste[0].Pfad + @"\" + titelliste[0].Datei).LastIndexOf(@"\")) :
                            titelliste[0].Pfad + @"\" + titelliste[0].Datei;

                        titelliste.ForEach(delegate(Audio_Titel aTitel)
                        {
                            string vergleich = (aTitel.Pfad.Substring(1, 1) != ":") ?
                                System.IO.Path.GetDirectoryName(stdPfad[0] + @"\" + aTitel.Pfad + @"\" + aTitel.Datei) :
                                System.IO.Path.GetDirectoryName(aTitel.Pfad + @"\" + aTitel.Datei);

                            while (!vergleich.StartsWith(bezugsDir) &&
                                !bezugsDir.StartsWith(vergleich))
                            {
                                if (bezugsDir.Contains(@"\"))
                                    bezugsDir = bezugsDir.Substring(0, bezugsDir.LastIndexOf(@"\"));
                                else break;
                            }
                        });
                    }
                }
                string s = Environment.CurrentDirectory;
                if (Directory.Exists(bezugsDir))
                    Environment.CurrentDirectory = bezugsDir != "" ? bezugsDir : s;
                List<string> files = ViewHelper.ChooseFiles("Musiktitel auswählen", "", true, validExt);
                Environment.CurrentDirectory = s;

                // Öffnen bestätigt
                if (files != null && files.Count > 0)
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        _DateienAufnehmen(files, null, null, AktKlangPlaylist, 0, false);
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                        SelectedEditorItem = SelectedEditorItem;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Beim Einfügen der Datei(en) ist ein Fehler aufgetreten.", ex);
            }
        }
        
        private Base.CommandBase _oncbAllTitelChecked = null;
        public Base.CommandBase OncbAllTitelChecked
        {
            get
            {
                if (_oncbAllTitelChecked == null)
                    _oncbAllTitelChecked = new Base.CommandBase(cbAllTitelChecked, null);
                return _oncbAllTitelChecked;
            }
        }
        void cbAllTitelChecked(object obj)
        {
            bool ziel = !AllTitelAktiv;
            foreach (Audio_Playlist_Titel aPlaylistTitel in AktKlangPlaylist.Audio_Playlist_Titel)
                aPlaylistTitel.Aktiv = ziel;
            OnChanged("AllTitelAktiv");
        }
        
        private Base.CommandBase _onPlaylistImportieren = null;
        public Base.CommandBase OnPlaylistImportieren
        {
            get
            {
                if (_onPlaylistImportieren == null)
                    _onPlaylistImportieren = new Base.CommandBase(PlaylistImportieren, null);
                return _onPlaylistImportieren;
            }
        }
        void PlaylistImportieren(object obj)
        {
            try
            {
                List<string> dateien;
                dateien = ViewHelper.ChooseFiles("Playlist(en) importieren", "", true, new string[3] { "xml", "wpl", "m3u8" });
                if (dateien != null)
                {
                    PlaylistenImportieren(dateien);
                    BGStoppen();

                    EditorListBoxItemListe = lbiPlaylistListNeuErstellen();
                    MusikListItemListe = mZeileEditorMusikNeuErstellen();
                    ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
                    ErwPlayerGeräuscheListItemListe = mZeileErwPlayerGeräuscheNeuErstellen();
                    FilterEditorPlaylistListe();
                    FilterMusikPlaylistListe();
                    FilterErwPlayerMusikPlaylistListe();
                    FilterErwPlayerGeräuschePlaylistListe();
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Löschen der Datenbank ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
            }
        }
        
        void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            AktKlangTheme.Audio_Playlist.Remove(((Button)sender).Tag as Audio_Playlist);
            lbEditorThemeItemVM lbTheme = SelectedEditorThemeItem;
            SelectedEditorThemeItem = null;
            SelectedEditorThemeItem = lbTheme;
        }

        void btnRemoveTheme_Click(object sender, RoutedEventArgs e)
        {
            AktKlangTheme.Audio_Theme1.Remove(((Button)sender).Tag as Audio_Theme);
            lbEditorThemeItemVM lbTheme = SelectedEditorThemeItem;
            SelectedEditorThemeItem = null;
            SelectedEditorThemeItem = lbTheme;
        }

        #endregion

        #region //---- Textfilter ----

        /// <summary>
        /// Läd die AudioZeilen-Liste auf Basis der ausgewählten AktKlangPlaylist.
        /// </summary>
        public void LadeFilteredAudioZeilen()
        {
            string suchTextEditorAudioZeilen = _suchTextEditorAudioZeilen.ToLower().Trim();
            string[] suchWorte = suchTextEditorAudioZeilen.Split(' ');

            if (!TitellistAZ)
            {
                if (suchTextEditorAudioZeilen == string.Empty) // kein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.OrderBy(n => n.aPlayTitel.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.aPlayTitel.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.aPlayTitel.Reihenfolge).ToList();
            }
            else
            {
                //Sortierung nach Reihenfolge
                if (suchTextEditorAudioZeilen == string.Empty) // kein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.OrderBy(n => n.aPlayTitel.Audio_Titel.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.aPlayTitel.Audio_Titel.Name).ToList();
                else // mehrere Suchwörter
                    FilteredLbEditorAudioZeilenListe = LbEditorAudioZeilenListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.aPlayTitel.Audio_Titel.Name).ToList();
            }
        }


        /// <summary>
        /// Erstellt die Liste der noch nicht in dem AktTheme benutzten ThemesListBoxItem.
        /// </summary>
        private List<lbEditorThemeItemVM> FilterThemeÜbrigListBoxItemListe()
        {
            List<lbEditorThemeItemVM> lbiThemes = new List<lbEditorThemeItemVM>();            
            List<Guid> schonAktiveThemes = new List<Guid>();
            
            //Guid-Liste der schon verwendeten Themes erstellen
            if (AktKlangTheme != null)
                schonAktiveThemes.Add(AktKlangTheme.Audio_ThemeGUID);

            if (SelectedEditorThemeItem != null)
            {
                foreach (Audio_Theme aTheme in SelectedEditorThemeItem.ATheme.Audio_Theme2)
                {
                    schonAktiveThemes.Add(aTheme.Audio_ThemeGUID);
                    schonAktiveThemes = CheckUnterTheme(aTheme.Audio_ThemeGUID, schonAktiveThemes);
                }

                //Themes, die das Aktuelle Theme enthalten auch auf die Guid-Liste
                foreach (Audio_Theme aTheme in SelectedEditorThemeItem.ATheme.Audio_Theme1)
                {
                    schonAktiveThemes.Add(aTheme.Audio_ThemeGUID);
                    schonAktiveThemes = CheckUnterThemeInLbi(aTheme.Audio_ThemeGUID, schonAktiveThemes);
                }
            }

            foreach (lbEditorThemeItemVM lbi in EditorThemeListBoxItemListe)
            {
                if (!schonAktiveThemes.Contains(lbi.ATheme.Audio_ThemeGUID))
                    lbiThemes.Add(lbi);
            }

            return lbiThemes;
        }
            
        private List<Guid> CheckUnterTheme(Guid wpnlGuid, List<Guid> schonAktiveThemes)
        {
            foreach (Audio_Theme aUnterTheme in Global.ContextAudio.LoadThemesByGUID(wpnlGuid).Audio_Theme1)
            {
                if (!schonAktiveThemes.Contains(aUnterTheme.Audio_ThemeGUID))
                {
                    schonAktiveThemes.Add(aUnterTheme.Audio_ThemeGUID);
                    CheckUnterTheme(aUnterTheme.Audio_ThemeGUID, schonAktiveThemes);
                }
            }
            return schonAktiveThemes;
        }
        private List<Guid> CheckUnterThemeInLbi(Guid lbiGuid, List<Guid> schonAktiveThemes)
        {
            foreach (Audio_Theme aUnterTheme in Global.ContextAudio.LoadThemesByGUID(lbiGuid).Audio_Theme1)
            {
                if (aUnterTheme.Audio_ThemeGUID == AktKlangTheme.Audio_ThemeGUID &&
                    !schonAktiveThemes.Contains(lbiGuid))
                {
                    schonAktiveThemes.Add(lbiGuid);
                    CheckUnterTheme(lbiGuid, schonAktiveThemes);
                }
            }
            return schonAktiveThemes;
        }

        /// <summary>
        /// Filtert die EditorListBoxItem-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterThemeEditorPlaylistListe()
        {
            string suchTextEditorTheme = _suchTextEditorTheme.ToLower().Trim();
            string[] suchWorte = suchTextEditorTheme.Split(' ');

            if (suchTextEditorTheme == string.Empty) // kein Suchwort
                FilteredEditorThemeListBoxItemListe = EditorThemeListBoxItemListe.OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredEditorThemeListBoxItemListe = EditorThemeListBoxItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredEditorThemeListBoxItemListe = EditorThemeListBoxItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }
                        

        /// <summary>
        /// Filtert die EditorListBoxItem-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterEditorPlaylistListe()
        {
            FilteredEditorListBoxItemListe = EditorListBoxItemListe.AsParallel()
                .Where(t => (t.APlaylist.Hintergrundmusik == LbEditorMitMusik && LbEditorMitMusik == true) ||
                        (!t.APlaylist.Hintergrundmusik == LbEditorMitGeräusche && LbEditorMitGeräusche == true)).ToList();

            string suchTextEditor = _suchTextEditor.ToLower().Trim();
            string[] suchWorte = suchTextEditor.Split(' ');

            if (PlaylistAZ)
            {
                //Sortierung nach Alphabet
                if (suchTextEditor == string.Empty) // kein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.OrderBy(n => n.APlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.APlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.APlaylist.Name).ToList();
            }
            else
            {
                //Sortierung nach Reihenfolge
                if (suchTextEditor == string.Empty) // kein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.OrderBy(n => n.APlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.APlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredEditorListBoxItemListe = FilteredEditorListBoxItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.APlaylist.Reihenfolge).ToList();
            }

            if (!rbEditorEditPlaylist && AktKlangTheme != null)
            {
                foreach (Audio_Playlist aPlaylist in AktKlangTheme.Audio_Playlist)
                {
                    lbEditorItemVM lbi = FilteredEditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID);
                    if (lbi != null)
                        FilteredEditorListBoxItemListe.Remove(lbi);
                }
            }
        }
        
        /// <summary>
        /// Filtert die MusikListBoxItem-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterMusikPlaylistListe()
        {
            if (MusikListItemListe == null)
                return;
            string[] suchWorte = _suchTextMusik.Split(' ');
            if (MusikAZ)
            {
                if (_suchTextMusik == string.Empty) // kein Suchwort
                    FilteredMusikPlaylistItemListe = MusikListItemListe.OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredMusikPlaylistItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredMusikPlaylistItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            }
            else
            {
                if (_suchTextMusik == string.Empty) // kein Suchwort
                    FilteredMusikPlaylistItemListe = MusikListItemListe.OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredMusikPlaylistItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredMusikPlaylistItemListe = MusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
            }
        }

        /// <summary>
        /// Filtert die MusikTitel-Liste auf Basis des SuchTextes.
        /// </summary>
        private void FilterMusikTitelListe()
        {
            string[] suchWorte = _suchTextMusikTitel.ToLower().Split(' ');
            if (HintergrundMusikListe == null || HintergrundMusikListe.Count == 0) return;
            HintergrundMusikListe.ForEach(delegate(ListBoxItem lbi) { lbi.Visibility = Visibility.Visible; });
            FilteredHintergrundMusikListe = MusikTitelAZ ?
                HintergrundMusikListe.OrderBy(n => ((Audio_Playlist_Titel)n.Tag).Audio_Titel.Name).ToList():
                HintergrundMusikListe.OrderBy(t => ((Audio_Playlist_Titel)t.Tag).Reihenfolge).ToList(); 
            
            if (suchWorte.Length == 1) // nur ein Suchwort                
            {
                FilteredHintergrundMusikListe.FindAll(t => !t.Content.ToString().ToLower().Contains(suchWorte[0])).ForEach(delegate(ListBoxItem lbi)
                { lbi.Visibility = Visibility.Collapsed;});
            }
            else
            {
                FilteredHintergrundMusikListe.FindAll(t => !TextContains(t.Content.ToString().ToLower(), suchWorte)).ForEach(delegate(ListBoxItem lbi)
                { lbi.Visibility = Visibility.Collapsed;});
            }
        }

        private bool TextContains(string suchwort, string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (suchwort != wort)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Filtert die Geräusche im Erw.Player ListBoxItem-Liste auf Basis des SuchTextes und der Theme-SuchTextes
        /// </summary>
        public void FilterErwPlayerGeräuschePlaylistListe()
        {
            if (ErwPlayerGeräuscheListItemListe == null)
                return;
            string st = (_suchTextErwPlayerGeräusche + _suchTextErwPlayerTheme != "") ? _suchTextErwPlayerGeräusche + ' ' + _suchTextErwPlayerTheme : string.Empty;
            string[] suchWorte = st.Split(' ');
            if (GeräuscheAZ)
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerGeräuscheListItemListe =
                        ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            }
            else
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerGeräuscheListItemListe =
                        ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerGeräuscheListItemListe = ErwPlayerGeräuscheListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
            }
        }

        /// <summary>
        /// Filtert die Musik im Erw.Player ListBoxItem-Liste auf Basis des SuchTextes und der Theme-SuchTextes.
        /// </summary>
        public void FilterErwPlayerMusikPlaylistListe()
        {
            if (ErwPlayerMusikListItemListe == null)
                return;
            string st = (_suchTextMusik + _suchTextErwPlayerTheme != "")? _suchTextMusik + ' ' + _suchTextErwPlayerTheme : string.Empty;
            string[] suchWorte = st.Split(' ');

            if (MusikAZ)
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Name).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Name).ToList();
            }
            else
            {
                if (st == string.Empty) // kein Suchwort 
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else if (suchWorte.Length == 1) // nur ein Suchwort                
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
                else // mehrere Suchwörter
                    FilteredErwPlayerMusikListItemListe = ErwPlayerMusikListItemListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.aPlaylist.Reihenfolge).ToList();
            }
        }

        /// <summary>
        /// Filtert die Themes im Erw.Player grdThemeButton-Liste auf Basis des SuchTextes.
        /// </summary>
        public void FilterErwPlayerThemeListe()
        {
            string[] suchWorte = _suchTextErwPlayerTheme.ToLower().Split(' ');

            if (_suchTextErwPlayerTheme == string.Empty) // kein Suchwort
                FilteredErwPlayerThemeListe = ErwPlayerThemeListe.OrderBy(n => n.VM.Theme.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort                
                FilteredErwPlayerThemeListe =
                    ErwPlayerThemeListe.FindAll(s => s.Contains(suchWorte[0])).OrderBy(n => n.VM.Theme.Name).ToList();
            else // mehrere Suchwörter
                FilteredErwPlayerThemeListe = ErwPlayerThemeListe.FindAll(s => s.Contains(suchWorte)).OrderBy(n => n.VM.Theme.Name).ToList();
        }


        private string _suchTextEditorAudioZeilen = string.Empty;
        public string SuchTextEditorAudioZeilen
        {
            get { return _suchTextEditorAudioZeilen; }
            set
            {
                Set(ref _suchTextEditorAudioZeilen, value);
                LadeFilteredAudioZeilen();
            }
        }

        private string _suchTextEditorTheme = string.Empty;
        public string SuchTextEditorTheme
        {
            get { return _suchTextEditorTheme; }
            set
            {
                Set(ref _suchTextEditorTheme, value);
                FilterThemeEditorPlaylistListe();
            }
        }

        private string _suchTextEditor = string.Empty;
        public string SuchTextEditor
        {
            get { return _suchTextEditor; }
            set
            {
                Set(ref _suchTextEditor, value);
                FilterEditorPlaylistListe();
            }
        }
                
        private string _suchTextErwPlayerGeräusche = string.Empty;
        public string SuchTextErwPlayerGeräusche
        {
            get { return _suchTextErwPlayerGeräusche; }
            set
            {
                Set(ref _suchTextErwPlayerGeräusche, value);
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        private string _suchTextErwPlayerTheme = string.Empty;
        public string SuchTextErwPlayerTheme
        {
            get { return _suchTextErwPlayerTheme; }
            set
            {
                Set(ref _suchTextErwPlayerTheme, value);
                FilterErwPlayerThemeListe();
                FilterErwPlayerMusikPlaylistListe();
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        private string _suchTextMusik = string.Empty;
        public string SuchTextMusik
        {
            get { return _suchTextMusik; }
            set
            {
                Set(ref _suchTextMusik, value);
                FilterMusikPlaylistListe();
                FilterErwPlayerMusikPlaylistListe();
            }
        }
        private string _suchTextMusikTitel = string.Empty;
        public string SuchTextMusikTitel
        {
            get { return _suchTextMusikTitel; }
            set
            {
                Set(ref _suchTextMusikTitel, value);
                FilterMusikTitelListe();
            }
        }



        public int PlaylistWarteZeitMinDecValue
        {
            get
            {
                return (AktKlangPlaylist.WarteZeitMin > 10000) ? 5000 :
                       (AktKlangPlaylist.WarteZeitMin > 2000) ? 1000 : 200;
            }
        }

        public int PlaylistWarteZeitMinIncValue
        {
            get
            {
                return (AktKlangPlaylist.WarteZeitMin >= 10000) ? 5000 :
                         (AktKlangPlaylist.WarteZeitMin >= 2000) ? 1000 : 200;
            }
        }

        public int PlaylistWarteZeitMaxDecValue
        {
            get
            {
                return (AktKlangPlaylist.WarteZeitMax > 10000) ? 5000 :
                       (AktKlangPlaylist.WarteZeitMax > 2000) ? 1000 : 200;
            }
        }

        public int PlaylistWarteZeitMaxIncValue
        {
            get
            {
                return (AktKlangPlaylist.WarteZeitMax >= 10000) ? 5000 :
                     (AktKlangPlaylist.WarteZeitMax >= 2000) ? 1000 : 200;
            }
        }

        public string AktKlangPlaylistWarteZeitTooltip
        {
            get
            {
                return (AktKlangPlaylistWarteZeit < 1000 ? AktKlangPlaylistWarteZeit + " ms" : AktKlangPlaylistWarteZeit < 60000 ?
                    Math.Round((double)AktKlangPlaylistWarteZeit / 1000, 2).ToString() + " sek." :
                    AktKlangPlaylistWarteZeit / 60000 + " min.");
            }
        }

        public long AktKlangPlaylistWarteZeit
        {
            get { return AktKlangPlaylist.WarteZeit; }
            set
            {
                AktKlangPlaylist.WarteZeit = value;
                OnChanged(nameof(AktKlangPlaylistWarteZeit));
                OnChanged("AktKlangPlaylistWarteZeitToolTip");
            }
        }

        public long AktKlangPlaylistWarteZeitMin
        {
            get
            {
                OnChanged("PlaylistWarteZeitMinIncValue");
                OnChanged("PlaylistWarteZeitMinDecValue");
                return AktKlangPlaylist.WarteZeitMin;
            }
            set
            {
                AktKlangPlaylist.WarteZeitMin = value;
                if (AktKlangPlaylistWarteZeitMax < value)
                    AktKlangPlaylistWarteZeitMax = value;
                OnChanged(nameof(AktKlangPlaylistWarteZeitMin));
            }
        }

        public long AktKlangPlaylistWarteZeitMax
        {
            get
            {
                OnChanged("PlaylistWarteZeitMaxIncValue");
                OnChanged("PlaylistWarteZeitMaxDecValue");
                return AktKlangPlaylist.WarteZeitMax;
            }
            set
            {
                AktKlangPlaylist.WarteZeitMax = value;
                if (AktKlangPlaylistWarteZeitMin > value)
                    AktKlangPlaylistWarteZeitMin = value;
                OnChanged(nameof(AktKlangPlaylistWarteZeitMax));
            }
        }

        public string TextPlaylistWarteZeitMax
        {
            get { return GetZeitText(AktKlangPlaylistWarteZeitMax) + Environment.NewLine + "Maximale Pausenzeit bevor Playlist gespielt wird"; }
        }
        public string TextPlaylistWarteZeitMin
        {
            get { return GetZeitText(AktKlangPlaylistWarteZeitMin) + Environment.NewLine + "Minimale Pausenzeit bevor Playlist gespielt wird"; }
        }

        public string GetZeitText(long zeit)
        {
            return (zeit < 1000 ? zeit + " ms" : zeit < 60000 ?
                    Math.Round((double)zeit / 1000, 2).ToString() + " sek." :
                    zeit / 60000 + " min.");
        }


        private string _neuerPlaylistName = "NeuePlayliste";
        public string NeuerPlaylistName
        {
            get { return _neuerPlaylistName; }
            set
            {
                _neuerPlaylistName = GetNeuenNamen("NeuePlayliste", 0);
                OnChanged(nameof(NeuerPlaylistName));
            }
        }

        private bool _playlistAZ = false;
        public bool PlaylistAZ
        {
            get { return _playlistAZ; }
            set
            {
                Set(ref _playlistAZ, value);
                FilterEditorPlaylistListe();
            }
        }

        private bool _titellistAZ = false;
        public bool TitellistAZ
        {
            get { return _titellistAZ; }
            set
            {
                Set(ref _titellistAZ, value);
                LadeFilteredAudioZeilen();
            }
        }
        private bool _musikNacheinander = false;
        public bool MusikNacheinander
        {
            get { return _musikNacheinander; }
            set { Set(ref _musikNacheinander, value); }
        }

        private bool _musikAZ = false;
        public bool MusikAZ
        {
            get { return _musikAZ; }
            set
            {
                Set(ref _musikAZ, value);
                FilterMusikPlaylistListe();
                FilterErwPlayerMusikPlaylistListe();
            }
        }

        private bool _geräuscheAZ = false;
        public bool GeräuscheAZ
        {
            get { return _geräuscheAZ; }
            set
            {
                Set(ref _geräuscheAZ, value);
                FilterErwPlayerGeräuschePlaylistListe();
            }
        }

        private bool _musikTitelAZ = false;
        public bool MusikTitelAZ
        {
            get { return _musikTitelAZ; }
            set
            {
                Set(ref _musikTitelAZ, value);
                FilterMusikTitelListe();
            }
        }


        private ObservableCollection<TitelInfo> _titelListe = new ObservableCollection<TitelInfo>();
        /// <summary>
        /// Titel der aktuellen Playlist.
        /// </summary>
        public ObservableCollection<TitelInfo> TitelListe
        {
            get { return _titelListe; }
            set { Set(ref _titelListe, value); }
        }

        private Audio_Playlist _currentPlaylist;
        public Audio_Playlist CurrentPlaylist
        {
            get { return _currentPlaylist; }
            set
            {
                Set(ref _currentPlaylist, value);
                TitelListe.Clear();
                if (value != null)
                    value.Audio_Playlist_Titel.Select(pt => new TitelInfo(pt)).ToList().ForEach(ti => TitelListe.Add(ti));
                OnChanged("TitelListe");
            }
        }


        private bool _geräuscheIsMuted = false;
        public bool GeräuscheIsMuted
        {
            get { return _geräuscheIsMuted; }
            set { Set(ref _geräuscheIsMuted, value); }
        }

        private bool _bgPlayerIsMuted = false;
        public bool BGPlayerIsMuted
        {
            get { return _bgPlayerIsMuted; }
            set { Set(ref _bgPlayerIsMuted, value); }
        }

        private double _bgPlayerVolume = (double)Einstellungen.GeneralMusikVolume;
        public double BGPlayerVolume
        {
            get { return _bgPlayerVolume; }
            set
            {
                Set(ref _bgPlayerVolume, value);
                Einstellungen.GeneralMusikVolume = (int)Math.Round(value,0);

                if (!MusikAktiv.aData.muted && MusikAktiv.aData.isPlaying())
                    MusikAktiv.aData.setVolume((value) / 100);
            }
        }

        #endregion

        #region //---- Konvertierungen ----

        private static String ConvertByteToString(byte[] bytes, int pos1, int pos2)
        {
            if ((pos1 > pos2) || (pos2 > bytes.Length - 1))
                throw new ArgumentException("Aruments of range");

            int length = pos2 - pos1 + 1;
            Char[] chars = new Char[length];

            for (int i = 0; i < length; i++)
                chars[i] = Convert.ToChar(bytes[i + pos1]);
            String s = new String(chars);
            s = s.Replace("\0", "");

            return s;
        }

        #endregion
        
        #region //---- Fading ----

        public void BGFadingOut(Musik BG, bool playerStoppen, bool sofort)
        {
            try
            {
                DispatcherTimer dtimer = new DispatcherTimer();
                dtimer.Tick += new EventHandler(_timerBGFadingOut_Tick);

                _listMusikFadingOut.Add(dtimer);
                
                FadingOut_Started = BG.aData;

                dtimer.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

                Fading fadInfo = new Fading();
                fadInfo.BG = BG;
                fadInfo.aData = BG.aData;
                fadInfo.Start = DateTime.MinValue;
                fadInfo.startVol = BG.aData.getVolume();
                fadInfo.lookupVol = BGPlayerVolume;
                fadInfo.fadingOutSofort = sofort;
                fadInfo.mPlayerStoppen = playerStoppen;
                if (BG.aData.isPlaying() && BG.aData == FadingIn_Started)
                {
                    fadInfo.startVol = FadingIn_Started.getVolume();
                    fadInfo.fadingInStartet = fadInInfo.fadingInStartet;
                }
                else
                    fadInfo.startVol = BG.aData.getVolume();

                fadInfo.fadingTime = (BG.aData.getLength() * 1/3 < fadingTime * 10) ? BG.aData.getLength() * 1 / 30 : fadingTime;

                dtimer.Tag = fadInfo;
                dtimer.Start();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("BGFading Out Exeption" + Environment.NewLine + "Fehler beim Fading-Out ist ein Fehler aufgetreten.", ex);
            }
        }

        public void _timerBGFadingOut_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)((DispatcherTimer)sender).Tag; 
            
            if (fadInfo.Start == DateTime.MinValue)
            {
                fadInfo.Start = DateTime.Now;
                fadInfo.fadingOutStartet = fadInfo.Start;            
            }

            if (fadInfo.fadingInStartet != DateTime.MinValue) 
            {                
                //Fading Out abbrechen weil FadingIn des Titel aktiviert
                if (fadInfo.fadingOutStartet < fadInfo.fadingInStartet)
                {
                    fadInfo.BG.FadingOutStarted = false;
                    FadingOut_Started = null;
                    ((DispatcherTimer)sender).Stop();
                    _listMusikFadingOut.Remove(((DispatcherTimer)sender));
                    return;
                }
            }      
            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            if (!fadInfo.fadingOutSofort && _vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                fadInfo.fadingOutSofort = true;

            if (FadingIn_Started != null || fadInfo.fadingOutSofort) //&& FadingIn_Started.Source != null
            {
                if (!fadInfo.fadingOutSofort)
                {
                    fadInfo.Start = DateTime.Now;
                    _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;
                }
                fadInfo.fadingOutSofort = true;

                //solange Volume runterregeln bis der Titel extern das Fadingstoppt

                if (fadInfo.aData != null)
                {
                    double aktVol = fadInfo.Offset/100 +
                        (fadInfo.fadingTime != 0? fadInfo.startVol - fadInfo.startVol * (_vergangeneZeit / (fadInfo.fadingTime * fadingIntervall)) : 0);
                    
                    aktVol = aktVol < 0 ? 0 : aktVol;

                    if (fadInfo.lookupVol != BGPlayerVolume)        // wenn während des Fading die Lautstärke verändert wird
                    {
                        fadInfo.Offset = BGPlayerVolume - fadInfo.lookupVol;
                        fadInfo.lookupVol = BGPlayerVolume;
                    }

                    if (!fadInfo.aData.muted && fadInfo.aData.getVolume() != aktVol)
                        fadInfo.aData.setVolume(aktVol);

                    if (aktVol == 0)
                    {
                        if (fadInfo.mPlayerStoppen)// && fadInfo.BG.FadingOutStarted)   //bei Volume 0 Fadingauf false und Song freigeben
                        {
                            //setNewLbFading(fadInfo.BG.mPlayer, "Backgroundmusik gestoppt");
                            fadInfo.aData.Stop();
                            Player_Ended(fadInfo.aData, null);
                            fadInfo.aData.Close();

                            //MusikAktiv-Daten Löschen aus dem Speicher
                            if (MusikAktiv == BGPlayer.BG.FirstOrDefault(t => t.aData == fadInfo.aData))
                                MusikAktiv = new Musik();
                            BGPlayer.BG.Remove(BGPlayer.BG.FirstOrDefault(t => t.aData == fadInfo.aData));
                        }
                        if (!fadInfo.mPlayerStoppen && //fadInfo.BG.FadingOutStarted &&
                            fadInfo.BG.aPlaylist != null && fadInfo.BG.aPlaylist.Name == BGPlayerAktPlaylist.Name)
                        {
                            fadInfo.aData.Pause();
                            fadInfo.BG.isPaused = true;
                        }
                        fadInfo.BG.FadingOutStarted = false;
                        FadingOut_Started = null;
                        ((DispatcherTimer)sender).Stop();
                        _listMusikFadingOut.Remove(((DispatcherTimer)sender));
                    }                    
                }
            }
        }

        public Fading fadInInfo = new Fading();
        public void FadingIn(Musik BG, KlangZeile klZeile, AudioData aData, double zielVol)
        {
            if (_timerFadingIn.IsEnabled && 
                (klZeile != null || BG != null) && 
                ((Fading)_timerFadingIn.Tag).aData != aData)              //Anderer Fading-In am laufen -> Abbrechen
            {
                _timerFadingIn.Stop();
                ((Fading)_timerFadingIn.Tag).aData.Pause();
                ((Fading)_timerFadingIn.Tag).aData.setPosition(0);
                if (((Fading)_timerFadingIn.Tag).klZeile != null)
                    ((Fading)_timerFadingIn.Tag).klZeile.istPause = true;
            }
            FadingIn_Started = aData;

            _timerFadingIn.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

            if (BG != null) fadInInfo.BG = BG;
            if (klZeile != null) fadInInfo.klZeile = klZeile;

            fadInInfo.fadingOutStartet = DateTime.MinValue;
            fadInInfo.fadingInStartet = DateTime.MinValue;
            fadInInfo.aData = aData;
            fadInInfo.Start = DateTime.MinValue;
            fadInInfo.zielVol = zielVol;
            fadInInfo.startVol = 0;
            fadInInfo.lookupVol = BGPlayerVolume;

            DispatcherTimer fadOut = _listMusikFadingOut.FirstOrDefault(t => ((Fading)t.Tag).aData == aData);
            if (fadOut != null)
            {
                fadInInfo.startVol = aData.getVolume();
                if (fadOut.IsEnabled)                       
                    fadInInfo.fadingOutStartet = ((Fading)fadOut.Tag).fadingOutStartet;
            }
            else
            {
                aData.setVolume(0);
            }                   
            fadInInfo.fadingTime = (aData.getLength() * 1 / 3 < fadingTime * 10) ? aData.getLength() * 1 / 30 : fadingTime;              // Fading auf 1/3 der Liedlänge kürzen

            if (!(fadOut != null && fadOut.IsEnabled))
                aData.Play();

            _timerFadingIn.Tag = fadInInfo;
            _timerFadingIn.Start();
        }
        
        public void _timerFadingIn_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)_timerFadingIn.Tag;
            
            if (fadInfo.Start == DateTime.MinValue)
            {
                fadInfo.Start = DateTime.Now;
                fadInfo.fadingInStartet = fadInfo.Start;
            }

            //Fading In abbrechen weil FadingOut des Titels aktiviert
            if (fadInfo.fadingOutStartet != DateTime.MinValue)
            {
                DispatcherTimer fadOut = _listMusikFadingOut.FirstOrDefault(t => ((Fading)t.Tag).aData == fadInfo.aData);
                if (fadOut != null)
                    ((Fading)fadOut.Tag).fadingInStartet = fadInfo.fadingInStartet;

                if (fadInfo.fadingInStartet < fadInfo.fadingOutStartet)
                {
                    FadingIn_Started = null;
                    _timerFadingIn.Stop();
                    return;
                }
            }
 
            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            double aktVol = fadInfo.Offset/100 + 
                (fadInfo.fadingTime != 0? fadInfo.zielVol * ((_vergangeneZeit / (fadInInfo.fadingTime * fadingIntervall)) + fadInfo.startVol) : fadInfo.zielVol);

            if (fadInfo.lookupVol != BGPlayerVolume)
            {
                fadInfo.Offset = BGPlayerVolume - fadInfo.lookupVol;
                fadInfo.lookupVol = BGPlayerVolume;
                fadInfo.zielVol += fadInfo.Offset/100;
            }

            if (FadingIn_Started != fadInfo.aData)
                stopFadingIn = true;

            if (fadInfo.aData != null && !fadInfo.aData.muted)
                fadInfo.aData.setVolume(aktVol);

            if (stopFadingIn || fadInfo.aData.getVolume() >= fadInfo.zielVol)
            {
                if (fadInfo.aData.getVolume() >= fadInfo.zielVol && FadingIn_Started == fadInfo.aData)
                {
                    FadingIn_Started = null;
                    _timerFadingIn.Stop();
                }
                stopFadingIn = false;
            }
        }
        
        public void FadingOut(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile, GruppenObjekt grpobj, bool playerStoppen, bool sofort)
        {
            try
            {
                if (_timerFadingOut.IsEnabled && ((Fading)_timerFadingOut.Tag).aData != klZeile.aData)              //Anderer Fading-Out am laufen -> Abbrechen
                {
                    _timerFadingOut.Stop();
                    ((Fading)_timerFadingOut.Tag).aData.Pause();
                    ((Fading)_timerFadingOut.Tag).aData.setPosition(0);
                    if (((Fading)_timerFadingOut.Tag).klZeile != null)
                        ((Fading)_timerFadingOut.Tag).klZeile.istPause = true;
                }

                if (_timerFadingIn.IsEnabled && ((Fading)_timerFadingIn.Tag).aData == klZeile.aData)          // Titel ist Fading-In --> Fading-In abbrechen
                {
                    _timerFadingIn.Stop();
                    FadingIn_Started = null;
                }
                FadingOut_Started = klZeile.aData;

                _timerFadingOut.Interval = TimeSpan.FromMilliseconds(fadingIntervall);

                Fading fadInfo = new Fading();
                fadInfo.klZeile = klZeile;
                fadInfo.aData = klZeile.aData;
                fadInfo.Start = DateTime.MinValue;
                fadInfo.startVol = klZeile.aData.getVolume();
                fadInfo.fadingOutSofort = sofort;
                fadInfo.mPlayerStoppen = playerStoppen;

                fadInfo.lookupVol = klZeile.aPlaylistTitel.Audio_Playlist.Hintergrundmusik ? BGPlayerVolume : klZeile.aData.getVolume();
                if (!klZeile.aPlaylistTitel.Audio_Playlist.Hintergrundmusik)
                    fadInfo.lookupVol = klZeile.aData.getVolume();
                
                fadInfo.fadingTime = (fadInfo.aData.getLength() * 1 / 3 < fadingTime * 10) ? fadInfo.aData.getLength() * 1 / 30 : fadingTime;               // Fading auf 1/3 der Liedlänge kürzen

                _timerFadingOut.Tag = fadInfo;
                _timerFadingOut.Start();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Fading Out Exeption" + Environment.NewLine + "Fehler beim Fading-Out ist ein Fehler aufgetreten.", ex);
            }
        }
        
        public void FadingInGeräusch(GruppenObjekt grpobj)
        {
            group newgroup = new group();
            newgroup.grpobj = grpobj;    //force_Volume == null 
            newgroup.zielProzent = !grpobj.DoForceVolume ? FadingGeräuscheVolProzent : grpobj.force_Volume == null? 0: (double)grpobj.force_Volume * 100;
            newgroup.startProzent = grpobj.Vol_PlaylistMod != FadingGeräuscheVolProzent ? grpobj.Vol_PlaylistMod : 0;
            newgroup.lookupVol = newgroup.startProzent;
            newgroup.StartZeit = DateTime.MinValue;
            newgroup.mZeile = ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => (Guid)t.Tag == grpobj.aPlaylist.Audio_PlaylistGUID);
           
            if (!_timerFadingInGeräusche.IsEnabled)
            {
                FadingInGeräusche _fadingInGeräusche = new FadingInGeräusche();
                _fadingInGeräusche.gruppenIn.Clear();
                _fadingInGeräusche.gruppenIn.Add(newgroup);

                _timerFadingInGeräusche.Tag = _fadingInGeräusche;
                _timerFadingInGeräusche.Start();
            }
            else
            {
                ((FadingInGeräusche)_timerFadingInGeräusche.Tag).gruppenIn.Add(newgroup);
            }
        }

        public void _timerFadingInGeräusche_Tick(object sender, EventArgs e)
        {
            FadingInGeräusche fadInfo = (FadingInGeräusche)_timerFadingInGeräusche.Tag;

            List<group> grpToDelete = new List<group>();

            foreach (group gruppe in fadInfo.gruppenIn)
            {
                if (gruppe.StartZeit == DateTime.MinValue)
                    gruppe.StartZeit = DateTime.Now;

                gruppe._vergangeneZeit = DateTime.Now.Subtract(gruppe.StartZeit).TotalMilliseconds;

                if (!gruppe.grpobj.wirdAbgespielt)
                {
                    grpToDelete.Add(gruppe);
                    continue;
                }

                //Aktualisiere zielProzent im Fall, dass User den Wert geändert hat
                //if (gruppe.zielProzent != 
                //    (!gruppe.grpobj.DoForceVolume ? FadingGeräuscheVolProzent : 
                //    gruppe.grpobj.force_Volume == null? 0: (double)gruppe.grpobj.force_Volume * 100))
                //    gruppe.zielProzent = !gruppe.grpobj.DoForceVolume ? FadingGeräuscheVolProzent : 
                //        gruppe.grpobj.force_Volume == null? 0: (double)gruppe.grpobj.force_Volume * 100;

                //if (gruppe.grpobj.force_Volume == null && gruppe.zielProzent != slPlaylistVolume.Value)
                //    gruppe.zielProzent = slPlaylistVolume.Value;
                gruppe.grpobj.Vol_PlaylistMod = (gruppe.grpobj.Vol_PlaylistMod != gruppe.zielProzent) ? (gruppe.zielProzent) * (gruppe._vergangeneZeit / (fadingTime * fadingIntervall)) + gruppe.startProzent : gruppe.zielProzent;
                gruppe.grpobj.Vol_PlaylistMod = gruppe.grpobj.Vol_PlaylistMod > gruppe.zielProzent ? gruppe.zielProzent : gruppe.grpobj.Vol_PlaylistMod;// _volProzentModFadingIn;
               // double l = 0;

                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in
                    gruppe.grpobj._listZeile.FindAll(t => t.aData != null))
                {
                    if (!kZeile.aData.muted)
                        kZeile.aData.setVolume(!gruppe.grpobj.DoForceVolume ?
                            (kZeile.Aktuell_Volume / 100) * (gruppe.grpobj.Vol_PlaylistMod / 100) :
                            gruppe.grpobj.Vol_PlaylistMod / 100);
                  //  l = kZeile.aData.getVolume();
                }
                
                if (gruppe.mZeile != null)
                    gruppe.mZeile.VM.FadingPercentage = Math.Round(gruppe.grpobj.Vol_PlaylistMod, 2);
                     

                if (!gruppe.grpobj.wirdAbgespielt || gruppe.grpobj.Vol_PlaylistMod == gruppe.zielProzent)
                {
                    grpToDelete.Add(gruppe);
                    gruppe.mZeile.VM.FadingPercentage = 0;
                }
            }
            //Check noch angeklickt zum FadingIn
            for (int c = 0; c < grpToDelete.Count; c++)
                fadInfo.gruppenIn.Remove(grpToDelete[c]);

            if (fadInfo.gruppenIn.Count == 0)
                _timerFadingInGeräusche.Stop();
        }

        public void FadingOutGeräusch(bool playerStoppen, bool sofort, MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj)
        {
            group newgroup = new group();
            newgroup.grpobj = grpobj;
            newgroup.mZeile = ErwPlayerGeräuscheListItemListe.FirstOrDefault(t => (Guid)t.Tag == grpobj.aPlaylist.Audio_PlaylistGUID);
            // .force_Volume != null      
            newgroup.startProzent = grpobj.DoForceVolume && !grpobj.aPlaylist.Fading ? grpobj.force_Volume.Value * 100 : grpobj.Vol_PlaylistMod;
            newgroup.zielProzent = 0;
            newgroup.lookupVol = newgroup.startProzent;
            newgroup.StartZeit = DateTime.MinValue;

            if (!_timerFadingOutGeräusche.IsEnabled)
            {
                FadingOutGeräusche _fadingOutGeräusche = new FadingOutGeräusche();
                _fadingOutGeräusche.gruppenOut.Clear();
                _fadingOutGeräusche.gruppenOut.Add(newgroup);
                _fadingOutGeräusche.fadingOutSofort = sofort;

                _timerFadingOutGeräusche.Interval = TimeSpan.FromMilliseconds(fadingIntervall);
                _timerFadingOutGeräusche.Tag = _fadingOutGeräusche;
                _timerFadingOutGeräusche.Start();
            }
            else
            {
                ((FadingOutGeräusche)_timerFadingOutGeräusche.Tag).gruppenOut.Add(newgroup);
            }
        }

        public void _timerFadingOutGeräusche_Tick(object sender, EventArgs e)
        {
            try
            {
                FadingOutGeräusche fadInfo = (FadingOutGeräusche)_timerFadingOutGeräusche.Tag;

                List<group> grpToDelete = new List<group>();

                if (fadInfo.gruppenOut.Count == 0) return;

                foreach (group gruppe in fadInfo.gruppenOut)
                {
                    if (gruppe == null || gruppe.grpobj == null)
                        continue;

                    if (gruppe.StartZeit == DateTime.MinValue)
                        gruppe.StartZeit = DateTime.Now;

                    gruppe._vergangeneZeit = DateTime.Now.Subtract(gruppe.StartZeit).TotalMilliseconds;

                    if (!fadInfo.fadingOutSofort && gruppe._vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                        fadInfo.fadingOutSofort = true;

                    if (gruppe.grpobj.wirdAbgespielt)
                    {
                        grpToDelete.Add(gruppe);
                        continue;
                    }
                    if (FadingIn_Started != null || fadInfo.fadingOutSofort) //&& FadingIn_Started.Source != null
                    {
                        fadInfo.fadingOutSofort = true;

                        gruppe.grpobj.Vol_PlaylistMod = (gruppe.grpobj.Vol_PlaylistMod != 0) ? gruppe.startProzent - gruppe.startProzent * (gruppe._vergangeneZeit / (fadingTime * fadingIntervall)) : 0;
                        gruppe.grpobj.Vol_PlaylistMod = gruppe.grpobj.Vol_PlaylistMod < 0 ? 0 : gruppe.grpobj.Vol_PlaylistMod;

                        // double l = 0;
                        foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t.aData != null))
                        {
                            if (!kZeile.aData.muted)
                                kZeile.aData.setVolume(!gruppe.grpobj.DoForceVolume ?
                                    (kZeile.Aktuell_Volume / 100) * (gruppe.grpobj.Vol_PlaylistMod / 100) :
                                    gruppe.grpobj.Vol_PlaylistMod / 100);
                            //    l = kZeile.aData.getVolume();
                        }
                        if (gruppe.mZeile != null)
                            gruppe.mZeile.VM.FadingPercentage = Math.Round(gruppe.grpobj.Vol_PlaylistMod, 2);

                        if (gruppe.grpobj.Vol_PlaylistMod == 0)
                        {
                            foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile in gruppe.grpobj._listZeile.FindAll(t => t.aData != null))
                            {
                                kZeile.istPause = true;
                                kZeile.aData.Pause();
                                kZeile.istStandby = true;
                                kZeile.istLaufend = false;
                                if (kZeile.audioZeileVM != null) kZeile.audioZeileVM.Progress = 0;
                                if (gruppe.grpobj.mZeileVM != null) gruppe.grpobj.mZeileVM.Min1SongWirdGespielt = null;
                            }
                            grpToDelete.Add(gruppe);
                        }
                    }
                    if (gruppe.grpobj.wirdAbgespielt)
                        grpToDelete.Add(gruppe);

                }

                for (int c = 0; c < grpToDelete.Count; c++)
                {
                    if (grpToDelete[c].grpobj != null && !grpToDelete[c].grpobj.wirdAbgespielt)
                    {
                        _GrpObjecte.Remove(grpToDelete[c].grpobj);
                        fadInfo.gruppenOut.Remove(grpToDelete[c]);
                    }
                }
                if (fadInfo.gruppenOut.Count == 0)
                {
                    _timerFadingOutGeräusche.Stop();
                    return;
                }
            }
            catch (Exception ex)
            {
             //   ViewHelper.ShowError("Fehler beim Fading Out" + Environment.NewLine + "Beim Fading-Out ist ein Fehler aufgetreten", ex);
            }
        }
        
        public void _timerFadingOut_Tick(object sender, EventArgs e)
        {
            Fading fadInfo = (Fading)_timerFadingOut.Tag;

            if (fadInfo.Start == DateTime.MinValue)
                fadInfo.Start = DateTime.Now;

            double _vergangeneZeit = DateTime.Now.Subtract(fadInfo.Start).TotalMilliseconds;

            if (!fadInfo.fadingOutSofort && _vergangeneZeit > 5000)  //maximale Wartezeit zum FadingOut
                fadInfo.fadingOutSofort = true;

            if (FadingIn_Started != null  || fadInfo.fadingOutSofort)//&& FadingIn_Started.Source != null
            {
                fadInfo.fadingOutSofort = true;
                //solange Volume runterregeln bis der Titel extern das Fadingstoppt
                if (fadInfo.aData != null)
                {
                    double aktVol = fadInfo.Offset/100 +
                        (fadInfo.fadingTime != 0 ? fadInfo.startVol - fadInfo.startVol * (_vergangeneZeit / (fadInfo.fadingTime * fadingIntervall)) : 0);
                    aktVol = aktVol < 0 ? 0 : aktVol;

                    if (fadInfo.lookupVol != BGPlayerVolume)
                    {
                        fadInfo.Offset = BGPlayerVolume - fadInfo.startVol;
                        fadInfo.lookupVol = BGPlayerVolume;
                    }

                    if (!fadInfo.aData.muted && fadInfo.aData.getVolume() != aktVol)
                        fadInfo.aData.setVolume(aktVol);

                    if (aktVol == 0)
                    {
                        if (fadInfo.mPlayerStoppen && fadInfo.klZeile.FadingOutStarted)   //bei Volume 0 Fadingauf false und Song freigeben
                        {                            
                            fadInfo.aData.Stop();
                            Player_Ended(fadInfo.aData, null);
                            fadInfo.aData.setPosition(0);
                            if (fadInfo.klZeile.audioZeileVM != null) fadInfo.klZeile.audioZeileVM.Progress = 0;
                            fadInfo.aData.Close();
                        }
                        if (!fadInfo.mPlayerStoppen && fadInfo.klZeile.FadingOutStarted)
                        {
                            fadInfo.aData.Pause();
                            fadInfo.aData.setPosition(0);
                            fadInfo.klZeile.istPause = true;
                        }

                        //     _volProzentModFadingOut = 1;
                        fadInfo.klZeile.FadingOutStarted = false;
                        FadingOut_Started = null;
                        _timerFadingOut.Stop();
                        if (fadInfo.mPlayerStoppen && fadInfo.klZeile.FadingOutStarted)
                            _GrpObjecte.Remove(fadInfo.grpobj);
                    }
                }
            }
        }
        
        #endregion

        #region //---- Timer und Zyklische Abläufe ----

        public void MusikProgBarTimer_Tick(object sender, EventArgs e)
        {
            if (MusikAktiv.aData != null)
            {
                if (MusikAktiv.aData.isPlaying())
                {

                    if (BGPlayer.AktPlaylistTitel != null && Info_BGTitel == null)
                        GetMusikGeneralInfo();

                    BGPosition = MusikAktiv.aData.getPosition();

                    if (BGPlayer.AktPlaylistTitel != null &&
                        BGPlayer.AktPlaylistTitel.TeilAbspielen &&
                        BGPosition < BGPlayerAktPlaylistTitelTeilStart)
                        MusikAktiv.aData.setPosition(BGPlayerAktPlaylistTitelTeilStart);

                    if (BGPlayer.AktPlaylistTitel != null &&
                        BGPlayer.AktPlaylistTitel.Audio_Titel != null &&
                        (BGPlayer.AktPlaylistTitel != null &&
                        MusikAktiv.aData.getLength() != BGPlayer.AktPlaylistTitel.Audio_Titel.Länge))
                    {
                        BGPlayer.AktPlaylistTitel.Audio_Titel.Länge = MusikAktiv.aData.getLength();
                        OnChanged("BGPlayerAktPlaylistTitelLänge");
                    }
                    
                };                
                
                //Bei Musikplaylists die Endposition vor Fading überprüfen
                if ((MusikAktiv.aData.getPosition() + TimeSpan.FromMilliseconds(fadInInfo.fadingTime * fadingIntervall).TotalMilliseconds >= MusikAktiv.aData.getLength()) ||
                    (BGPlayer.AktPlaylistTitel != null && BGPlayer.AktPlaylistTitel.TeilAbspielen && MusikAktiv.aData.getPosition() +
                    TimeSpan.FromMilliseconds(fadInInfo.fadingTime * fadingIntervall).TotalMilliseconds >= BGPlayer.AktPlaylistTitel.TeilEnde))
                {
                    Info_BGTitel = null;
                    SelectedMusikTitelItem = GetNextMusikTitel();
                }                
            }
        }
        
        private void KlangProgBarTimer_Tick(object sender, EventArgs e)
        {
            bool found = false;
            UInt16 loops = 0;
            KlangProgBarTimer.Tag = (KlangProgBarTimer.Tag.ToString() == "0") ? "1" : "0";
            try
            {
                for (int posObjGruppe = 0; posObjGruppe < _GrpObjecte.Count; posObjGruppe++)
                {                    
                    List<KlangZeile> KlangZeilenLaufend = _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend);

                    if (KlangZeilenLaufend != null && KlangZeilenLaufend.Count != 0)
                    {
                        if (KlangProgBarTimer.Interval == TimeSpan.FromMilliseconds(1000))
                            KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(100);
                        for (int durchlauf = 0; durchlauf < KlangZeilenLaufend.Count; durchlauf++)
                        {
                            if (KlangZeilenLaufend[durchlauf].istPause)
                                continue;

                            int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
                            if (objGruppe == -1)
                                break;
                            found = true;
                            loops++;
                            //if (_GrpObjecte[posObjGruppe].visuell &&
                            //    KlangZeilenLaufend[durchlauf].audioZeileVM != null)
                            //{
                            //    if (KlangZeilenLaufend[durchlauf].aData != null &&
                            //        KlangZeilenLaufend[durchlauf]._mplayer.HasAudio == false &&
                            //        KlangZeilenLaufend[durchlauf]._mplayer.BufferingProgress == 1)
                            //        KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.Now;
                            //    else
                            //        KlangZeilenLaufend[durchlauf].dtLiedLastCheck = DateTime.MinValue;

                            //    //keine Informationen nach 1 Sekunde vom MediaPlayer über Track -> Gelb -> nächstes Lied
                            //    if (((TimeSpan)(DateTime.Now - KlangZeilenLaufend[durchlauf].dtLiedLastCheck)).TotalMilliseconds > ((double)Zeitüberlauf+5000))
                            //    {
                            //        if (!KlangZeilenLaufend[durchlauf]._mplayer.HasAudio)
                            //        {
                            //            KlangZeilenLaufend[durchlauf].audioZeileVM.FilePlayable = false;
                            //            KlangZeilenLaufend[durchlauf]._mplayer.Stop();
                            //            KlangZeilenLaufend[durchlauf]._mplayer.Close();
                            //            //KlangZeilenLaufend[durchlauf].playable = false;
                            //            KlangZeilenLaufend[durchlauf].istStandby = true;
                            //            KlangZeilenLaufend[durchlauf].istLaufend = false;
                            //            KlangZeilenLaufend[durchlauf].istPause = false;
                            //            CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);
                            //        }
                            //    }
                            //}

                            if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.Aktiv && (KlangProgBarTimer.Tag.ToString() == "0") &&
                                KlangZeilenLaufend[durchlauf].aData != null)
                            {
                                if ((!_timerFadingInGeräusche.IsEnabled && !_timerFadingOutGeräusche.IsEnabled) ||
                                    !_GrpObjecte[posObjGruppe].aPlaylist.Fading)
                                {
                                    // Volume anpassen
                                    double volMin = KlangZeilenLaufend[durchlauf].volMin_wert > .004 ? KlangZeilenLaufend[durchlauf].volMin_wert : .005;

                                    if (!_GrpObjecte[posObjGruppe].DoForceVolume)
                                    {
                                        if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.VolumeChange && !KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                        {
                                            if ((((TimeSpan)(DateTime.Now - _GrpObjecte[posObjGruppe].LastVolUpdate)).Seconds > KlangZeilenLaufend[durchlauf].UpdateZyklusVol) &&
                                                Math.Abs(KlangZeilenLaufend[durchlauf].aData.getVolume() * 100 - KlangZeilenLaufend[durchlauf].volZiel) <= KlangZeilenLaufend[durchlauf].Vol_jump)
                                            {
                                                KlangZeilenLaufend[durchlauf].volZiel =
                                                    ((double)((new Random()).Next(0, (int)(1000 * Math.Round(KlangZeilenLaufend[durchlauf].volMax_wert - volMin, 0)))) / 1000 +
                                                    volMin) / 1;
                                            }

                                            // Feinere Sprungabstände bei kleinerem Lautstärkeunterschied
                                            KlangZeilenLaufend[durchlauf].Vol_jump = (KlangZeilenLaufend[durchlauf].volMax_wert - volMin < SliderTeile) ?
                                                (KlangZeilenLaufend[durchlauf].volMax_wert - volMin) / SliderTeile :
                                                1;

                                            double volJump = ((KlangZeilenLaufend[durchlauf].Aktuell_Volume < volMin ||
                                                KlangZeilenLaufend[durchlauf].Aktuell_Volume > (double)KlangZeilenLaufend[durchlauf].volMax_wert) ||
                                                (Math.Abs(KlangZeilenLaufend[durchlauf].volZiel - KlangZeilenLaufend[durchlauf].Aktuell_Volume) > 6) ||
                                                (KlangZeilenLaufend[durchlauf].volMax_wert - volMin < SliderTeile)) ?
                                                KlangZeilenLaufend[durchlauf].Vol_jump : 1;

                                            KlangZeilenLaufend[durchlauf].Aktuell_Volume = (KlangZeilenLaufend[durchlauf].volZiel < KlangZeilenLaufend[durchlauf].Aktuell_Volume) ?
                                                KlangZeilenLaufend[durchlauf].Aktuell_Volume -= volJump :
                                                KlangZeilenLaufend[durchlauf].Aktuell_Volume += volJump;
                                        }
                                        else
                                            if (!KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                                KlangZeilenLaufend[durchlauf].Aktuell_Volume != KlangZeilenLaufend[durchlauf].aPlaylistTitel.Volume)
                                                KlangZeilenLaufend[durchlauf].Aktuell_Volume = KlangZeilenLaufend[durchlauf].aPlaylistTitel.Volume;
                                    }
                                    double sollWert = KlangZeilenLaufend[durchlauf].aData.muted? 0: 
                                        (KlangZeilenLaufend[durchlauf].Aktuell_Volume / 100) * (!_GrpObjecte[posObjGruppe].aPlaylist.Fading ? (FadingGeräuscheVolProzent / 100): 
                                        (!_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik ? _GrpObjecte[posObjGruppe].Vol_PlaylistMod / 100 : 
                                        1));
                                    

                                    //Forcing des VOLUME
                                    if (!KlangZeilenLaufend[durchlauf].aData.muted)
                                    {
                                        if ((FadingIn_Started == null || FadingIn_Started.audioStream == 0) &&
                                            !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                            _GrpObjecte[posObjGruppe].DoForceVolume)
                                            KlangZeilenLaufend[durchlauf].aData.setVolume(_GrpObjecte[posObjGruppe].force_Volume.Value);
                                        else
                                            if (FadingIn_Started == null || FadingIn_Started.audioStream == 0 &&
                                                !KlangZeilenLaufend[durchlauf].FadingOutStarted &&
                                                (Convert.ToSingle(sollWert) != KlangZeilenLaufend[durchlauf].aData.getVolume()))
                                                KlangZeilenLaufend[durchlauf].aData.setVolume(sollWert);
                                    }
                                }

                                //einmaliges ermitteln des Endzeitpunkts
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeileVM != null &&                                
                                    KlangZeilenLaufend[durchlauf].audioZeileVM.TitelMaximum == 10000000 && 
                                    KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel != null)
                                {
                                    if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel.Länge != KlangZeilenLaufend[durchlauf].aData.getLength())
                                    {
                                        KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel.Länge = KlangZeilenLaufend[durchlauf].aData.getLength();
                                        Global.ContextAudio.Update<Audio_Titel>(KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel);
                                        UpdatePlaylistLänge(_GrpObjecte[posObjGruppe].aPlaylist);
                                    }
                                    KlangZeilenLaufend[durchlauf].audioZeileVM.TitelMaximum = (double)KlangZeilenLaufend[durchlauf].aPlaylistTitel.Audio_Titel.Länge;
                                }
                                double aktPos = KlangZeilenLaufend[durchlauf].aData.getPosition();
                                //aktualisiere ProgressBar - bei Wartezeit auf Maximum
                                if (_GrpObjecte[posObjGruppe].visuell && KlangZeilenLaufend[durchlauf].audioZeileVM != null )
                                    KlangZeilenLaufend[durchlauf].audioZeileVM.Progress = 
                                        (!KlangZeilenLaufend[durchlauf].istWartezeit) ?
                                        aktPos :
                                            KlangZeilenLaufend[durchlauf].aData.getLength();
       

                                // Endposition von Geräuschen überprüfen bei vorzeitigem Ende ODER über der MaxPosition
                                if (!_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik)
                                    if ((KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen &&
                                        aktPos >= KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilEnde) ||
                                        (
                                        // aktPos >= getLength GEHT NICHT !!!
                                        //aktPos >= KlangZeilenLaufend[durchlauf].aData.getLength() ||
                                        !KlangZeilenLaufend[durchlauf].aData.isPlaying()))
                                    {
                                        _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));
                                        if (_GrpObjecte[posObjGruppe].Gespielt.Count > 50)
                                            _GrpObjecte[posObjGruppe].Gespielt.RemoveAt(0);

                                        bool nurEinLiedAktiv = ( _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count == 1) ? true : false;
                                        if (nurEinLiedAktiv && KlangZeilenLaufend[durchlauf].aPlaylistTitel.Pause == 0)
                                        {
                                            if (aktPos != 0)
                                            {
                                                KlangZeilenLaufend[durchlauf].aData.setPosition(
                                                    (KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen) ?
                                                        Math.Round(KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilStart.Value, 0, MidpointRounding.ToEven) :
                                                        0);
                                                if (!KlangZeilenLaufend[durchlauf].aData.isPlaying())
                                                    KlangZeilenLaufend[durchlauf].aData.Play();
                                            }
                                            else
                                            {
                                                KlangZeilenLaufend[durchlauf].aData.Stop();
                                                KlangZeilenLaufend[durchlauf].aData.Play();
                                            }
                                            if (KlangZeilenLaufend[durchlauf].aPlaylistTitel.PauseChange)
                                            {
                                                long neu = (new Random()).Next(Convert.ToUInt16(KlangZeilenLaufend[durchlauf].aPlaylistTitel.PauseMin),
                                                        Convert.ToUInt16(KlangZeilenLaufend[durchlauf].aPlaylistTitel.PauseMax));
                                                KlangZeilenLaufend[durchlauf].aPlaylistTitel.Pause = neu;
                                            }
                                        }
                                        else
                                        {                                            
                                         //   KlangZeilenLaufend[durchlauf].aData.Stop();                                            
                                            Player_Ended(KlangZeilenLaufend[durchlauf].aData, null);
                                            KlangZeilenLaufend[durchlauf].aData.Close();
                                           // KlangZeilenLaufend[durchlauf].istLaufend = false;
                                            KlangZeilenLaufend[durchlauf].istPause = true;
                                          //  KlangZeilenLaufend[durchlauf].istStandby = false;
                                            
                                            CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);

                                            KlangZeilenLaufend[durchlauf].istStandby = true;

                                            if (KlangZeilenLaufend[durchlauf].audioZeileVM != null)
                                                KlangZeilenLaufend[durchlauf].audioZeileVM.Progress = 0;
                                        }
                                    }
                                    
                                //Musik Endposition (incl vor Fading) überprüfen
                                if (_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik && !KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                    //&&KlangZeilenLaufend[durchlauf]._mplayer.NaturalDuration.HasTimeSpan)

                                    //Bei TeilAbspielen und Ende erreicht
                                    if ((KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen &&
                                         KlangZeilenLaufend[durchlauf].aData.getPosition() + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= 
                                         KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilEnde)
                                        ||
                                        (!KlangZeilenLaufend[durchlauf].aPlaylistTitel.TeilAbspielen &&
                                        KlangZeilenLaufend[durchlauf].aData.getPosition() + TimeSpan.FromMilliseconds(fadingTime * fadingIntervall).TotalMilliseconds >= 
                                        KlangZeilenLaufend[durchlauf].aData.getLength())
                                        )
                                    {
                                        _GrpObjecte[posObjGruppe].Gespielt.Add(Convert.ToUInt16(durchlauf));
                                        if (_GrpObjecte[posObjGruppe].Gespielt.Count > 50)
                                            _GrpObjecte[posObjGruppe].Gespielt.RemoveAt(0);

                                        if (!KlangZeilenLaufend[durchlauf].FadingOutStarted)
                                        {
                                            KlangZeilenLaufend[durchlauf].FadingOutStarted = true;
                                            FadingOut(KlangZeilenLaufend[durchlauf], _GrpObjecte[posObjGruppe], true, false);
                                        }

                                        KlangZeilenLaufend[durchlauf].istLaufend = false;
                                        KlangZeilenLaufend[durchlauf].istPause = false;

                                        bool nurEinLiedAktiv = (_GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count == 1) ? true : false;
                                        KlangZeilenLaufend[durchlauf].istStandby = nurEinLiedAktiv;

                                        CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);

                                        if (!nurEinLiedAktiv)
                                            KlangZeilenLaufend[durchlauf].istStandby = true;

                                        if (KlangZeilenLaufend[durchlauf].audioZeileVM != null)
                                            KlangZeilenLaufend[durchlauf].audioZeileVM.Progress = 0;
                                    }
                            }
                        }
                    }

                    if (_GrpObjecte[posObjGruppe].wirdAbgespielt &&
                      //  !_GrpObjecte[posObjGruppe].visuell &&
                        KlangZeilenLaufend != null && KlangZeilenLaufend.Count == 0 &&
                        _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.playable).Count > 0 &&
                        !_GrpObjecte[posObjGruppe].wartezeitTimer.IsEnabled)
                    {
                        _GrpObjecte[posObjGruppe]._listZeile.ForEach(delegate(KlangZeile kZeile) 
                        {
                            kZeile.istPause = false;
                            kZeile.playable = true;
                            kZeile.istStandby = true;
                        });
                        CheckPlayStandbySongs(_GrpObjecte[posObjGruppe]);
                    }
                }
                if (!found)
                {
                    KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(1000);
                }
            }
            catch (Exception)
            {
                // ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Durchlauf der Prozedur 'KlangProgBarTimer' ist ein Fehler aufgetreten", ex);
            }
        }

        void KlangPlayEndetimer_Tick(object sender, EventArgs e)
        {
            int posit = -1;
            int zeile = -1;
            int neu = -1;
            double wertPlus = -1;
            int IndexPlus = -1;
            try
            {
                (sender as DispatcherTimer).Stop();

                UInt16 sollID_Zeile = Convert.ToUInt16((sender as DispatcherTimer).Tag);

                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt _chkGrpObj in _GrpObjecte)
                {
                    if (_chkGrpObj._listZeile.FirstOrDefault(t => t.ID_Zeile == sollID_Zeile) != null)
                    {
                        grpobj = _chkGrpObj;
                        break;
                    }
                }
                if (grpobj != null)
                {
                    zeile = grpobj._listZeile.IndexOf(
                        grpobj._listZeile.FirstOrDefault(t => t.ID_Zeile == sollID_Zeile));

                    grpobj._listZeile[zeile].istWartezeit = false;
                    if (grpobj.visuell)
                    {
                        grpobj._listZeile[zeile].audioZeileVM.Progress = 0;
                        if (grpobj._listZeile[zeile].aPlaylistTitel.PauseChange)
                        {
                            if (Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMin) > Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMax))
                                grpobj._listZeile[zeile].aPlaylistTitel.PauseMin = grpobj._listZeile[zeile].aPlaylistTitel.PauseMax;
                            neu = (new Random()).Next(Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMin),
                                                      Convert.ToUInt16(grpobj._listZeile[zeile].aPlaylistTitel.PauseMax));
                            grpobj._listZeile[zeile].aPlaylistTitel.Pause = neu;
                        }
                    }

                    // Song aus der Liste der laufenden Songs herausnehmen
                    grpobj._listZeile[zeile].istLaufend = false;
                    // Song in die Liste der Standby-Songs aufnehmen wenn nur ein Song in Liste
                    if (grpobj._listZeile.FindAll(t => t.istStandby).Count == 0)
                    {
                        grpobj._listZeile[zeile].istStandby = true;
                        grpobj._listZeile[zeile].istPause = false;
                        CheckPlayStandbySongs(grpobj);
                    }
                    else
                    {
                        posit = 4;
                        // Song in die Liste der Standby-Songs aufnehmen wenn nur mehere Songs verfügbar
                        //FALSCH     // somit wird nicht 2x der gleiche Song gespielt
                        CheckPlayStandbySongs(grpobj);
                        grpobj._listZeile[zeile].istStandby = true;
                        grpobj._listZeile[zeile].istPause = false;
                        posit = 5;
                    }
                }
                lstKlangPlayEndetimer.Remove(sender as DispatcherTimer);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Fehler beim Überprüfen der Endewartezeit" + Environment.NewLine +
                 "Zeile=" + zeile + "   wertPlus" + wertPlus + "   Neu=" + neu + "   IndexPlus=" + IndexPlus + " Posit=" + posit, ex);
            }
        }
        

        #endregion

        #region *** hotkeyListe ***

        private void addHotkey(int i)
        {
            btnHotkey hkey = new btnHotkey();
            hkey.VM.taste = i;
            hkey.VM.aPlaylistGuid = Guid.Empty;

            hotkeyListe.Add(hkey);
        }

        public void AktualisiereHotKeys()
        {
            hotkeyListe.RemoveRange(0, hotkeyListe.Count);
            for (int i = 48; i <= 57; i++)
                addHotkey(i);

            for (int i = 65; i < 91; i++)
                addHotkey(i);
        }

        

        #endregion
        
        #region //---- EVENTS ----

        #region --- BassPlayer ----
        void Player_Ended(object sender, EventArgs e)
        {
            try
            {
                int posObjGruppe = 0;
                
                while (posObjGruppe < _GrpObjecte.Count &&
                    !_GrpObjecte[posObjGruppe]._listZeile.Exists(t => t.mediaHashCode.Equals((sender as AudioData).GetHashCode()))) // .GetHashCode()))) .mediaHashCode
                    posObjGruppe++;
                // Editor Zeilen behandeln
                if (posObjGruppe < _GrpObjecte.Count)
                {
                    int zeile = _GrpObjecte[posObjGruppe]._listZeile.FindIndex(t => t.mediaHashCode.Equals((sender as AudioData).GetHashCode())); //.GetHashCode())); .mediaHashCode
                    if (zeile == -1) return;

                    if (!_GrpObjecte[posObjGruppe].wirdAbgespielt)
                    {
                        if (_timerFadingOutGeräusche.Tag != null &&
                            ((FadingOutGeräusche)_timerFadingOutGeräusche.Tag).gruppenOut.FirstOrDefault(t => t.grpobj == _GrpObjecte[posObjGruppe]) != null)
                        {
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].istPause = false;
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].istStandby = true;
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].istLaufend = false;
                        }
                        else
                            return;
                    }

                    int objGruppe = _GrpObjecte[posObjGruppe].objGruppe;
                    if (objGruppe == -1)
                        return;

                    if (!_GrpObjecte[posObjGruppe].aPlaylist.Hintergrundmusik &&           // Direkt wieder anstarten wenn der Titel die einigste Möglichkeit ist
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.Pause == 0 &&
                        _GrpObjecte[posObjGruppe].aPlaylist.MaxSongsParallel == _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istLaufend).Count &&
                        _GrpObjecte[posObjGruppe]._listZeile.FindAll(t => t.istStandby).Count == 0)
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].aData.setPosition((!_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.TeilAbspielen) ?
                            0 : Math.Round(_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.TeilStart.Value, 0, MidpointRounding.ToEven));

                        if (_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.PauseChange)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.Pause =
                                (new Random()).Next(_GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMin_wert,
                                    _GrpObjecte[posObjGruppe]._listZeile[zeile].pauseMax_wert);
                    }
                    else
                    {
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].istWartezeit = true;
                        
                        _GrpObjecte[posObjGruppe]._listZeile[zeile].aData.Stop();
                        //Player_Ended(_GrpObjecte[posObjGruppe]._listZeile[zeile].aData, null);
                        if (!_GrpObjecte[posObjGruppe].visuell)
                            _GrpObjecte[posObjGruppe].mZeileVM.Min1SongWirdGespielt = false;
                        
                        KlangPlayEndetimer = new DispatcherTimer();

                        if (_GrpObjecte[posObjGruppe].visuell)
                            //Im Editor keine Wartezeit abwarten
                            KlangPlayEndetimer.Interval = TimeSpan.FromMilliseconds(0);
                        else
                            //Neue Wartezeit fest oder per RANDOM bestimmen                        
                            KlangPlayEndetimer.Interval = (_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.PauseChange) ?
                                TimeSpan.FromMilliseconds(
                                    (new Random()).Next((int)_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.PauseMin,
                                    (int)_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.PauseMax)) :
                                TimeSpan.FromMilliseconds(_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.Pause);

                        KlangPlayEndetimer.Tag = _GrpObjecte[posObjGruppe]._listZeile[zeile].ID_Zeile;
                        KlangPlayEndetimer.Tick += new EventHandler(KlangPlayEndetimer_Tick);
                        KlangPlayEndetimer.Start();
                        lstKlangPlayEndetimer.Add(KlangPlayEndetimer);

                        if (!_GrpObjecte[posObjGruppe]._listZeile[zeile].aPlaylistTitel.TeilAbspielen)
                            _GrpObjecte[posObjGruppe]._listZeile[zeile].aData.Close();
                    }
                }
                App.CloseSplashScreen();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Playlist Fehler" + Environment.NewLine + "Nach dem Beenden des Musiktitels ist ein Fehler aufgetreten", ex);
            }
        }
        
        void Player_KlangMediaFailed(object sender, ExceptionEventArgs e)
        {
            try
            {
                char[] Separator = new char[] { '_' };

               // int mediahash = (sender as AudioData).GetHashCode();
                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj = null;
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt chkgrpobj in _GrpObjecte)
                    if (chkgrpobj._listZeile.Exists(t => t.aData.audioStream.Equals((sender as AudioData).audioStream)))//.mediaHashCode.Equals
                    {
                        grpobj = chkgrpobj;
                        break;
                    }

                if (grpobj != null)
                {
                    int zeile = grpobj._listZeile.FindIndex(t => t.aData.audioStream.Equals((sender as AudioData).audioStream));

                    int objGruppe = grpobj.objGruppe;
                    if (objGruppe == -1)
                        return;

                    grpobj.NochZuSpielen.RemoveAll(t => t.Equals(grpobj._listZeile[zeile].aPlaylistTitel.Audio_TitelGUID));
                    if (grpobj._listZeile[zeile].aData != null)
                    {
                        grpobj._listZeile[zeile].aData.Stop();
                        Player_Ended(grpobj._listZeile[zeile].aData, null);
                        grpobj._listZeile[zeile].aData.Close();
                    }

                    grpobj._listZeile[zeile].playMediaFailed++;

                    grpobj._listZeile[zeile].istPause = false;
                    grpobj._listZeile[zeile].istLaufend = false;
                    grpobj._listZeile[zeile].istStandby = true;
                    grpobj._listZeile[zeile].playable = true;
                                        
                    // *** VORGANG WENN ES ZU OFT VORGEKOMMEN IST, DASS DER TITEL NICHT ABGESPIELT WERDEN KANN
                    // *** WIRD DER TITEL ALS nicht abspielbar GEKENNZEICHNET

                    //if ((grpobj._listZeile[zeile].playMediaFailed > 4))
                    //{
                    //    grpobj._listZeile[zeile].playable = false;
                    //    if (grpobj._listZeile[zeile].audioZeileVM != null)
                    //        grpobj._listZeile[zeile].audioZeileVM.FilePlayable = false;
                    //}

                    foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                    {
                        if ((Guid)mZeile.Tag == grpobj.aPlaylist.Audio_PlaylistGUID)
                        {
                            mZeile.VM.TeilAbspielbar = true; //Yellow 
                            mZeile.VM.Output = grpobj._listZeile.FindAll(t => t.playable).FindAll(t => t.aPlaylistTitel.Aktiv).Count + 
                                " von " + grpobj._listZeile.FindAll(t => t.aPlaylistTitel.Aktiv).Count + " Titel abspielbar";
                            OnChanged("TeilAbspielbar");
                        }
                    }
                    CheckPlayStandbySongs(grpobj);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswetrten des Klang-Playerfehlers ist ein Fehler aufgetreten.", ex);
            }
        }

        void Player_MusikMediaFailed(object sender, ExceptionEventArgs e)
        {
            try
            {

                (sender as AudioData).Stop();
                Player_Ended((sender as AudioData), null);
                (sender as AudioData).Close();
              //  (sender as AudioData).MediaFailed -= Player_KlangMediaFailed;
                MusikProgBarTimer.Stop();
                if (SelectedMusikTitelItem != null)
                {
                    SelectedMusikTitelItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));      // Yellow
                    SelectedMusikTitelItem.ToolTip = "Datei kann nicht abgespielt werden. Ungültiger Name? Vermeiden Sie Sonderzeichen( #&'! ) im Zusammenhang mit Netzlaufwerken.";
                }
                SpieleNeuenMusikTitel(Guid.Empty);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswerten des Musikfehlers ist ein Fehler aufgetreten.", ex);
            }
        }

        #endregion

        /// <summary>
        /// Läd die AudioZeilen-Liste auf Basis der ausgewählten AktKlangPlaylist.
        /// </summary>
        public void LadeAudioZeilen()
        {
            List<AudioZeileVM> itemList = new List<AudioZeileVM>();

            if (AktKlangPlaylist != null &&
                (AktKlangPlaylist.Audio_Playlist_Titel.Count == 0 ||
                 LbEditorAudioZeilenListe.Count == 0 ||
                 LbEditorAudioZeilenListe.Count(t => t.AktKlangPlaylist == AktKlangPlaylist) != AktKlangPlaylist.Audio_Playlist_Titel.Count))
            {
                Global.SetIsBusy(true, string.Format("Öffne Playliste & Überprüfe Dateien..."));

                foreach (Audio_Playlist_Titel aPlayTitel in AktKlangPlaylist.Audio_Playlist_Titel)
                {
                    AudioZeileVM aZeile = new AudioZeileVM();
                    aZeile.PlayerVM = this;
                    aZeile.AktKlangPlaylist = AktKlangPlaylist;
                    aZeile.aPlayTitel = aPlayTitel;

                    if (AudioInAnderemPfadSuchen &&
                        !File.Exists(aZeile.aPlayTitel.Audio_Titel.Pfad + "\\" + aZeile.aPlayTitel.Audio_Titel.Datei))
                    {
                        Audio_Titel titel = setTitelStdPfad(aZeile.aPlayTitel.Audio_Titel);
                        setTitelStdPfad_AufrufeHintereinander = 0;
                        if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                            Global.ContextAudio.Update<Audio_Titel>(titel);
                    }
                    if (!File.Exists(aZeile.aPlayTitel.Audio_Titel.Pfad + "\\" + aZeile.aPlayTitel.Audio_Titel.Datei))
                        aZeile.FileNotExist = true;

                    itemList.Add(aZeile);
                }
                LbEditorAudioZeilenListe = itemList;
                CheckBtnGleicherPfad(AktKlangPlaylist);
                Global.SetIsBusy(false);
            }

        }

        /// <summary>
        /// Läd die ThemeZeilen-Liste auf Basis der ausgewählten AktKlangTheme.
        /// </summary>
        private void LadeAudioThemeZeilen()
        {
            List<ListboxItemIcon> itemList = new List<ListboxItemIcon>();

            foreach (Audio_Playlist aPlaylist in AktKlangTheme.Audio_Playlist)
            {
                ListboxItemIcon aZeile = new ListboxItemIcon();
                aZeile.Tag = aPlaylist;
            }
        }

        private void LadeBoxThemeList()
        {
            List<boxThemeTheme> lstBoxThemeHintergrund = new List<boxThemeTheme>();
            List<boxThemeTheme> lstBoxThemeGeräusche = new List<boxThemeTheme>();
            List<boxThemeTheme> lstBoxThemeTheme = new List<boxThemeTheme>();

            foreach (Audio_Playlist aPlayList in AktKlangTheme.Audio_Playlist)
            {
                boxThemeTheme boxTheme = new boxThemeTheme();
                boxTheme.aPlaylist = aPlayList;
                boxTheme.aTheme = AktKlangTheme;
                boxTheme.btnRemove.Tag = aPlayList;
                boxTheme.btnRemove.Click += btnRemove_Click;

                if (aPlayList.Hintergrundmusik)
                    lstBoxThemeHintergrund.Add(boxTheme);
                else
                    lstBoxThemeGeräusche.Add(boxTheme);
            }
            boxThemeThemeHintergrundList = lstBoxThemeHintergrund;
            boxThemeThemeGeräuscheList = lstBoxThemeGeräusche;

            // Erstelle Untergeordnete Themes
            foreach (Audio_Theme aUnterTheme in AktKlangTheme.Audio_Theme1.
                Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).OrderBy(t => t.Name))
            {
                boxThemeTheme bxTheme = new boxThemeTheme();
                bxTheme.txblkName.Text = aUnterTheme.Name;
                bxTheme.Tag = aUnterTheme.Audio_ThemeGUID;
                bxTheme.btnRemove.Tag = aUnterTheme;
                bxTheme.btnRemove.Click += btnRemoveTheme_Click;
                lstBoxThemeTheme.Add(bxTheme);
            }
            boxThemeThemeList = lstBoxThemeTheme;
        }

        public void ThemeItemIconAblegen(Audio_Playlist aPlaylist)
        {
            if (AktKlangTheme == null)
            {
                NeuesKlangThemeInDB("");
                UpdateAlleListen();
                SelectedEditorThemeItem = FilteredEditorThemeListBoxItemListe.FirstOrDefault(t => t.ATheme == AktKlangTheme);
            }
            if (aPlaylist.Hintergrundmusik && AktKlangTheme.Audio_Playlist.Count(t => t.Hintergrundmusik) == 1)
            {
                ViewHelper.Popup("Es ist bereits eine Musik-Playliste in dem Theme eingetragen." + Environment.NewLine + Environment.NewLine +
                       "Entfernen Sie zunächst die aktuelle Musik-Playliste des Themes um eine neue zu definieren.");
            }
            else
            {
                AktKlangTheme.Audio_Playlist.Add(aPlaylist);
            }
            lbEditorThemeItemVM lvTheme = SelectedEditorThemeItem;
            SelectedEditorThemeItem = null;
            SelectedEditorThemeItem = lvTheme;
        }

        /// <summary>
        /// Neuer Titel vom FileChooser aus. Wird zur aktuellen Playlist hinzugefügt.
        /// </summary>
        /// <param name="sender"></param>
        private void AddTitel(object sender)
        {
            string pfad = "";
            Audio_Titel aTitel = CreateTitel(pfad);
            Audio_Playlist_Titel aPlaylistTitel = AddTitelToPlaylist(CurrentPlaylist, aTitel);
            TitelListe.Add(new TitelInfo(aPlaylistTitel));
        }

        public void RenewMusikNochZuSpielen()
        {
            if (BGPlayer.AktPlaylist == null)
                return;
            if (MusikNacheinander && (BGPlayer.Gespielt.Count != 0 || BGPlayer.AktPlaylist.Audio_Playlist_Titel.Count == 0))
            {
                int aktPos = FilteredMusikPlaylistItemListe.FindIndex(t => t.VM.aPlaylist == BGPlayer.AktPlaylist);
                if (aktPos == FilteredMusikPlaylistItemListe.Count - 1)
                    SelectedMusikPlaylistItem = FilteredMusikPlaylistItemListe.First();
                else
                    if (FilteredMusikPlaylistItemListe.Count >= aktPos)
                        SelectedMusikPlaylistItem = FilteredMusikPlaylistItemListe.ElementAt(aktPos + 1);
                return;
            }
            List<Audio_Playlist_Titel> aPlayTitelList = new List<Audio_Playlist_Titel>(MusikTitelAZ?
                BGPlayer.AktPlaylist.Audio_Playlist_Titel.OrderBy(t =>  t.Audio_Titel.Name):
                BGPlayer.AktPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge));

            foreach (Audio_Playlist_Titel aPlaylistTitel in aPlayTitelList)
            {
                if (aPlaylistTitel.Aktiv && !BGPlayer.MusikNOK.Contains(aPlaylistTitel.Audio_TitelGUID))
                {
                    for (int bew = 0; bew <= aPlaylistTitel.Rating; bew++)
                        BGPlayer.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                }
            }            
        }

        public void SpieleNeuenMusikTitel(Guid Index, bool addGespielt = true)
        {
            while (BGPlayer.BG.FirstOrDefault(t => (t.aPlaylist == null && t.aData == null) || //.audioStream == 0
                (!t.FadingOutStarted && t.isPaused)) != null)
            {
                int x = BGPlayer.BG.FindIndex(t => (t.aPlaylist == null && t.aData == null) || //.audioStream == 0
                    (!t.FadingOutStarted && t.isPaused));
                if (x != -1)
                    BGPlayer.BG.RemoveAt(x);
            }

            if (BGPlayer.NochZuSpielen.Count == 0 &&
                ((BGPlayer.AktPlaylist.Repeat == null || (BGPlayer.AktPlaylist.Repeat != null && BGPlayer.AktPlaylist.Repeat.Value)) &&
                BGPlayer.MusikNOK.Count != FilteredHintergrundMusikListe.Count || !MusikAktivIsPaused))
            {
                BGPlayer.NochZuSpielen.Clear();
                BGPlayer.Gespielt.Clear();
                BGPlayerGespieltCount = 0;
                RenewMusikNochZuSpielen();
                if (BGPlayer.NochZuSpielen.Count == 0)
                    return;
            }
            else
                if (BGPlayer.NochZuSpielen.Count == 0)
                {
                    MusikAktivIsPaused = true;
                    return;
                }

            if (MusikAktiv.aData != null && MusikAktiv.aData.audioStream != 0 && MusikAktiv.aData.getPosition() > 0)
            {
                if (SelectedMusikTitelItem != null && addGespielt)
                {
                    BGPlayer.Gespielt.Add((Guid)((Audio_Playlist_Titel)SelectedMusikTitelItem.Tag).Audio_TitelGUID);

                    if (BGPlayer.Gespielt.Count > 50)
                        BGPlayer.Gespielt.RemoveAt(0);
                }
                MusikProgBarTimer.Stop();
            }
            else
                if (MusikAktiv.aData != null && MusikAktiv.aData.isStopped())
                    BGPlayer.Gespielt.Add(Index);
            BGPlayerGespieltCount = BGPlayer.Gespielt.Count;

            if (BGPlayer.NochZuSpielen.Count != 0)  // kein abspielbarer Titel gefunden
            {
                if (FilteredHintergrundMusikListe.Count == 0)
                {
                    SelectedMusikTitelItem = null;
                    BGPlayerAktPlaylistTitel = null;
                    SelectedMusikTitelItem = FilteredHintergrundMusikListe[0];
                }
                else
                {
                    if (Index != Guid.Empty)
                    {
                        BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(Index));
                        if (!addGespielt)
                            BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == Index);
                    }
                    else
                    {
                        // Shuffle-Modus
                        if (BGPlayer.AktPlaylist.Shuffle)
                        {
                            Guid u = BGPlayer.NochZuSpielen[(new Random()).Next(0, BGPlayer.NochZuSpielen.Count)];
                            BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(u));
                            BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == u);
                        }
                        else
                        {
                            int i = BGPlayer.AktPlaylistTitel == null? -1:
                                FilteredHintergrundMusikListe.IndexOf(FilteredHintergrundMusikListe.FirstOrDefault(t => ((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == BGPlayer.AktPlaylistTitel.Audio_TitelGUID));
                            BGPlayerAktPlaylistTitel = BGPlayer.AktPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == (Guid)((Audio_Playlist_Titel)FilteredHintergrundMusikListe[i + 1].Tag).Audio_TitelGUID);
                            BGPlayer.NochZuSpielen.RemoveAll(t => t.Equals(BGPlayer.AktPlaylistTitel.Audio_TitelGUID));
                        }
                        SelectedMusikTitelItem = FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == BGPlayer.AktPlaylistTitel.Audio_TitelGUID);
                    }

                    if (!BGPlayer.MusikNOK.Contains(BGPlayer.AktPlaylistTitel.Audio_TitelGUID))
                    {
                        Audio_Titel titel = BGPlayer.AktPlaylistTitel.Audio_Titel;
                        if (AudioInAnderemPfadSuchen &&
                            !Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei))
                        {
                            titel = setTitelStdPfad(titel);
                            setTitelStdPfad_AufrufeHintereinander = 0;
                            if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                                Global.ContextAudio.Update<Audio_Titel>(titel);
                        }

                        if (Directory.Exists(titel.Pfad) && !File.Exists(titel.Pfad + "\\" + titel.Datei) ||
                            !Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)))
                        {
                            if (!BGPlayer.MusikNOK.Contains(BGPlayer.AktPlaylistTitel.Audio_TitelGUID))
                                BGPlayer.MusikNOK.Add(BGPlayer.AktPlaylistTitel.Audio_TitelGUID);
                        }
                        else
                        {
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(titel.Pfad + "\\" + titel.Datei)) &&
                                File.Exists(titel.Pfad + "\\" + titel.Datei))
                            {
                                // Alle laufenden Musiktitel OutFading initiieren
                                BGPlayer.BG.Where(t2 => !t2.FadingOutStarted).ToList()
                                    .ForEach(delegate(Musik m)
                                {
                                    if (m.aData == null)
                                        BGPlayer.BG.Remove(m);
                                    else
                                        if (m.aData.isPlaying() && m.aTitel != null &&
                                            (m.aTitel.Audio_TitelGUID != titel.Audio_TitelGUID ||
                                            wiederholungenLeft >= 1 ||
                                            BGPlayer.AktPlaylist.Audio_Playlist_Titel.Where(t => t.Aktiv).ToList().Count == 1))
                                        {
                                            m.FadingOutStarted = true;
                                            BGFadingOut(m, true, false);   //BassSong des Titels Freigeben
                                        }
                                });

                                if (BGPlayer.BG.Where(t => !t.FadingOutStarted).FirstOrDefault(t => t.aTitel == titel) != null)
                                {
                                    MusikAktiv = BGPlayer.BG.FirstOrDefault(t => t.aTitel == titel);
                                }
                                else
                                {
                                    BGPlayer.BG.Add(new Musik());
                                    MusikAktiv = BGPlayer.BG[BGPlayer.BG.Count - 1];
                                    MusikAktiv.aPlaylist = BGPlayer.AktPlaylist;
                                }
                                MusikAktivIsPaused = false;

                                MusikAktiv.aData =
                                    PlayFile(false, null, null, MusikAktiv.aData, //.mPlayer
                                        titel.Pfad + "\\" + titel.Datei, Einstellungen.GeneralMusikVolume, true,
                                        BGPlayer.AktPlaylistTitel.TeilAbspielen? BGPlayerAktPlaylistTitelTeilStart: 0);
                                
                                if (MusikAktiv.aData.isPlaying())
                                {                                    
                                    MusikAktiv.aTitel = titel;
                                    Info_BGTitel = null;
                                    MusikProgBarTimer.Tag = -1;
                                    MusikProgBarTimer.Start();
                                }
                            }                            
                        }
                    }          
                }
            }
            else
                SelectedMusikTitelItem = null;
            OnChanged("BGPlayerAktPlaylistTitelLänge");
        }

        private void GetMusikGeneralInfo()
        {
            FileInfo file = new FileInfo(MusikAktiv.aData.getFilename());// .mPlayer.Source.LocalPath);
            Stream str = file.OpenRead();
            byte[] bytes = new byte[128];
            str.Seek(-128, SeekOrigin.End);
            int numBytesToRead = 128;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = str.Read(bytes, numBytesRead, numBytesToRead);

                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            str.Close();

            String tag = ConvertByteToString(bytes, 0, 2);
            if (tag != "TAG")
            {
                Info_BGTitel = System.IO.Path.GetFileNameWithoutExtension(MusikAktiv.aData.getFilename());
                Info_BGArtist = "---";
                Info_BGAlbum = "---";
                Info_BGJahr = "---";
                Info_BGGenre = "---";
            }
            else
            {
                string[] _genres = {
                        "Blues","Classic Rock","Country","Dance","Disco","Funk","Grunge","Hip-Hop","Jazz","Metal",
                        "New Age","Oldies","Other","Pop","R&B","Rap","Reggae","Rock","Techno","Industrial",
                        "Alternative","Ska","Death Metal","Pranks","Soundtrack","Euro-Techno","Ambient","Trip-Hop",
                        "Vocal","Jazz+Funk","Fusion","Trance","Classical","Instrumental","Acid","House",
                        "Game","Sound Clip","Gospel","Noise","Alternative Rock","Bass","Soul","Punk","Space",
                        "Meditative","Instrumental Pop","Instrumental Rock","Ethnic","Gothic",
                        "Darkwave","Techno-Industrial","Electronic","Pop-Folk","Eurodance","Dream",
                        "Southern Rock","Comedy","Cult","Gangsta","Top 40","Christian Rap","Pop/Funk","Jungle",
                        "Native American","Cabaret","New Wave","Psychadelic","Rave","Showtunes","Trailer","Lo-Fi",
                        "Tribal","Acid Punk","Acid Jazz","Polka","Retro","Musical","Rock & Roll","Hard Rock","Folk",
                        "Folk/Rock","National Folk","Swing","Fast-Fusion","Bebob","Latin","Revival","Celtic","Bluegrass",
                        "Avantgarde","Gothic Rock","Progressive Rock","Psychedelic Rock","Symphonic Rock","Slow Rock",
                        "Big Band","Chorus","Easy Listening","Acoustic","Humour","Speech","Chanson","Opera","Chamber Music",
                        "Sonata","Symphony","Booty Bass","Primus","Porn Groove","Satire","Slow Jam","Club",
                        "Tango","Samba","Folklore","Ballad","Power Ballad","Rhytmic Soul","Freestyle","Duet",
                        "Punk Rock","Drum Solo","Acapella","Euro-House","Dance Hall","Goa","Drum & Bass","Club-House",
                        "Hardcore","Terror","Indie","BritPop","Negerpunk","Polsk Punk","Beat","Christian Gangsta Rap",
                        "Heavy Metal","Black Metal","Crossover","Contemporary Christian",
                        "Christian Rock","Merengue","Salsa","Trash Metal","Anime","JPop","SynthPop"};

                string titel = ConvertByteToString(bytes, 3, 32);
                Info_BGTitel = titel != "" ? titel : System.IO.Path.GetFileNameWithoutExtension(MusikAktiv.aData.getFilename());
                Info_BGArtist = ConvertByteToString(bytes, 33, 62);
                Info_BGAlbum = ConvertByteToString(bytes, 63, 92);
                Info_BGJahr = ConvertByteToString(bytes, 93, 96);
                int z = Convert.ToInt32(bytes[127]);
                if (z <= _genres.Length - 1)
                    Info_BGGenre = _genres[z];
            }
            MusikStern1 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 1;
            MusikStern2 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 2;
            MusikStern3 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 3;
            MusikStern4 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 4;
            MusikStern5 = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.Rating >= 5;
            BGPlayerAktPlaylistTitelTeilAbspielen = BGPlayer.AktPlaylistTitel == null ? false : BGPlayer.AktPlaylistTitel.TeilAbspielen;
            
            MusikTeilMax = BGPlayer.AktPlaylistTitel.Audio_Titel.Länge == 0 ? 10000000 :
               // MusikAktiv .mPlayer.NaturalDuration.HasTimeSpan ?
                MusikAktiv.aData.getLength();// .mPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;// : 10000000;

            MusikTeilStart = BGPlayer.AktPlaylistTitel.TeilStart == null ? 0 : BGPlayer.AktPlaylistTitel.TeilStart.Value;

            MusikTeilEnde = BGPlayerAktPlaylistTitelTeilEnde == 0 && BGPlayer.AktPlaylistTitel.Audio_Titel.Länge != null ?
                BGPlayer.AktPlaylistTitel.Audio_Titel.Länge.Value :
                    BGPlayer.AktPlaylistTitel.TeilEnde != null ?
                        BGPlayer.AktPlaylistTitel.TeilEnde.Value : 10000000;
        }

        public void _datenloeschen(int mrRes, bool allesloeschen, string saveTMPdatei)
        {
            hotkeyListe.Clear();
            if (mrRes == 1)
            {
                Global.SetIsBusy(true, string.Format("Bestehende Daten werden gesichert..." + Environment.NewLine + saveTMPdatei));
                //this.UpdateLayout();
                if (Global.ContextAudio.PlaylistListe.Count > 0)
                    Global.ContextAudio.PlaylistListe[0].Export(saveTMPdatei, Global.ContextAudio.PlaylistListe[0].Audio_PlaylistGUID);
            }
            if (mrRes == 1 || allesloeschen)
            {
                Global.SetIsBusy(true, string.Format("Laufende Songs werden beendet..."));
                foreach (MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj in _GrpObjecte.FindAll(t => t.visuell))
                    AlleKlangSongsAus(grpobj, true, false, false, true);

                foreach (MusikZeile aZeile in ErwPlayerMusikListItemListe) aZeile.tbtnCheck.IsChecked = false;

                if (MusikAktiv.aData != null && MusikAktiv.aData.audioStream != 0)
                {
                    BGPlayer.NochZuSpielen.Clear();
                    BGPlayer.Gespielt.Clear();
                    BGPlayerGespieltCount = BGPlayer.Gespielt.Count;
                    BGPlayer.AktPlaylist = null;
                    BGPlayer.AktTitel.Clear();
                }

                //1.
                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.PlaylistTitelListe.Count +
                    " Titel in " + Global.ContextAudio.PlaylistListe.Count + " Playlisten)..."));
                int i = 0;
                foreach (Audio_Playlist_Titel aPlyTitel in Global.ContextAudio.PlaylistTitelListe)
                {
                    if (i == 10)
                    {
                        Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.PlaylistTitelListe.Count +
                            " Titel in " + Global.ContextAudio.PlaylistListe.Count + " Playlisten)..."));
                        i = 0;
                    }
                    Global.ContextAudio.RemoveTitelFromPlaylist(aPlyTitel);
                    i++;
                }

                //2.
                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.PlaylistListe.Count +
                    " Playlisten )..."));
                foreach (Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe)
                    Global.ContextAudio.Delete<Audio_Playlist>(aPlaylist);

                //3. ??
                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.ThemeListe.Count + " Themes)..."));
                foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe)
                {
                    foreach (Audio_Theme aTheme1 in aTheme.Audio_Theme1)
                        aTheme.Audio_Theme2.Remove(aTheme1);

                    Global.ContextAudio.Delete<Audio_Theme>(aTheme);
                }

                Global.SetIsBusy(true, string.Format("Speicher wird freigegeben" + Environment.NewLine + "(" + Global.ContextAudio.TitelListe.Count + " Titel)..."));
                foreach (Audio_Titel aTitel in Global.ContextAudio.TitelListe)
                    Global.ContextAudio.RemoveTitel(aTitel);

                Global.SetIsBusy(true, string.Format("Grundzustand wird hergestellt..."));
            }
            FadingIn_Started = null;
            stopFadingIn = false;

            hotkeyListe = new List<btnHotkey>();
            BGPlayer = new MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.MusikView();
            _GrpObjecte = new List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt>();
            AktKlangPlaylist = null;
            AktKlangTheme = null;
            BGSongTimer.Close();
            foreach (DispatcherTimer dispTmr in lstKlangPlayEndetimer)
                if (dispTmr != null) dispTmr.Stop();
            lstKlangPlayEndetimer.Clear();

            KlangProgBarTimer.Stop();
            MusikProgBarTimer.Stop();
            BGPlayer.BG.Clear();

            setStdPfad();
            fadingTime = MeisterGeister.Logic.Einstellung.Einstellungen.Fading; 
            if (Einstellungen.ShowPlaylistFavorite)
                MainViewModel.Instance.UpdateFavorites();
        }

        public void abspielProzess(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj, bool checkStatus, bool sollStandby, MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile, RoutedEventArgs e)
        {
            if (klZeile.aPlaylistTitel.Audio_Titel == null)
                return;
            Audio_Titel aTitel = AudioInAnderemPfadSuchen ? setTitelStdPfad(klZeile.aPlaylistTitel.Audio_Titel) : klZeile.aPlaylistTitel.Audio_Titel;
            setTitelStdPfad_AufrufeHintereinander = 0;
            if (aTitel.Pfad != klZeile.aPlaylistTitel.Audio_Titel.Pfad ||
                aTitel.Datei != klZeile.aPlaylistTitel.Audio_Titel.Datei)
            {
                klZeile.aPlaylistTitel.Audio_Titel = aTitel;
                Global.ContextAudio.Update<Audio_Titel>(klZeile.aPlaylistTitel.Audio_Titel);
            }
            try
            {
                if (e == null || e.Source == null)
                {
                    if (!Directory.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad) ||
                        !File.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + klZeile.aPlaylistTitel.Audio_Titel.Datei))
                    {
                        klZeile.playable = false;
                        klZeile.istLaufend = false;
                        if (klZeile.audioZeileVM != null) klZeile.audioZeileVM.FileNotExist = true;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.aPlaylistTitel.Audio_TitelGUID));
                    }
                    else
                    {
                        klZeile.playable = true;
                        if (klZeile.audioZeileVM != null) klZeile.audioZeileVM.FileNotExist = false;
                        //okay if abfrage unten
                        if (checkStatus)
                        {
                            if (grpObj.aPlaylist.MaxSongsParallel == 0 && grpObj.aPlaylist.Audio_Playlist_Titel.Count > 0)
                            {
                                grpObj.aPlaylist.MaxSongsParallel = 1;
                                Global.ContextAudio.Update<Audio_Playlist>(grpObj.aPlaylist);
                            }
                            if (grpObj.aPlaylist.MaxSongsParallel > grpObj._listZeile.FindAll(t => t.istLaufend == true).Count)
                            {
                                if (grpObj.aPlaylist.Hintergrundmusik)
                                {
                                    klZeile.FadingOutStarted = false;
                                    FadingOut_Started = null;
                                }

                                klZeile.aData =
                                    PlayFile(true, klZeile,                                         
                                        grpObj,
                                        klZeile.aData,
                                        klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + klZeile.aPlaylistTitel.Audio_Titel.Datei,
                                        klZeile.Aktuell_Volume, grpObj.aPlaylist.Hintergrundmusik,
                                        klZeile.aPlaylistTitel.TeilAbspielen ? ((klZeile.aPlaylistTitel.TeilStart != null)? klZeile.aPlaylistTitel.TeilStart.Value:0) : 0);
                                
                                //if (klZeile.aData != null)
                                //    klZeile.mediaHashCode = klZeile.aData.GetHashCode();
                                
                                if (e != null && e.Source != null) klZeile.istStandby = false;

                                klZeile.istLaufend = true;
                                klZeile.istPause = false;
                                grpObj.ListTitelLaufend = String.Join(",", grpObj._listZeile.FindAll(t => t.TitelLaufend != null).Select(t => t.TitelLaufend));
                            }
                            else
                            {
                                klZeile.istStandby = true;
                            }
                        }
                        else
                        {
                            if (klZeile.aData != null)// && klZeile._mplayer.Source != null)
                            {
                                if (grpObj.aPlaylist.Hintergrundmusik)
                                {
                                    if (!klZeile.FadingOutStarted)
                                    {
                                        klZeile.FadingOutStarted = true;
                                        FadingOut(klZeile, grpObj, true, true);
                                    }
                                }
                                else
                                {
                                    klZeile.aData.Stop();
                                    Player_Ended(klZeile.aData, null);
                                    if (!klZeile.aPlaylistTitel.TeilAbspielen)
                                        klZeile.aData.Close(); 
                                }
                                klZeile.istStandby = false;
                                klZeile.istLaufend = false;
                                klZeile.istPause = false;
                            }
                            CheckPlayStandbySongs(grpObj);
                        }
                        if (grpObj._listZeile.FindAll(t => t.istLaufend).Count > 0)
                        {
                            KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(100);
                            KlangProgBarTimer.IsEnabled = true;
                            KlangProgBarTimer.Start();
                        }
                        else
                        {
                            for (int i = 0; i < _GrpObjecte.Count; i++)
                            {
                                if (_GrpObjecte[i]._listZeile.FindAll(t => t.istLaufend).Count > 0)
                                {
                                    KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(100);
                                    KlangProgBarTimer.IsEnabled = true;
                                    KlangProgBarTimer.Start();
                                    break;
                                }
                                else
                                {
                                    KlangProgBarTimer.Interval = TimeSpan.FromMilliseconds(1000);
                                }
                            }
                        }
                    }
                    grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.aPlaylistTitel.Audio_TitelGUID));
                    
                }
                else
                {
                    klZeile.aPlaylistTitel.Aktiv = checkStatus;
                    klZeile.istStandby = checkStatus;
                    if (checkStatus)
                    {
                        klZeile.istLaufend = false;
                        if (!grpObj.NochZuSpielen.Contains(klZeile.aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= klZeile.aPlaylistTitel.Rating; i++)
                                grpObj.NochZuSpielen.Add(klZeile.aPlaylistTitel.Audio_TitelGUID);
                        }
                    }
                    else
                    {
                        if (klZeile.aData != null)
                        {
                            klZeile.aData.Stop();
                            Player_Ended(klZeile.aData, null);
                            if (!klZeile.aPlaylistTitel.TeilAbspielen)
                                klZeile.aData.Close();
                        }
                        klZeile.istLaufend = false;
                        klZeile.istPause = false;
                        grpObj.NochZuSpielen.RemoveAll(t => t.Equals(klZeile.aPlaylistTitel.Audio_TitelGUID));

                        if (!grpObj.visuell)
                        {
                            if (grpObj._listZeile.Count(t => t.istLaufend) > 0 &&
                                grpObj._listZeile.Count(t => t.istWartezeit) == 0)
                                grpObj.mZeileVM.Min1SongWirdGespielt = true;
                            else
                                if (grpObj._listZeile.Count(t => t.istWartezeit) > 0)
                                    grpObj.mZeileVM.Min1SongWirdGespielt = false;
                                else
                                    grpObj.mZeileVM.Min1SongWirdGespielt = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Datenfehler" + Environment.NewLine + "Der AbspielProzess konnte nicht ordnungsgemäß durchgeführt werden.", ex);
            }
        }

        public void chkTitel(AudioZeileVM audioZeileVM)
        {
            try
            {
                GruppenObjekt grpobj = null;
                foreach (GruppenObjekt chkgrpObj in _GrpObjecte.FindAll(t => t.visuell))
                {
                    if (chkgrpObj._listZeile.FirstOrDefault(t => t.audioZeileVM == audioZeileVM) != null) // .FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeileVM.chkTitel == (CheckBox)sender) != null)
                    {
                        grpobj = chkgrpObj;
                        break;
                    }
                }
                if (grpobj == null)
                    return;

                int zeile = grpobj._listZeile.IndexOf(
                    grpobj._listZeile.FindAll(t => t.audioZeileVM != null).FirstOrDefault(t => t.audioZeileVM == audioZeileVM)); //.chkTitel == (Control)sender));
                
                if (!grpobj._listZeile[zeile].aPlaylistTitel.Aktiv && grpobj._listZeile[zeile].istLaufend)//audioZeileVM.Checked 
                {
                    grpobj._listZeile[zeile].aData.Pause();
                    grpobj._listZeile[zeile].aData.setPosition(0);
                    grpobj._listZeile[zeile].istLaufend = false;
                }

                if ((grpobj.visuell && grpobj.wirdAbgespielt || !grpobj.visuell) ||
                    (grpobj.visuell && !grpobj.wirdAbgespielt && grpobj.tbtnKlangPause != null &&  grpobj.tbtnKlangPause.IsChecked.Value))
                    abspielProzess(grpobj, grpobj._listZeile[zeile].aPlaylistTitel.Aktiv , grpobj.wirdAbgespielt, grpobj._listZeile[zeile], null); //audioZeileVM.Checked

                //if (grpobj.aPlaylist != null)
                //{
                //    Audio_Playlist_Titel playlisttitel =
                //        grpobj.aPlaylist.Audio_Playlist_Titel.Where(t => t.Audio_TitelGUID ==
                //            grpobj._listZeile[zeile].aPlaylistTitel.Audio_TitelGUID).FirstOrDefault(
                //            t => t.Aktiv != audioZeileVM.Checked);

                //    if (playlisttitel != null)
                //        playlisttitel.Aktiv = audioZeileVM.Checked;
                //}
                if (grpobj.visuell && grpobj.wirdAbgespielt) CheckPlayStandbySongs(grpobj);
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Auswahlfehler" + Environment.NewLine + "Beim der Prozedure 'chkTitel' ist ein Fehler aufgetreten", ex);
            }
        }

        public void CheckPlayStandbySongs(GruppenObjekt grpobj)
        {
            int titel = -1;
            try
            {
                int laufende = grpobj._listZeile.FindAll(t => t.istLaufend).Count;
                if (!grpobj.visuell)
                {
                    if (grpobj._listZeile.Count(t => t.istLaufend) > 0 &&
                        grpobj._listZeile.Count(t => t.istWartezeit) == 0)
                        grpobj.mZeileVM.Min1SongWirdGespielt = true;
                    else
                        if (grpobj._listZeile.Count(t => t.istWartezeit) > 0)
                            grpobj.mZeileVM.Min1SongWirdGespielt = false;
                        else
                            grpobj.mZeileVM.Min1SongWirdGespielt = null;
                }
                
                List<KlangZeile> klZeilenAktivStandbyNichtPause = grpobj._listZeile.Where(t0 => t0.aPlaylistTitel.Aktiv)
                    .Where(t1 => t1.playable).Where(t => t.istStandby).ToList().FindAll(t => t.istPause == false);
                int standbyNichtPausePlayable = klZeilenAktivStandbyNichtPause.Count;
                titel = 0;
                if ((laufende == 0 && standbyNichtPausePlayable != 0) ||
                   (laufende != 0 && standbyNichtPausePlayable != 0 && grpobj.aPlaylist.MaxSongsParallel > laufende))
                {
                    int neueSongs = (laufende == 0) ? grpobj.aPlaylist.MaxSongsParallel :
                        grpobj.aPlaylist.MaxSongsParallel - laufende;

                    if (neueSongs < 0)
                        neueSongs = 0;

                    if (neueSongs == 0 && grpobj.aPlaylist.MaxSongsParallel == 0)
                        neueSongs = 1;

                    for (int i = 0; i < neueSongs; i++)
                    {
                        if (standbyNichtPausePlayable >= 1)
                        {
                            if (grpobj.NochZuSpielen.Count == 0)
                            {
                                for (int x = 0; x < standbyNichtPausePlayable; x++)
                                {
                                    if (//klZeilenStandbyNichtPause[x].audioZeileVM == null &&
                                        klZeilenAktivStandbyNichtPause[x].istStandby &&
                                        klZeilenAktivStandbyNichtPause[x].aPlaylistTitel.Aktiv)
                                    {
                                        for (int t = 0; t <= klZeilenAktivStandbyNichtPause[x].aPlaylistTitel.Rating; t++)
                                            grpobj.NochZuSpielen.Add(klZeilenAktivStandbyNichtPause[x].aPlaylistTitel.Audio_TitelGUID);
                                    }
                                }
                            }

                            if (grpobj.aPlaylist.Hintergrundmusik)
                            {
                                if (grpobj.NochZuSpielen.Count > 0)
                                {
                                    int neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.aPlaylistTitel.Audio_TitelGUID == zuspielendeGuid);
                                    grpobj._listZeile[posZeile].istStandby = false;

                                    // Titel anstarten
                                    if (!grpobj.visuell)
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].aPlaylistTitel.Aktiv, grpobj.wirdAbgespielt, grpobj._listZeile[posZeile], null);
                                    else
                                    {
                                        if (grpobj._listZeile[posZeile].audioZeileVM != null)
                                            chkTitel(grpobj._listZeile[posZeile].audioZeileVM);
                                        else
                                            grpobj._listZeile[posZeile].istStandby = true;
                                    }
                                    standbyNichtPausePlayable--;
                                    if (neuPos < grpobj.NochZuSpielen.Count)
                                    {
                                        klZeilenAktivStandbyNichtPause.Remove(grpobj._listZeile[posZeile]);
                                        grpobj.NochZuSpielen.RemoveAll(t => t.Equals(zuspielendeGuid));
                                    }
                                }
                            }
                            else
                            {
                                titel = 3;
                                if (grpobj.NochZuSpielen.Count > 0 &&
                                    grpobj._listZeile.Where(z => z.playable).ToList().FindAll(t => t.istStandby).Count > 0)
                                {
                                    int neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);

                                    if (grpobj.NochZuSpielen.Count > 1)
                                    {
                                        int loops = 0;
                                        while (!grpobj._listZeile.First(t => t.aPlaylistTitel.Audio_TitelGUID == grpobj.NochZuSpielen[neuPos]).istStandby &&
                                               (grpobj._listZeile.FindAll(t => t.istStandby).Count != 0) && 
                                               loops < 10 * grpobj.NochZuSpielen.Count)                         // sicher gehen, dass kein unendlich-Loop entsteht
                                        {
                                            neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                            loops++;
                                        }
                                        if (loops > 10 * grpobj.NochZuSpielen.Count)
                                            neuPos = (new Random()).Next(0, grpobj.NochZuSpielen.Count);
                                    }

                                    titel = 4;
                                    if (grpobj.NochZuSpielen.Count == 0)  //dürfte nie der Fall sein - wird weiter oben schon abgefragt
                                    {
                                        titel = 5;
                                        break;
                                    }
                                    Guid zuspielendeGuid = grpobj.NochZuSpielen[neuPos];
                                    titel = 6;
                                    int posZeile = grpobj._listZeile.FindIndex(t => t.aPlaylistTitel.Audio_TitelGUID == zuspielendeGuid); //ToList().Where(z => z.playable).

                                    titel = 7;
                                    if (posZeile == -1 || posZeile > grpobj._listZeile.Count-1)
                                    {
                                        titel = 10;
                                        standbyNichtPausePlayable--;
                                        break;
                                    }
                                    grpobj._listZeile[posZeile].istStandby = false;
                                    if (!grpobj.visuell)
                                    {
                                        titel = 8;
                                        abspielProzess(grpobj, grpobj._listZeile[posZeile].aPlaylistTitel.Aktiv, grpobj.wirdAbgespielt, grpobj._listZeile[posZeile], null);

                                        titel = 9;
                                    }
                                    else
                                    {
                                        // Titel anstarten
                                        if (grpobj._listZeile[posZeile].audioZeileVM != null)
                                            chkTitel(grpobj._listZeile[posZeile].audioZeileVM);
                                        else
                                            grpobj._listZeile[posZeile].istStandby = true;
                                    }
                                    standbyNichtPausePlayable--;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Beim Überprüfen der StandyBy-Songs ist eine Fehler aufgetreten: Datenfehler" + Environment.NewLine + "titel=" + titel, ex);
            }
        }

        public void AlleKlangSongsAus(GruppenObjekt grpobj, bool erwPlayerAus , bool checkboxAus, bool ZeileLoeschen, bool sofortAus)
        {
            if (erwPlayerAus)
                ErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile mZeile)
                {
                    if (mZeile.tbtnCheck.IsChecked.Value)
                    {
                        mZeile.tbtnCheck.IsChecked = false;
                        mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                    }
                });

            if (grpobj == null || !grpobj.wirdAbgespielt)
                return;

            List<KlangZeile> laufendeKZeilen = grpobj._listZeile.FindAll(t => t.istLaufend == true).ToList();
            
            laufendeKZeilen.ForEach(delegate(KlangZeile kZeile)
            {
                if (kZeile.aData != null)
                {

                    if (!grpobj.aPlaylist.Hintergrundmusik || sofortAus)
                    {
                     //   kZeile._mplayer.MediaEnded -= Player_Ended;
                        kZeile.aData.Stop();
                        Player_Ended(kZeile.aData, null);
                        if (ZeileLoeschen)
                            kZeile.aData.Close();
                    }
                    else
                    {
                        if (!kZeile.FadingOutStarted)
                        {
                            kZeile.FadingOutStarted = true;
                            FadingOut(kZeile, grpobj, true, true);
                        }
                    }
                }
                kZeile.istLaufend = false;
                kZeile.istPause = false;

                kZeile.audioZeileVM.TitelMaximum = 100;
                kZeile.audioZeileVM.TitelMinimum = 0;

                if (ZeileLoeschen)
                {
                    grpobj.lbEditorListe.Items.Clear();
                    grpobj._listZeile.Clear();
                }
            });
            grpobj.wirdAbgespielt = false;
            if (!grpobj.visuell)
                grpobj.mZeileVM.Min1SongWirdGespielt = null;

            GC.GetTotalMemory(true);                //GC update (Memory wird aktualisiert)
        }

        public void BGStoppen()
        {
            try
            {
                if (!MusikAktiv.FadingOutStarted)
                {
                    MusikAktivIsPaused = false;
                    if (MusikAktiv.aData != null && MusikAktiv.aData.isPlaying() &&
                        !MusikAktiv.FadingOutStarted)
                    {
                        MusikAktiv.FadingOutStarted = true;
                        BGFadingOut(MusikAktiv, true, true);
                    }
                }
                MusikProgBarTimer.Stop();
                MusikAktivIsPaused = true;
                MusikAktiv.aPlaylist = null;
                //BGPlayeraktiv = BGPlayer.BG.FindIndex(t => !t.FadingOutStarted);
                //BGPlayeraktiv = (BGPlayeraktiv == 0) ? 1 : 0;
                MusikAktiv = BGPlayer.BG.FirstOrDefault(t => !t.FadingOutStarted);

                RenewMusikNochZuSpielen();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgemeiner Fehler" + Environment.NewLine + "Beim Auswählen des Stop-Buttons ist ein Fehler aufgetreten.", ex);
            }
        }

        public void PlaylisteLeeren(GruppenObjekt grpobj)
        {
            if (grpobj == null)
                return;

            if (AktKlangPlaylist != null)
            {
                if (BGPlayer != null && BGPlayer.AktPlaylist == AktKlangPlaylist && grpobj.wirdAbgespielt)
                    BGStoppen();

                AlleKlangSongsAus(grpobj, true, true, true, true);
                grpobj.wirdAbgespielt = false;
            }
            if (grpobj._listZeile.Count > 0)
            {
                grpobj.lbEditorListe.Items.Clear();
                grpobj._listZeile.RemoveRange(0, grpobj._listZeile.Count);
            }

            grpobj.anzVolChange = 0;
            grpobj.anzPauseChange = 0;
            grpobj.NochZuSpielen.Clear();
            grpobj.Gespielt.Clear();
        }
        
        public void sortPlaylist(Audio_Playlist aPlaylist, int abPos)
        {
            int reihe = abPos < 0 ? 0 : abPos;
            foreach (Audio_Playlist_Titel playlisttitel in aPlaylist.Audio_Playlist_Titel.Where(t => t.Reihenfolge >= abPos).OrderBy(t => t.Reihenfolge))
            {
                if (playlisttitel.Reihenfolge != reihe)
                {
                    playlisttitel.Reihenfolge = reihe;
                    Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel);
                }
                reihe++;
            }
        }

        public void sortPlaylist(List<lbEditorItemVM> listLbi, int abPos)
        {
            int reihe = abPos < 0 ? 0 : abPos;
            foreach (lbEditorItemVM lbi in listLbi.Where(t => t.APlaylist.Reihenfolge >= abPos).OrderBy(t => t.APlaylist.Reihenfolge))
            {
                if (lbi.APlaylist.Reihenfolge != reihe)
                {
                    lbi.APlaylist.Reihenfolge = reihe;
                    Global.ContextAudio.Update<Audio_Playlist>(lbi.APlaylist);
                }
                reihe++;
            }
        }

        public bool KlangDateiHinzu(string pfad_datei, AudioZeile aZeile, AudioZeileVM aZeileVM, Audio_Playlist aPlaylist, int position)
        {
            bool titelGuidInPlaylist = false;
            bool titelNeuHinzugefügt = false;
            bool found = false;
            string pfad_max = System.IO.Path.GetDirectoryName(pfad_datei);
            string[] temp_pfad_datei = new string[] { pfad_max, System.IO.Path.GetFileName(pfad_datei) };

            foreach (string pfad in stdPfad)
            {
                if (pfad != "" && pfad_max.ToLower().StartsWith(pfad.ToLower()))
                {
                    if (pfad_max != pfad)
                        temp_pfad_datei[1] = pfad_max.Substring(pfad.EndsWith(@"\") ? pfad.Length : pfad.Length + 1) + "\\" + temp_pfad_datei[1];
                    temp_pfad_datei[0] = pfad_max.Substring(0, pfad.Length);
                    found = true;
                }
            }

            if (!found && temp_pfad_datei[0].Length <= 200)
            {
                // Pfad noch kein Standard-Pfad -> Neuer Standard-Pfad wird so groß als möglich!
                if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
                    Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
                    "Soll der Pfad mit in die Standard-Pfade integriert werden?"))
                {
                    List<string> allSamePfad = MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }).ToList().FindAll(t => temp_pfad_datei[0].StartsWith(t));
                    if (allSamePfad != null)
                    {
                        string maxSamePfad = allSamePfad.Max();
                        temp_pfad_datei[0] = maxSamePfad;
                    }

                    MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
                        MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + temp_pfad_datei[0];
                    setStdPfad();
                }
                else
                    return false;
            }
            Audio_Titel titel = Global.ContextAudio.TitelListe.FindAll(t => t.Pfad == temp_pfad_datei[0]).FirstOrDefault(tt => tt.Datei == temp_pfad_datei[1]);

            if (titel != null && aPlaylist != null)
                titelGuidInPlaylist = aPlaylist.Audio_Playlist_Titel.FirstOrDefault(t => t.Audio_TitelGUID == titel.Audio_TitelGUID) == null ? false : true;

            int confYNC = -1;

            if ((aZeile != null && aZeile.chkTitel.ContextMenu != null && aZeile.chkTitel.ContextMenu.IsVisible) || aZeileVM != null)
                confYNC = 2;
            else
                if (titel != null && titelGuidInPlaylist && Keyboard.Modifiers == ModifierKeys.Control)
                    confYNC = ViewHelper.ConfirmYesNoCancel("Titel schon vorhanden", "Den Titel '" + titel.Name + "' gibt es schon in der Playliste." +
                        Environment.NewLine + Environment.NewLine + "Soll der Titel mehrfach in der Playliste aufgeführt werden?");

            if (titel == null || confYNC == 2)          //nicht in DB oder Neuen Titel in die Playliste ODER Verschieben
            {
                titelNeuHinzugefügt = true;
                titel = Global.ContextAudio.New<Audio_Titel>();     //erstelle ein leeres Titel-Objekt

                //eigenschaften setzen
                string s = System.IO.Path.GetFileNameWithoutExtension(temp_pfad_datei[1]);
                titel.Name = s.Length > 100 ? s.Substring(0, 99) : s;

                titel.Pfad = temp_pfad_datei[0];
                if (titel.Pfad.Length > 200)
                {
                    ViewHelper.ShowError("Dateistruktur zu groß" + Environment.NewLine + "Die Datei '" + titel.Pfad + "\\" + titel.Datei +
                        "' kann nicht interiert werden, da der Pfad zu komplex ist (Länge)." + Environment.NewLine + Environment.NewLine +
                        "Bitte kopieren Sie die Datei in einen weniger komplexen Bereich.", new Exception());
                    titel.Pfad = titel.Pfad.Substring(0, 199);
                }
                titel.Datei = temp_pfad_datei[1];

                //zur datenbank hinzufügen
                Global.ContextAudio.Insert<Audio_Titel>(titel);
            }
            if (titel != null && !titelGuidInPlaylist)
                titelNeuHinzugefügt = true;

            if (titelNeuHinzugefügt)
            {
                Global.ContextAudio.AddTitelToPlaylist(aPlaylist, titel);

                Audio_Playlist_Titel playlisttitel = Global.ContextAudio.LoadPlaylist_TitelByPlaylist(aPlaylist, titel).Last();
                if (playlisttitel != null)
                {
                    //AudioZeile per Drag&Drop hinzugefügt
                    if (aZeile != null || aZeileVM != null) 
                    {
                        if (aZeile != null)
                            aZeile.rsldTeilSong.PlayTitel = playlisttitel;

                        Audio_Playlist_Titel aplaytitelDnD =
                            aZeile != null ? (Audio_Playlist_Titel)(aZeile.Tag) :
                            aZeileVM.aPlayTitel;

                        if (aplaytitelDnD.Audio_Titel.Länge == null)
                            aplaytitelDnD.Audio_Titel.Länge = getTitelLänge(playlisttitel.Audio_Titel);
                                                
                        playlisttitel.Audio_Titel.Länge = aplaytitelDnD.Audio_Titel.Länge;
                        playlisttitel.Aktiv = aplaytitelDnD.Aktiv;
                        playlisttitel.Länge = aplaytitelDnD.Länge;
                        playlisttitel.Pause = aplaytitelDnD.Pause;
                        playlisttitel.PauseChange = aplaytitelDnD.PauseChange;
                        playlisttitel.PauseMax = aplaytitelDnD.PauseMax;
                        playlisttitel.PauseMin = aplaytitelDnD.PauseMin;
                        playlisttitel.Rating = aplaytitelDnD.Rating;
                        playlisttitel.Speed = aplaytitelDnD.Speed;
                        playlisttitel.TeilAbspielen = aplaytitelDnD.TeilAbspielen;
                        playlisttitel.TeilEnde = aplaytitelDnD.TeilEnde;
                        playlisttitel.TeilStart = aplaytitelDnD.TeilStart;
                        playlisttitel.Volume = aplaytitelDnD.Volume;
                        playlisttitel.VolumeChange = aplaytitelDnD.VolumeChange;
                        playlisttitel.VolumeMax = aplaytitelDnD.VolumeMax;
                        playlisttitel.VolumeMin = aplaytitelDnD.VolumeMin;
                        playlisttitel.Reihenfolge = playlisttitel.Audio_Playlist.Audio_Playlist_Titel.Count - 1;
                    }
                    else
                    {
                        playlisttitel.VolumeChange = false;
                        playlisttitel.Volume = 50;
                        playlisttitel.VolumeMin = 0;
                        playlisttitel.VolumeMax = 100;
                        playlisttitel.PauseChange = false;
                        playlisttitel.Pause = 1000;
                        playlisttitel.PauseMin = 100;
                        playlisttitel.PauseMax = 10000;
                        playlisttitel.Speed = 1;
                        playlisttitel.Reihenfolge = AktKlangPlaylist.Audio_Playlist_Titel.Count - 1;
                        if (playlisttitel.Audio_Titel.Länge == null)
                            playlisttitel.Audio_Titel.Länge = getTitelLänge(playlisttitel.Audio_Titel);
                    }
                    sortPlaylist(playlisttitel.Audio_Playlist, -1);
                    Global.ContextAudio.Update<Audio_Playlist_Titel>(playlisttitel);

                    LadeAudioZeilen();
                    KlangNewRow(_GrpObjecte.First(t => t.visuell), playlisttitel);
                }
            }
            return titelNeuHinzugefügt;
        }
        
        public void UpdatePlaylistLänge(Audio_Playlist aPlaylist)
        {
            double gesamt = 0;
            foreach (Audio_Playlist_Titel aPlyTitel in aPlaylist.Audio_Playlist_Titel)
                gesamt += aPlyTitel.Audio_Titel.Länge != null ? aPlyTitel.Audio_Titel.Länge.Value : 0;

            if (aPlaylist.Länge != gesamt)
            {
                aPlaylist.Länge = gesamt;
                Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);                
            }
        }

        public void KlangNewRow(GruppenObjekt grpobj, Audio_Playlist_Titel playlisttitel)
        {
            string songdatei = playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;
            if (grpobj == null)
                return;
            int objGruppe = grpobj.objGruppe;


            KlangZeile klZeile = new KlangZeile(rowErstellt);
            
            klZeile.aPlaylistTitel = playlisttitel;
            
            klZeile.mediaHashCode = klZeile.aData.GetHashCode();

            if (grpobj.visuell)
            {
                klZeile.audioZeileVM = LbEditorAudioZeilenListe.First(t => t.aPlayTitel.Audio_Titel.Datei == playlisttitel.Audio_Titel.Datei);//aPlayTitel == playlisttitel);
                klZeile.audioZeileVM.grpobj = grpobj;
                if (AudioInAnderemPfadSuchen &&
                    !File.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + (klZeile.aPlaylistTitel.Audio_Titel.Datei == null ? "" : klZeile.aPlaylistTitel.Audio_Titel.Datei)))
                {
                    klZeile.aPlaylistTitel.Audio_Titel = setTitelStdPfad(klZeile.aPlaylistTitel.Audio_Titel);
                    setTitelStdPfad_AufrufeHintereinander = 0;
                    if (File.Exists(klZeile.aPlaylistTitel.Audio_Titel.Pfad + "\\" + klZeile.aPlaylistTitel.Audio_Titel.Datei))
                        Global.ContextAudio.Update<Audio_Titel>(klZeile.aPlaylistTitel.Audio_Titel);
                }
            }

            klZeile.pauseMin_wert = Convert.ToInt32(playlisttitel.PauseMin);
            klZeile.pauseMax_wert = Convert.ToInt32(playlisttitel.PauseMax);
            klZeile.volMin_wert = Convert.ToInt32(playlisttitel.VolumeMin);
            klZeile.volMax_wert = (Convert.ToInt32(playlisttitel.VolumeMax) >= klZeile.volMin_wert) ?
                Convert.ToInt16(playlisttitel.VolumeMax) : klZeile.volMin_wert;
            klZeile.Aktuell_Volume = playlisttitel.Volume;
            klZeile.Vol_jump = (klZeile.Vol_jump < 1 || klZeile.Vol_jump > 3) ? 1 :
                (klZeile.volMax_wert - klZeile.volMin_wert) / SliderTeile;

            //klZeile.playspeed = playlisttitel.Speed;

            if (playlisttitel.Aktiv && !grpobj.aPlaylist.Hintergrundmusik)
                klZeile.istStandby = true;
            else
                klZeile.istStandby = false;

            if (!grpobj.wirdAbgespielt)
                klZeile.istPause = true;

            klZeile.playable = playlisttitel.Aktiv;

            grpobj._listZeile.Add(klZeile);
            if (playlisttitel.VolumeChange) grpobj.anzVolChange++;
            if (playlisttitel.PauseChange) grpobj.anzPauseChange++;
            rowErstellt++;
        }

        // behält die Reihenfolge bei
        private static List<string> ohneDoppelte(List<string> stringList)
        {
            // Dictionary das mitzählt, wie oft ein Element bereits vorkam
            Dictionary<string, int> stringOccurence = new Dictionary<string, int>();
            // Mit 0 initialisieren
            foreach (string s in stringList)
                stringOccurence[s] = 0;

            // Kopie erzeugen
            List<string> result = new List<string>(stringList);
            // Alle Elemente entfernen, die vorher schonmal aufgetreten sind (und dabei mitzählen, dass sie aufgetreten sind)
            result.RemoveAll(x => (stringOccurence[x]++ > 0));
            return result;
        } 


        public void setStdPfad()
        {
            char[] charsToTrim = { '\\' };
            if (stdPfad.Count > 0) stdPfad.RemoveRange(0, stdPfad.Count);
            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
            stdPfad = ohneDoppelte(stdPfad);
            if (String.Join("|", stdPfad) != MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis)
                MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis = String.Join("|", stdPfad);
        }

        public void UpdateHotkeyUsed()
        {
            if (Global.ContextAudio.PlaylistListe == null) return;
            List<btnHotkey> lstHotKeyUsed = new List<btnHotkey>();

            foreach(Audio_Playlist aPlaylist in Global.ContextAudio.PlaylistListe.FindAll(t => t.Key != null).OrderBy(tt => tt.Key))
            {
                btnHotkey hkey = new btnHotkey();
               // hkey.VM.AudioVM = this;
                hkey.VM.aPlaylistGuid = aPlaylist.Audio_PlaylistGUID;
                hkey.VM.taste = (char)aPlaylist.Key[0];
                hkey.VM.aPlaylist = aPlaylist;
                lstHotKeyUsed.Add(hkey);
            };

            hotkeyListUsed = lstHotKeyUsed;
            IsAuswahlHotkey = false;
        }
        
        public void CheckBtnGleicherPfad(Audio_Playlist aPlaylist)
        {
            List<Audio_Titel> titelliste = Global.ContextAudio.LoadTitelByPlaylist(aPlaylist);
            if (titelliste.Count > 0 && 
                System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(titelliste[0].Pfad + (titelliste[0].Pfad !=""?"\\":"") + titelliste[0].Datei)))
            {
                string titelRef = System.IO.Path.GetDirectoryName(titelliste[0].Pfad + (titelliste[0].Pfad != "" ? "\\" : "") + titelliste[0].Datei);

                titelliste.ForEach(delegate(Audio_Titel aTitel)
                {
                    string vergleich = System.IO.Path.GetDirectoryName(aTitel.Pfad + (aTitel.Pfad != "" ? "\\" : "") + aTitel.Datei);

                    while (!vergleich.StartsWith(titelRef))
                    {
                        if (titelRef.Contains(@"\"))
                            titelRef = titelRef.Substring(0, titelRef.LastIndexOf(@"\"));
                        else break;
                    }
                });
                _chkAnzDateienInDir(aPlaylist);
            }
        }

        public void _chkAnzDateienInDir(Audio_Playlist aPlaylist)
        {
            if (!Global.IsInitialized)
                return;
            ChkAnzDateienVerfügbar = false;
            if (_chkAnzDateien._bkworker.IsBusy)
            {
                _chkAnzDateien._bkworker.CancelAsync();
                _chkAnzDateien._bkworker.Dispose();
            }
            _chkAnzDateien.aPlaylist = aPlaylist;
            _chkAnzDateien._bkworker = new BackgroundWorker();
            _chkAnzDateien._bkworker.WorkerReportsProgress = true;
            _chkAnzDateien._bkworker.WorkerSupportsCancellation = true;
            _chkAnzDateien._bkworker.DoWork += new DoWorkEventHandler(_bkworkerCHKAnzDateien_DoWork);
            _chkAnzDateien._bkworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkworkerCHKAnzDateien_RunWorkerCompleted);
            _chkAnzDateien._bkworker.RunWorkerAsync();
        }

        public void _bkworkerCHKAnzDateien_DoWork(object sender, DoWorkEventArgs e)
        {
            _chkAnzDateien.titelliste = Global.ContextAudio.LoadTitelByPlaylist(_chkAnzDateien.aPlaylist);

            if (_chkAnzDateien.titelliste != null && _chkAnzDateien.titelliste.Count > 0)
            {
                _chkAnzDateien.allFilesMP3.Clear();
                _chkAnzDateien.allFilesWAV.Clear();
                _chkAnzDateien.allFilesOGG.Clear();
                _chkAnzDateien.allFilesWMA.Clear();

                if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(_chkAnzDateien.titelliste[0].Pfad + "\\" + _chkAnzDateien.titelliste[0].Datei)))
                {
                    _chkAnzDateien.titelRef = System.IO.Path.GetDirectoryName(_chkAnzDateien.titelliste[0].Pfad + "\\" + _chkAnzDateien.titelliste[0].Datei);

                    _chkAnzDateien.titelliste.ForEach(delegate(Audio_Titel aTitel)
                    {
                        string vergleich = System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei);

                        while (!vergleich.StartsWith(_chkAnzDateien.titelRef))
                        {
                            if (_chkAnzDateien.titelRef.Contains(@"\"))
                                _chkAnzDateien.titelRef = _chkAnzDateien.titelRef.Substring(0, _chkAnzDateien.titelRef.LastIndexOf(@"\"));
                            else break;
                        }
                    });
                    if (Directory.Exists(_chkAnzDateien.titelRef))
                    {
                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.mp3", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesMP3.Add(datei);

                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.wav", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesWAV.Add(datei);

                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.ogg", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesOGG.Add(datei);

                        foreach (string datei in Directory.GetFiles(_chkAnzDateien.titelRef, "*.wma", SearchOption.AllDirectories))
                            if (_chkAnzDateien.titelliste.FirstOrDefault(t => t.Pfad + "\\" + t.Datei == datei) == null)
                                _chkAnzDateien.allFilesWMA.Add(datei);

                        if (_chkAnzDateien.aPlaylist != _aktKlangPlaylist) return;
                    }
                }
            }
        }

        public void _bkworkerCHKAnzDateien_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ChkAnzDateienVerfügbar = true;
                if (e.Error == null)
                {
                    int all = _chkAnzDateien.allFilesMP3.Count + _chkAnzDateien.allFilesOGG.Count +
                              _chkAnzDateien.allFilesWAV.Count + _chkAnzDateien.allFilesWMA.Count;
                    if (_chkAnzDateien.aPlaylist == AktKlangPlaylist &&
                        System.IO.Path.IsPathRooted(_chkAnzDateien.titelRef) &&
                        Directory.Exists(_chkAnzDateien.titelRef) &&
                        all > 0)
                    {
                        ChkAnzDateienResult =
                            _chkAnzDateien.titelRef + Environment.NewLine + "Update der Titel im o.g. Verzeichnis" + Environment.NewLine +
                            _chkAnzDateien.titelliste.Count + " Dateien sind in der Playlist vorhanden." + Environment.NewLine +
                            all + " neue Sound-Dateien wurden incl. den Unterverzeichnisse gefunden" + Environment.NewLine + Environment.NewLine +
                            _chkAnzDateien.allFilesMP3.Count + " neue MP3-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesOGG.Count + " neue OGG-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesWAV.Count + " neue WAV-Dateien gefunden." + Environment.NewLine +
                            _chkAnzDateien.allFilesWMA.Count + " neue WMA-Dateien gefunden." + Environment.NewLine + Environment.NewLine +
                            "Klicken um alle Dateien neu gefundenen Dateien zu integrieren.";
                    }
                    else
                        ChkAnzDateienResult = null;
                }
                else
                    ChkAnzDateienResult = null;
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                ChkAnzDateienVerfügbar = true;
                (sender as BackgroundWorker).Dispose();
                ChkAnzDateienResult = null;
            }
        }

        public void _DateienAufnehmen(List<string> files, AudioZeile aZeile, AudioZeileVM aZeileVM, Audio_Playlist aPlaylist, int position, bool jetztUpdaten)
        {
            string[] extension = new String[4] { ".mp3", ".wav", ".wma", ".ogg" };
            bool hinzugefuegt = false;

            // AktKlangPlaylist == null also neue Playliste anlegen
            if (aPlaylist == null)
                aPlaylist = NeueKlangPlaylistInDB(NeuerPlaylistName); //NeueKlangPlaylistInDB(AktKlangPlaylist.Name);

            if (files != null && files.Count >= 0)
            foreach (string dateihinzu in files)
            {
                if (Array.IndexOf(extension, dateihinzu.ToLower().Substring(dateihinzu.Length - 4)) != -1)
                {
                    KlangDateiHinzu(dateihinzu, aZeile, aZeileVM, aPlaylist, position);
                    hinzugefuegt = true;
                }
                else
                {
                    // Winamp-Datei
                    if (dateihinzu.ToLower().EndsWith(".m3u8"))
                    {
                        _DateienAufnehmen(getWinampFilesFromPlaylist(dateihinzu), null, null, aPlaylist, 0, true);
                        hinzugefuegt = true;
                    }
                    else
                    {
                        if (dateihinzu.ToLower().EndsWith(".wpl"))
                        {
                            _DateienAufnehmen(getMPlayerFilesFromPlaylist(dateihinzu), null, null, aPlaylist, 0, true);
                            hinzugefuegt = true;
                        }
                    }
                }
            }
            if (hinzugefuegt)
            {
                if (aPlaylist == AktKlangPlaylist)
                {
                    if (SelectedEditorItem == null || SelectedEditorItem.APlaylist != aPlaylist)
                        SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == aPlaylist);
                    else
                        AktKlangPlaylist = AktKlangPlaylist;
                }
                else
                    if (jetztUpdaten)
                        Global.ContextAudio.Update<Audio_Playlist>(aPlaylist);
            }
        }
        
        private void AlleThemesExportieren(string dlgFolder)
        {
            Global.SetIsBusy(true, string.Format("Alle Themes werden exportiert ..."));

            foreach (Audio_Theme aTheme in Global.ContextAudio.ThemeListe.
                                Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
            {
                Global.SetIsBusy(true, string.Format("Theme '" +  ViewHelper.GetValidFilename(aTheme.Name) + "' wird exportiert"));
                string pfaddatei = dlgFolder + "\\Theme_" + ViewHelper.GetValidFilename(aTheme.Name) + ".xml";
                
                ExportTheme(aTheme, pfaddatei);
            }

            Global.SetIsBusy(true, string.Format("Theme Export beendet ..."));
        }

        private void DeleteAll(int mrRes)
        {
            _datenloeschen(mrRes, true, "");

            Global.SetIsBusy(true, string.Format("Datenbank wird gesichert..."));
            Global.ContextAudio.UpdateList<Audio_Titel>();
            Global.ContextAudio.UpdateList<Audio_Playlist>();
            Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
            Global.ContextAudio.UpdateList<Audio_Theme>();
            Global.ContextAudio.UpdateList<Audio_Theme_Playlist>();

            Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));
            AktualisiereHotKeys();
            Global.SetIsBusy(false);

            Init();
        }

        private void ThemeImportieren(List<string> dateien, bool ohnePlaylistenImport=false)
        {
            bool _nicht_first = false;
            bool allePlaylistenÜberspringen = false;
            bool allePlaylistenImportieren = false;
            bool einePlaylisteImportieren = false;
            foreach (string pfad in dateien)
            {
                Global.SetIsBusy(true, string.Format("Neues Theme  '" + ViewHelper.GetValidFilename(System.IO.Path.GetFileNameWithoutExtension(pfad)) + "'  wird importiert ..."));

                
                XmlTextReader textReader = new XmlTextReader(pfad);
                textReader.Read();
                while (textReader.NodeType == XmlNodeType.XmlDeclaration ||
                    (textReader.NodeType == XmlNodeType.Element && textReader.Name == "Audio_Theme"))
                    textReader.Read();
                if (textReader.Name == "Audio_Playlist")
                    continue;
                if (textReader.NodeType != XmlNodeType.Comment)
                {
                    textReader.Read();
                    XmlDocument doc = new XmlDocument();
                    XmlNode node = doc.ReadNode(textReader);
                    if (node.Attributes.Count > 0 && node.Attributes["Audio_ThemeGUID"] != null &&
                        node.Attributes["Audio_ThemeGUID"].Value == "00000000-0000-0000-0000-00000000a11e")
                        break;

                    if (ViewHelper.ConfirmYesNoCancel("Unsicherer Verlauf", "Theme-Import:  " + System.IO.Path.GetFileNameWithoutExtension(pfad) + Environment.NewLine + Environment.NewLine +
                        "ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
                        "Leider konnte dieser Prozess noch NICHT ZUVERLÄSSIG programmiert werden." + Environment.NewLine +
                        Environment.NewLine + "Es muss damit gerechtnet werden, das die exportierte Datei" + Environment.NewLine + "NICHT MEHR IMPORTIERT werden kann!" +
                        Environment.NewLine + Environment.NewLine + "Soll der Vorgang trotzdem fortgesetzt werden?") != 2)
                    {
                        Global.SetIsBusy(false);
                        return;
                    }

                    Audio_Playlist.Import(pfad, "Audio_Theme", _nicht_first);
                    _nicht_first = true;
                    Global.ContextAudio.Save();
                }
                else
                {
                    string thName = "";
                    List<string> aPlayListsName = new List<string>();
                    List<string> aPlayListsGuid = new List<string>();
                    List<string> aThemesName = new List<string>();
                    List<string> aThemesGuid = new List<string>();
                    List<string> lstNotInclude = new List<string>();
                    bool nurGeräusch = false;
                    while (textReader.Read())
                    {
                        XmlDocument doc = new XmlDocument();
                        if (textReader.NodeType == XmlNodeType.Element && textReader.Name == "Themename")
                        {
                            thName = textReader.NamespaceURI;

                            for (int i = 0; i < textReader.AttributeCount; i++)
                            {
                                if (textReader.Name == "IstNurGeräuschTheme")
                                {
                                    nurGeräusch = Convert.ToBoolean(textReader.Value);
                                    break;
                                }
                                textReader.MoveToNextAttribute();
                            }

                            textReader.Read();
                            while (textReader.NodeType != XmlNodeType.EndElement &&
                                   (textReader.NodeType == XmlNodeType.Element &&
                                    textReader.Name.StartsWith("Playlist") || 
                                    textReader.Name.StartsWith("Theme")))
                            {
                                XmlNode node = doc.ReadNode(textReader);
                                if (node.Attributes.Count > 0 && node.Attributes["Name"] != null &&
                                    (node.Attributes["Audio_PlaylistGUID"] != null || node.Attributes["Audio_ThemeGUID"] != null))
                                {
                                    if (node.Name.StartsWith("Playlist"))
                                    {
                                        // Playlisten einlesen
                                        aPlayListsName.Add(node.Attributes["Name"].Value);

                                        // Suche nach der Playlistendatei in gleichen Ordner und hinzufügen
                                        List<string> playlist_file = new List<string>();

                                        string dir = System.IO.Path.GetDirectoryName(pfad);
                                        DirectoryInfo d = new DirectoryInfo(dir);
                                        foreach (FileInfo f in d.GetFiles("*.xml").Where(t => t.FullName.ToLower().EndsWith(".xml")).Where(t => t.Length != 0))
                                        {
                                            XmlTextReader textReaderPlayList = new XmlTextReader(f.FullName);
                                            textReaderPlayList.Read();
                                            while (textReaderPlayList.NodeType == XmlNodeType.XmlDeclaration ||
                                                (textReaderPlayList.NodeType == XmlNodeType.Element && textReader.Name == "Audio_Theme"))
                                                textReaderPlayList.Read();
                                            if (textReaderPlayList.Name == "Audio_Playlist")
                                            {
                                                while ( textReaderPlayList.Name != "Audio_PlaylistGUID")                                                
                                                    textReaderPlayList.Read();
                                                if (textReaderPlayList.NodeType == XmlNodeType.EndElement)
                                                    break;
                                                textReaderPlayList.Read();
                                                if (textReaderPlayList.NodeType == XmlNodeType.EndElement)
                                                    break;
                                                if (node.Attributes["Audio_PlaylistGUID"].Value == textReaderPlayList.Value)
                                                {
                                                    playlist_file.Add(f.FullName);
                                                    textReaderPlayList.Close();
                                                    break;
                                                }
                                                else
                                                {
                                                    textReaderPlayList.Close();
                                                    continue;
                                                }
                                            }                                            
                                        }

                                        aPlayListsGuid.Add(node.Attributes["Audio_PlaylistGUID"].Value);
                                        
                                        if (allePlaylistenÜberspringen)
                                            continue;
                                        //Abfrage Vorgehensweise beim Laden der Playlisten
                                        if (playlist_file.Count > 0 && !allePlaylistenImportieren)
                                        {
                                            int fragePlaylistüberschrieben = !ohnePlaylistenImport?                                                
                                                ViewHelper.ConfirmYesNoCancel("Enthaltene Playliste gefunden", 
                                                "Das Theme enthält Informationen zu einer Playliste die auch in dem Verzeichnis gefunden wurde." + 
                                                Environment.NewLine + Environment.NewLine +
                                                "Soll die Playliste des Themes ebenfalls geladen werden?" + 
                                                Environment.NewLine + Environment.NewLine + 
                                                "ACHTUNG!  Evtl. werden existierende Playlisten überschrieben."): 1;
                                            
                                            if (fragePlaylistüberschrieben == 2)
                                            {
                                                int frageFürAlle = ViewHelper.ConfirmYesNoCancel("Alle Playlisten laden/überschreiben", 
                                                    "Soll dieser Vorgang für alle enthaltenen Playlisten gelten" + Environment.NewLine);
                                                if (frageFürAlle == 2) allePlaylistenImportieren = true;
                                                else
                                                    if (frageFürAlle == 1) einePlaylisteImportieren = true;
                                                    else
                                                        if (frageFürAlle == 0) break;
                                            } else
                                                if (fragePlaylistüberschrieben == 1)
                                                {
                                                    int frageFürAlle = !ohnePlaylistenImport ?
                                                        ViewHelper.ConfirmYesNoCancel("Alle Playlisten überspringen",
                                                        "Soll dieser Vorgang für alle enthaltenen Playlisten gelten" + Environment.NewLine) : 2;
                                                    if (frageFürAlle == 2) allePlaylistenÜberspringen = true;
                                                    else
                                                        if (frageFürAlle == 1) allePlaylistenÜberspringen = false;
                                                        else
                                                            if (frageFürAlle == 0) break;
                                                }
                                        }

                                        if (!allePlaylistenÜberspringen && playlist_file.Count > 0 && 
                                            (allePlaylistenImportieren || einePlaylisteImportieren))                                             
                                            PlaylistenImportieren(playlist_file);

                                    }
                                    else
                                        if (node.Name.StartsWith("Theme"))
                                        {
                                            // UnterThemes einlesen
                                            if (node.Attributes["Audio_ThemeGUID"].Value.ToUpper() != "00000000-0000-0000-0000-00000000A11E")
                                            {
                                                aThemesName.Add(node.Attributes["Name"].Value);
                                                aThemesGuid.Add(node.Attributes["Audio_ThemeGUID"].Value);
                                            }
                                        }
                                }
                                if (textReader.NodeType == XmlNodeType.EndElement)
                                    break;
                            }

                            // Theme erstellen
                            if (thName != "")
                            {
                                if (Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name == thName) != null)
                                {
                                    int resp = ViewHelper.ConfirmYesNoCancel("Doppelter Theme-Name", "Ein Theme mit dem Namen '" + thName + "' ist schon vorhanden." + Environment.NewLine +
                                        Environment.NewLine + "Soll das vorhandene Theme überschrieben werden");
                                    if (resp == 2)
                                    {
                                        AktKlangTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name == thName);
                                        AktKlangTheme.Audio_Playlist.Clear();
                                        AktKlangTheme.Audio_Theme1.Clear();
                                        AktKlangTheme.Audio_Theme2.Clear();
                                    }
                                    else
                                        if (resp == 0)
                                        {
                                            Global.SetIsBusy(false);
                                            return;
                                        }
                                        else
                                            AktKlangTheme = null;
                                }
                                else
                                    AktKlangTheme = NeuesKlangThemeInDB(thName);

                                if (AktKlangTheme != null)
                                {
                                    AktKlangTheme.NurGeräusche = nurGeräusch;
                                    foreach (string aPlyLstGuid in aPlayListsGuid)
                                    {
                                        Audio_Playlist aPlayList = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Audio_PlaylistGUID.ToString() == aPlyLstGuid);
                                        if (aPlayList == null)
                                            aPlayList = Global.ContextAudio.PlaylistListe.FirstOrDefault(t => t.Name == aPlayListsName[aPlayListsGuid.IndexOf(aPlyLstGuid)]);

                                        if (aPlayList != null)
                                            AktKlangTheme.Audio_Playlist.Add(aPlayList);
                                        else
                                            lstNotInclude.Add(aPlayListsName[aPlayListsGuid.IndexOf(aPlyLstGuid)]);
                                    }

                                    foreach (string aThemeGuid in aThemesGuid)
                                    {
                                        Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID.ToString() == aThemeGuid);
                                        if (aTheme == null)
                                            aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Name != aThemesName[aThemesGuid.IndexOf(aThemeGuid)]);

                                        if (aTheme != null)
                                            AktKlangTheme.Audio_Theme1.Add(aTheme);
                                        else
                                            lstNotInclude.Add(aThemesName[aThemesGuid.IndexOf(aThemeGuid)]);
                                    }
                                }
                            }
                        }
                    }
                    Global.ContextAudio.Save();

                    if (lstNotInclude.Count > 0)
                    {
                        string text = "";
                        foreach (string s in lstNotInclude)
                            text += s + Environment.NewLine;

                        ViewHelper.Popup("ACHTUNG !!!" + Environment.NewLine + "-------------" + Environment.NewLine +
                            Environment.NewLine + "Import nur teilweise durchgeführt" + Environment.NewLine + Environment.NewLine +
                            "Von dem Theme -" + thName + "- konnten folgende Playlisten/Unterthemes leider nicht gefunden werden." +
                            Environment.NewLine + Environment.NewLine + text + Environment.NewLine +
                            "Bitte stellen Sie sicher, dass die Playlisten/Unterthemes integriert sind, bevor die Themes importiert werden.");
                    }
                }
            }
            //Update wir nach der Routine durchgeführt
            //UpdateAlleListen();  
        }

        private void PlaylistenImportieren(List<string> dateien)
        {
            try
            {
                bool _nicht_first = false;

                foreach (string pfad in dateien)
                {
                    Global.SetIsBusy(true, string.Format("Neue Playlist  '" + ViewHelper.GetValidFilename(System.IO.Path.GetFileNameWithoutExtension(pfad)) + "'  wird importiert ..."));
                    (App.Current.MainWindow as View.MainView).UpdateLayout();

                    if (pfad.EndsWith(".xml"))
                    {
                        if (AktKlangPlaylist == null) AktKlangPlaylist = new Audio_Playlist();
                        if (Audio_Playlist.Import(pfad, "Audio_Playlist", _nicht_first) != null)
                            AktKlangPlaylist = Global.ContextAudio.Liste<Audio_Playlist>()[0];
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(pfad);
                        AktKlangPlaylist = NeueKlangPlaylistInDB(fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length));

                        _DateienAufnehmen(new List<string>() { pfad }, null, null, AktKlangPlaylist, 0, false);
                        Global.ContextAudio.Update<Audio_Playlist>(AktKlangPlaylist);
                    }
                    Global.ContextAudio.Save();
                    _nicht_first = true;
                }
                Global.SetIsBusy(true, string.Format("Listen werden aktualisiert..."));

                AktualisiereHotKeys();
                if (AktKlangPlaylist != null)
                    SelectedEditorItem = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == AktKlangPlaylist);
                
                Global.SetIsBusy(false);
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Beim Import ist ein Fehler aufgetreten. Schließen Sie die Anwendung und wiederholen Sie den Vorgang.", ex);
                AktualisiereHotKeys();
            }
        }
       
        public void GetTotalLength(Audio_Playlist aPlaylist, bool busyWindow)
        {
            if (!AudioSpieldauerBerechnen || aPlaylist == null)
                return;
            if (aPlaylist.Länge == 0)
            {
                double gesLänge = 0;
                for (int i = 0; i < aPlaylist.Audio_Playlist_Titel.Count; i++)
                    if (aPlaylist.Audio_Playlist_Titel.ElementAt<Audio_Playlist_Titel>(i).Audio_Titel.Länge.HasValue)
                        gesLänge += aPlaylist.Audio_Playlist_Titel.ElementAt(i).Audio_Titel.Länge.Value;
            }
            if (BerechneSpieldauer)     //BackgroundWorker läuft noch - nur einen zulassen
                return;
            BerechneSpieldauer = true;

            if (busyWindow)
                Global.SetIsBusy(true, string.Format("Länge der Titel wird überarbeitet..."));
            
            BackgroundWorker workerGetLength = new BackgroundWorker();
            workerGetLength.WorkerReportsProgress = true;
            workerGetLength.WorkerSupportsCancellation = true;
            workerGetLength.DoWork += new DoWorkEventHandler(workerGetLength_DoWork);
            workerGetLength.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerGetLength_RunWorkerCompleted);

            workerGetLength.RunWorkerAsync(aPlaylist);
        }

        private void workerGetLength_DoWork(object sender, DoWorkEventArgs args)
        {
            AudioData aData = new AudioData();
            double totalLength = 0;

            try
            {
                Audio_Playlist plylst = (Audio_Playlist)args.Argument;

                totalLength = 0;
                plylst = (Audio_Playlist)args.Argument;
                for (int i = 0; i < plylst.Audio_Playlist_Titel.Count; i++)
                {
                    Audio_Titel aTitel = plylst.Audio_Playlist_Titel.ElementAt(i).Audio_Titel;                    
                    if (aTitel.Länge == null || aTitel.Länge == 0)
                    {
                        if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                            Directory.Exists(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                            File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
                        {        
                            if (plylst != (Audio_Playlist)args.Argument) break;
                            aData.setFilename(aTitel.Pfad + "\\" + aTitel.Datei, true);
                            double duration = aData.getLength();
                            
                            totalLength += duration;
                            
                            if (plylst.Audio_Playlist_Titel.Count >= i + 1)
                            {
                                if (aTitel.Länge != duration)
                                    aTitel.Länge = duration;
                            }
                            if (plylst != (Audio_Playlist)args.Argument) break;
                        }
                        else
                        {
                            _GrpObjecte.FindAll(t => t.aPlaylist == plylst).ForEach(delegate(MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpObj)
                            {
                                MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile klZeile = grpObj._listZeile.FirstOrDefault(t => t.aPlaylistTitel.Audio_TitelGUID == plylst.Audio_Playlist_Titel.ElementAt(i).Audio_TitelGUID);
                                if (klZeile != null) klZeile.playable = false;
                            });
                        }
                    }
                    else
                        totalLength += aTitel.Länge.Value;
                }
                if (plylst != (Audio_Playlist)args.Argument)
                    totalLength = 0;
                ((Audio_Playlist)args.Argument).Länge = totalLength;

                args.Result = (Audio_Playlist)args.Argument;
                aData.Close();
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Berechnungsfehler" + Environment.NewLine + "Beim Ermitteln der Gesamtlänge ist ein Fehler aufgetreten.", ex);
            }
        }

        private void workerGetLength_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            try
            {

                if (args.Error == null &&
                    ((Audio_Playlist)args.Result).Länge != 0)
                    Global.ContextAudio.Update<Audio_Playlist>(((Audio_Playlist)args.Result));

                BerechneSpieldauer = false;             // Berechnung der Gesamtlänge wieder freigeben
                Global.SetIsBusy(false);
                (sender as BackgroundWorker).Dispose();
            }
            catch (Exception)
            {
                BerechneSpieldauer = false;             // Berechnung der Gesamtlänge wieder freigeben
                Global.SetIsBusy(false);
                (sender as BackgroundWorker).Dispose();
            }
        }
        
        public void MoveLbEditorItem(Audio_Playlist aPlaylist, int dif)
        {
            lbEditorItemVM lbiAnfangVM = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == aPlaylist);
            int posEnde = FilteredEditorListBoxItemListe.IndexOf(lbiAnfangVM) + dif;
            lbEditorItemVM lbiEndeVM = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist == FilteredEditorListBoxItemListe.ElementAt(posEnde).APlaylist);

            int anf = lbiAnfangVM.APlaylist.Reihenfolge;
            int end = lbiEndeVM.APlaylist.Reihenfolge;

            lbEditorItemVM lbi;
            if (lbiEndeVM != null)
            {
                if (anf < end)
                {
                    for (int x = anf + 1; x <= end; x++)
                    {
                        lbi = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Reihenfolge == x);
                        if (lbi != null)
                        {
                            lbi.APlaylist.Reihenfolge--;

                            Global.ContextAudio.Update<Audio_Playlist>(lbi.APlaylist);
                        }
                    }
                    lbiAnfangVM.APlaylist.Reihenfolge = end;
                }
                else
                {
                    for (int x = anf - 1; x >= end; x--)
                    {
                        lbi = EditorListBoxItemListe.FirstOrDefault(t => t.APlaylist.Reihenfolge == x);
                        lbi.APlaylist.Reihenfolge++;
                        Global.ContextAudio.Update<Audio_Playlist>(lbi.APlaylist);
                    }
                    lbiAnfangVM.APlaylist.Reihenfolge = end;
                }
                Global.ContextAudio.Update<Audio_Playlist>(lbiAnfangVM.APlaylist);
            }
            OnChanged("EditorListBoxItemListe");
        }

        public void MoveAudioZeileItem(Audio_Playlist_Titel aPlaylistTitel, int dif)
        {
            AudioZeileVM aZeileAnfangVM = FilteredLbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel == aPlaylistTitel);
            int posEnde = FilteredLbEditorAudioZeilenListe.IndexOf(aZeileAnfangVM) + dif;
            AudioZeileVM aZeileEndeVM = LbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel == FilteredLbEditorAudioZeilenListe.ElementAt(posEnde).aPlayTitel);

            int anf = aZeileAnfangVM.aPlayTitel.Reihenfolge;
            int end = aZeileEndeVM.aPlayTitel.Reihenfolge;

            AudioZeileVM lbi;
            if (aZeileEndeVM != null)
            {
                if (anf < end)
                {
                    for (int x = anf + 1; x <= end; x++)
                    {
                        lbi = LbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel.Reihenfolge == x);
                        lbi.aPlayTitel.Reihenfolge--;

                        Global.ContextAudio.Update<Audio_Playlist_Titel>(lbi.aPlayTitel);
                    }
                    aZeileAnfangVM.aPlayTitel.Reihenfolge = end;
                }
                else
                {
                    for (int x = anf - 1; x >= end; x--)
                    {
                        lbi = LbEditorAudioZeilenListe.FirstOrDefault(t => t.aPlayTitel.Reihenfolge == x);
                        if (lbi != null)
                        {
                            lbi.aPlayTitel.Reihenfolge++;
                            Global.ContextAudio.Update<Audio_Playlist_Titel>(lbi.aPlayTitel);
                        }
                    }
                    aZeileAnfangVM.aPlayTitel.Reihenfolge = end;
                }
                Global.ContextAudio.Update<Audio_Playlist_Titel>(aZeileAnfangVM.aPlayTitel);
            }
            OnChanged("AktKlangPlaylist");
        }

        public void UpdateAlleListen()
        {
            EditorThemeListBoxItemListe = lbiThemeListNeuErstellen();
            EditorListBoxItemListe = lbiPlaylistListNeuErstellen();

            MusikListItemListe = mZeileEditorMusikNeuErstellen();
            ErwPlayerMusikListItemListe = mZeileErwPlayerMusikNeuErstellen();
            ErwPlayerGeräuscheListItemListe = mZeileErwPlayerGeräuscheNeuErstellen();
            ErwPlayerThemeListe = ThemeErwPlayerListeNeuErstellen();

            Refresh();

            FilterEditorPlaylistListe();
            FilterMusikPlaylistListe();
            FilterThemeEditorPlaylistListe();
            FilterErwPlayerMusikPlaylistListe();
            FilterErwPlayerGeräuschePlaylistListe();
            FilterErwPlayerThemeListe();
        }

        private void VisualGrpObj()
        {
            GruppenObjekt grpobj = new GruppenObjekt();
            tiErstellt++;

            grpobj.objGruppe = Convert.ToInt16(tiErstellt);

            //grpobj.wartezeitTimer.Tick += new EventHandler(wartezeitTimer_Tick);
            grpobj.visuell = true;
            _GrpObjecte.Add(grpobj);
        }

        private void RatingUpdate(int rating, Audio_Playlist_Titel aPlaylistTitel)
        {
            aPlaylistTitel.Rating = rating;
            Global.ContextAudio.Update<Audio_Playlist_Titel>(aPlaylistTitel);
        }

        public void ExportTheme(Audio_Theme aTheme, string pfaddatei)
        {
            if (aTheme != null)
            {
                if (pfaddatei != null)
                {
                    Global.SetIsBusy(true, string.Format("Das Theme '" + ViewHelper.GetValidFilename(aTheme.Name) + "'  wird exportiert ..."));

                    File.Delete(pfaddatei);
                    XmlTextWriter textWriter = new XmlTextWriter(pfaddatei, null);
                    textWriter.WriteStartDocument();

                    textWriter.WriteComment("Theme-Export vom " + DateTime.Now.ToShortDateString());
                    textWriter.WriteComment("Theme-Name: " + ViewHelper.GetValidFilename(aTheme.Name));

                    int i = 1;
                    textWriter.WriteStartElement("Themename", aTheme.Name);

                    textWriter.WriteStartAttribute("IstNurGeräuschTheme");
                    textWriter.WriteValue(aTheme.NurGeräusche);
                    textWriter.WriteEndAttribute();

                    foreach (Audio_Playlist aPlaylist in aTheme.Audio_Playlist)
                    {
                        textWriter.WriteStartElement("Playlist" + i);
                        textWriter.WriteStartAttribute("Name");
                        textWriter.WriteValue(aPlaylist.Name);
                        textWriter.WriteEndAttribute();

                        textWriter.WriteStartAttribute("Audio_PlaylistGUID");
                        textWriter.WriteValue(aPlaylist.Audio_PlaylistGUID.ToString());
                        textWriter.WriteEndAttribute();
                        textWriter.WriteEndElement();
                        i++;
                    }
                    int t_pos = 1;
                    foreach (Audio_Theme aUTheme in aTheme.Audio_Theme1)
                    {
                        textWriter.WriteStartElement("Theme" + t_pos);
                        textWriter.WriteStartAttribute("Name");
                        textWriter.WriteValue(aUTheme.Name);
                        textWriter.WriteEndAttribute();

                        textWriter.WriteStartAttribute("Audio_ThemeGUID");
                        textWriter.WriteValue(aUTheme.Audio_ThemeGUID.ToString());
                        textWriter.WriteEndAttribute();
                        textWriter.WriteEndElement();
                        t_pos++;
                    }
                    textWriter.WriteEndDocument();
                    textWriter.Close();
                }
            }
        }

        private void CheckUnterThemes(Audio_Theme aTheme, bool status)
        {
            foreach (Audio_Theme aUnterTheme in aTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")))
            {
                foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                {
                    if (aUnterTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        mZeile.tbtnCheck.IsChecked = status;
                }
                if (aUnterTheme.Audio_Theme1.Where(t => t.Audio_ThemeGUID != Guid.Parse("00000000-0000-0000-0000-00000000A11E")).ToList().Count > 0)
                    CheckUnterThemes(aUnterTheme, status);
            }
        }

        private void FilterGeräuscheAktiv()
        {
            SuchTextErwPlayerGeräusche = "";
            OnChanged("OnThemeGeräuscheFilterNichtAktiv");
            bool found = false;
            foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
            {
                if (mZeile.tbtnCheck.IsChecked.Value)
                {
                    found = true;
                    break;
                }
            }
            if (found)
                OnChanged("OnThemeGeräuscheFilterAktiv");
        }

        public void LoadMusikTitelListe()
        {
            List<KlangZeile> kZeileList = new List<KlangZeile>();
            List<ListBoxItem> itemList = new List<ListBoxItem>();
            if (BGPlayer.AktPlaylist != null)
            {
                List<Audio_Playlist_Titel> aPlaylisttitelSort = MusikTitelAZ ?
                    BGPlayer.AktPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Audio_Titel.Name).ToList() :
                    BGPlayer.AktPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Reihenfolge).ToList();

                foreach (Audio_Playlist_Titel playlisttitel in aPlaylisttitelSort)
                {
                    ListBoxItem lbitem = new ListBoxItem();
                    lbitem.Tag = playlisttitel;
                    lbitem.Content = playlisttitel.Audio_Titel.Name;
                    lbitem.ToolTip = playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;
                    lbitem.Background = Background;

                    if (!BGPlayer.AktPlaylist.Audio_Playlist_Titel.First(t => t.Audio_TitelGUID == playlisttitel.Audio_Titel.Audio_TitelGUID).Aktiv)
                    {
                        lbitem.FontStyle = FontStyles.Italic;
                        lbitem.Foreground = Brushes.DarkSlateGray;
                        lbitem.ToolTip = "Audio-Titel inaktiv." + Environment.NewLine + "Im Playlist-Editor anhaken zum Aktivieren" +
                                            Environment.NewLine + "Anklicken um den Titel abzuspielen";
                    }
                    if (BGPlayer.MusikNOK.Contains(playlisttitel.Audio_TitelGUID))
                    {
                        lbitem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));         // Brushes.Red;
                        lbitem.ToolTip = "Datei nicht gefunden. -> " + playlisttitel.Audio_Titel.Pfad + "\\" + playlisttitel.Audio_Titel.Datei;
                    }
                    itemList.Add(lbitem);

                    KlangZeile kZeile = new KlangZeile(rowErstellt);
                    kZeile.aPlaylistTitel = playlisttitel;
                    rowErstellt++;

                    kZeileList.Add(kZeile);
                }
            }
            HintergrundMusikListe = itemList;
            FilterMusikTitelListe();
            KlangZeilen = kZeileList;
        }

        public void ClickTbtnCheckErwPlayer(object sender, ExecutedRoutedEventArgs e)
        {
            MusikZeile mZeile = null;
            foreach (MusikZeile zeile in ErwPlayerGeräuscheListItemListe)
            {
                if (zeile.tbtnCheck == (ToggleButton)sender)
                {
                    mZeile = zeile;
                    break;
                }
            }

            GruppenObjekt grpobj = ((ToggleButton)sender).Tag == null ? null :
                _GrpObjecte.FindAll(t => t.visuell).FirstOrDefault(t => t.objGruppe == Convert.ToInt32(((ToggleButton)sender).Tag));
            if (grpobj == null)
                grpobj = _GrpObjecte.FindAll(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == ((Guid)(mZeile.Tag)));

            if (grpobj == null)
            {
                ((ToggleButton)sender).Tag = _GrpObjecte[_GrpObjecte.Count - 1].objGruppe;
                string sTitel = ((TextBlock)((StackPanel)((ToggleButton)e.Source).Parent).FindName("tblkTitel")).Text;

                int plylstItemPos = 0;
                while (((MusikZeile)ErwPlayerGeräuscheListItemListe[plylstItemPos]).tblkTitel.Text != sTitel)
                    plylstItemPos++;

                grpobj = _GrpObjecte.FirstOrDefault(t => t.objGruppe == tiErstellt);

                //Get Playlist
                grpobj.aPlaylist = Global.ContextAudio.PlaylistListe.
                        FirstOrDefault(t => t.Audio_PlaylistGUID.Equals(((MusikZeile)ErwPlayerGeräuscheListItemListe[plylstItemPos]).Tag));

                if (grpobj.aPlaylist != null)
                {
                    grpobj.visuell = false;

                    foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
                    {
                        KlangNewRow(grpobj, aPlaylistTitel);

                        if (aPlaylistTitel.Aktiv &&
                            !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                        }
                    }
                }
            }

            // grpobj.DoForceVolume = (mZeile.chkbxForceVol.IsChecked.Value);

            if ((FilteredErwPlayerGeräuscheListItemListe.Count == 0) ||
                (1 == 1) &&
                 !grpobj.wirdAbgespielt &&
                 _GrpObjecte.FindAll(t => t.wirdAbgespielt).FindAll(t => !t.visuell).FindAll(t => t.tiEditor == null).Count != 0)    //Abspielen
            {
                grpobj.wirdAbgespielt = true;
                if (!grpobj.aPlaylist.Fading)
                    mZeile.VM.FadingPercentage = 100;

                //WARTEZEIT DER PLAYLISTE EINBAUEN
                grpobj.wartezeitTimer.Tag = grpobj;

                if (grpobj.aPlaylist.WarteZeitAktiv)
                {
                    if (grpobj.wartezeitTimer.IsEnabled)
                        grpobj.wartezeitTimer.Stop();

                    grpobj.wartezeitTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)grpobj.aPlaylist.WarteZeit);
                    grpobj.wartezeitTimer.Start();
                }

                FilteredErwPlayerGeräuscheListItemListe.ForEach(delegate(MusikZeile _mZeile)
                {
                    if (_mZeile.tbtnCheck.IsChecked.Value && _mZeile.tbtnCheck != ((ToggleButton)sender))
                    {
                        GruppenObjekt grpObjAlleAnderen = _GrpObjecte.FirstOrDefault(t => t.objGruppe == Convert.ToInt32(_mZeile.tbtnCheck.Tag));
                        if (grpObjAlleAnderen != null && grpObjAlleAnderen.wirdAbgespielt != grpobj.wirdAbgespielt)
                        {
                            grpObjAlleAnderen.wirdAbgespielt = !grpobj.wirdAbgespielt;
                            grpObjAlleAnderen.tbtnKlangPause.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
                        }
                    }
                });
            }
        }

        public void ThemeButton_Checked(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Audio_Theme aTheme = Global.ContextAudio.ThemeListe.FirstOrDefault(t => t.Audio_ThemeGUID == ((Guid)((ToggleButton)sender).Tag));

                if (((ToggleButton)sender).IsChecked.Value)
                {
                    AktErwPlayerTheme = aTheme;
                    if (!aTheme.NurGeräusche)
                    {
                        foreach (grdThemeButton grdTbtn in ErwPlayerThemeListe)
                        {
                            if (grdTbtn.tbtnTheme.IsChecked.Value &&
                                grdTbtn.tbtnTheme != ((ToggleButton)sender) &&
                                !grdTbtn.chkbxPlus.IsChecked.Value)
                            {
                                grdTbtn.tbtnTheme.IsChecked = false;
                                ThemeButton_Checked(grdTbtn.tbtnTheme, null);
                            }
                        }

                        bool foundHintergrund = false;
                        foreach (Audio_Playlist aPlaylist in aTheme.Audio_Playlist)
                        {
                            if (aPlaylist.Hintergrundmusik)
                            {
                                foundHintergrund = true;
                                foreach (MusikZeile mZeile in ErwPlayerMusikListItemListe)
                                {
                                    if ((Guid)mZeile.Tag == aPlaylist.Audio_PlaylistGUID)
                                    {
                                        SelectedMusikPlaylistItem = mZeile;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!foundHintergrund)
                            btnBGStoppen(null);
                    }

                    foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                    {
                        if (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        {
                            mZeile.tbtnCheck.IsChecked = true;
                            mZeile.VM.tbtnCheckChecked(mZeile.tbtnCheck);
                        }
                    }
                    //Auswählen der Geräusche-Playlisst der untergeorgenten Themes
                    CheckUnterThemes(aTheme, true);
                    FilterGeräuscheAktiv();

                    OnThemeGeräuscheFilterAktivClick(null);
                }

                //  ----------- UNCHECKED ----------------
                else
                {
                    AktErwPlayerTheme = null;
                    if (!aTheme.NurGeräusche && e != null)
                        btnBGStoppen(null);

                    foreach (MusikZeile mZeile in ErwPlayerGeräuscheListItemListe)
                    {
                        if (aTheme.Audio_Playlist.FirstOrDefault(t => t.Audio_PlaylistGUID == (Guid)mZeile.Tag) != null)
                        {
                            mZeile.tbtnCheck.IsChecked = false;
                            mZeile.VM.tbtnCheckUnChecked(mZeile.tbtnCheck);
                        }
                    }
                    //Auswählen der Geräusche-Playlisst der untergeorgenten Themes
                    CheckUnterThemes(aTheme, false);
                }
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Auswählen des Themes ist ein Fehler aufgetreten.", ex);
            }
        }

        #endregion

        #region //---- Funktionen ----


        public Nullable<double> getTitelLänge(Audio_Titel aTitel)
        {
            Nullable<double> länge;

            AudioData aData = new AudioData();
            aData.setFilename(aTitel.Pfad + "\\" + aTitel.Datei, true);
            länge = aData.getLength();
            if (länge == -1) länge = 10000000;
            aData.Close();

            return länge;
        }


        private lbEditorItem NewlbEditorItem(lbEditorItem item)
        {
            lbEditorItem editorItem = new lbEditorItem();
            return editorItem;
        }

        public Audio_Theme NeuesKlangThemeInDB(string titel)
        {
            string themeName = GetNeuenNamen(titel == "" ? "Neues Theme" : titel, 1);
            Audio_Theme themelist = Global.ContextAudio.ThemeListe.Find(t => t.Name.Equals(themeName));

            if (themelist == null)
            {
                //Theme - Item erstellen      
                Audio_Theme aTheme = Global.ContextAudio.New<Audio_Theme>();
                aTheme.Name = themeName;
                aTheme.Hintergrund_VolMod = 50;
                aTheme.Klang_VolMod = 50;

                if (Global.ContextAudio.Insert<Audio_Theme>(aTheme))               //erfolgreich hinzugefügt
                {
                    Global.ContextAudio.Update<Audio_Theme>(aTheme);
                    AktKlangTheme = aTheme;
                }
                else
                {
                    ViewHelper.Popup("Ein neues Theme konnte nicht erstellt werden." + Environment.NewLine + Environment.NewLine +
                        "Schließen Sie das Programm und probieren Sie es später erneut.");
                    return null;
                }
                // UpdateAlleListen();
                return AktKlangTheme;
            }
            else
            {
                AktKlangTheme = themelist;
                //  UpdateAlleListen();
                return themelist;
            }
        }

        private Audio_Titel CreateTitel(string pfad)
        {
            Audio_Titel tmp = Global.ContextAudio.New<Audio_Titel>();
            tmp.Name = "Neuer Titel";
            tmp.Pfad = pfad;
            return tmp;
        }

        private Audio_Playlist_Titel AddTitelToPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel)
        {
            Audio_Playlist_Titel tmp;
            Global.ContextAudio.AddTitelToPlaylist(aPlaylist, aTitel, out tmp);
            return tmp;
        }
        
        private Audio_Playlist NeueKlangPlaylistInDB(string playlistname)
        {
            Audio_Playlist playlist = Global.ContextAudio.New<Audio_Playlist>();
            playlist.MaxSongsParallel = 1;
            playlist.Name = playlistname;
            playlist.Hintergrundmusik = false;

            //zur datenbank hinzufügen
            if (Global.ContextAudio.Insert<Audio_Playlist>(playlist))               //erfolgreich hinzugefügt
                AktKlangPlaylist = playlist;

            return playlist;
        }

        int setTitelStdPfad_AufrufeHintereinander = 0;
        public Audio_Titel setTitelStdPfad(Audio_Titel aTitel)
        {
            setTitelStdPfad_AufrufeHintereinander++;
            try
            {
                char[] charsToTrim = { '\\' };
                //Check Titel -> Pfad vorhanden ansonsten Standard-Pfad hinzufügen
                string chkExist = aTitel.Pfad + "\\" + aTitel.Datei;
                if (System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&  
                    File.Exists(chkExist))
                {
                    foreach (string pfad in stdPfad)
                    {
                        if (pfad == aTitel.Pfad)
                            return aTitel;

                        if (aTitel.Pfad != null && (aTitel.Pfad + "\\" + aTitel.Datei).Contains(pfad))
                        {
                            aTitel.Datei = (aTitel.Pfad.EndsWith("\\") ? aTitel.Pfad + aTitel.Datei : aTitel.Pfad + "\\" + aTitel.Datei).
                                Substring(pfad.EndsWith("\\") ? pfad.Length : pfad.Length + 1);
                            aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                            return aTitel;
                        }
                    }
                    // Pfad noch kein Standard-Pfad
                    if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
                        Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
                        "Soll der Pfad mit in die Standard-Pfade integriert werden?" + Environment.NewLine + Environment.NewLine + "Neuer Pfad:     " + aTitel.Pfad))
                    {
                        List<string> allSamePfad = MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }).ToList().FindAll(t => aTitel.Pfad.StartsWith(t));
                        if (allSamePfad != null)
                        {
                            string maxSamePfad = allSamePfad.Max();
                            aTitel.Pfad = maxSamePfad;
                            aTitel.Datei = aTitel.Pfad.Substring(maxSamePfad.Length+1) + aTitel.Datei;
                        }

                        MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
                            MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + aTitel.Pfad;
                        setStdPfad();
                    }
                    return aTitel;
                }

                //Pfad+Titel nicht gefunden -> Check Titel in einem anderen Standard-Pfad
                foreach (string pfad in stdPfad)
                {
                    if (aTitel.Datei == null && aTitel.Pfad != null)
                    {
                        aTitel.Datei = aTitel.Pfad;
                        aTitel.Pfad = "";
                    }
                    if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + aTitel.Datei))
                    {
                        aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                        return aTitel;
                    }

                    if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + System.IO.Path.GetFileName(aTitel.Datei)))
                    {
                        aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                        aTitel.Datei = System.IO.Path.GetFileName(aTitel.Datei);
                        return aTitel;
                    }
                }

                if (AudioInAnderemPfadSuchen && setTitelStdPfad_AufrufeHintereinander < 2)
                {
                    //ab hier: kein Std.-Pfad ist gültig -> Check in jedem Std.-Pfad mit Suche incl. Unterverzeichnisse nach dem Dateinamen
                    string gesuchteDatei = System.IO.Path.GetFileName(stdPfad[0].TrimEnd(charsToTrim) + "\\" + aTitel.Datei);
                    foreach (string pfad in stdPfad)
                    {
                        if (pfad != "C:\\" && Directory.Exists(pfad))
                        {
                            string[] pfad_datei = Directory.GetFiles(pfad.TrimEnd(charsToTrim), gesuchteDatei, SearchOption.AllDirectories);
                            if (pfad_datei.Length > 0)
                            {
                                aTitel.Pfad = System.IO.Path.GetDirectoryName(pfad_datei[0]);
                                aTitel.Datei = System.IO.Path.GetFileName(pfad_datei[0]);                                
                                aTitel = setTitelStdPfad(aTitel);
                                setTitelStdPfad_AufrufeHintereinander = 0;
                                return aTitel;
                            }
                        }
                    }
                }

                if (aTitel.Pfad == null) aTitel.Pfad = "";
                if (aTitel.Pfad == "" || aTitel.Datei == null)
                {
                    string pfadDatei = aTitel.Pfad != null || aTitel.Pfad != "" ? aTitel.Pfad : "";
                    if (pfadDatei != "" && !pfadDatei.EndsWith("\\"))
                        pfadDatei = pfadDatei + "\\";
                    if (aTitel.Datei != null)
                        pfadDatei = pfadDatei + aTitel.Datei;

                    aTitel.Pfad = System.IO.Path.GetDirectoryName(pfadDatei);
                    aTitel.Datei = System.IO.Path.GetFileName(pfadDatei);
                }
                return aTitel;
            }
            catch (Exception)
            {
                return aTitel;
            }
        }
        
        
        private ListBoxItem GetNextMusikTitel()
        {
            try
            {
                if (BGPlayer == null || BGPlayer.AktPlaylist == null)
                    return null;
                Guid old_GUID = BGPlayer.AktPlaylistTitel != null ? BGPlayer.AktPlaylistTitel.Audio_TitelGUID : Guid.Empty;

                if (BGPlayer.AktPlaylist.Repeat == null &&
                    BGPlayer.AktPlaylistTitel != null)
                {
                    // BGPlayerAktPlaylistTitel = null;
                    return FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == old_GUID);
                }

                if (wiederholungenLeft > 1)
                {
                    wiederholungenLeft--;
                    return FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == old_GUID);
                }

                if (BGPlayer.NochZuSpielen.Count == 0 && BGPlayer.AktPlaylist != null && BGPlayer.AktPlaylist.Repeat != null &&
                    BGPlayer.AktPlaylist.Repeat.Value || MusikAktivIsPaused)
                    RenewMusikNochZuSpielen();
                if (BGPlayer.NochZuSpielen.Count > 0)
                {
                    Guid next;
                    if (BGPlayer.AktPlaylist.Shuffle)
                        next = BGPlayer.NochZuSpielen[(new Random()).Next(0, BGPlayer.NochZuSpielen.Count - 1)];
                    else
                    {
                        // Liste neu auffrischen
                        BGPlayer.NochZuSpielen.Clear();
                        RenewMusikNochZuSpielen();

                        // wenn der momentane Titel der letzte war, von vorne beginnen
                        if (BGPlayer.NochZuSpielen.Count == 1)
                            next = BGPlayer.NochZuSpielen[0];
                        else
                        {
                            //ansonsten Liste löschen bis zum Nächsten 
                            if (old_GUID != Guid.Empty)
                            {
                                while (BGPlayer.NochZuSpielen.Count != 0 &&
                                    BGPlayer.NochZuSpielen[0] != old_GUID)
                                    BGPlayer.NochZuSpielen.RemoveAt(0);

                                if (BGPlayer.NochZuSpielen.Count == 0)
                                    return null;
                                Guid zuLöschen = BGPlayer.NochZuSpielen[0];
                                while (BGPlayer.NochZuSpielen.Count > 0 && BGPlayer.NochZuSpielen[0] == zuLöschen)
                                    BGPlayer.NochZuSpielen.RemoveAt(0);
                                if (BGPlayer.NochZuSpielen.Count == 0 &&
                                    BGPlayer.AktPlaylist.Repeat != null && BGPlayer.AktPlaylist.Repeat.Value)
                                    RenewMusikNochZuSpielen();
                            }

                            next = BGPlayer.NochZuSpielen.Count > 0 ? BGPlayer.NochZuSpielen[0] : Guid.Empty;
                        }
                    }
                    ListBoxItem lbi = next != Guid.Empty ?
                        FilteredHintergrundMusikListe.FirstOrDefault(t => (Guid)((Audio_Playlist_Titel)t.Tag).Audio_TitelGUID == next) :
                        null;
                    return lbi;
                }
                else
                    BGStoppen();

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AudioData PlayFile(bool notMusikPlayer, KlangZeile klZeile,
            GruppenObjekt grpobj, AudioData _player, String url, double vol, bool fading, double posStart)
        {
            try
            {
                if (_player == null)
                {
                    _player = new AudioData();
                    if (klZeile != null)
                        klZeile.mediaHashCode = _player.GetHashCode();
                }

                try
                {
                    if (_player.isStopped() || _player.getFilename() != url)
                    {
                        if (!_player.setFilename(url, klZeile == null))
                        {
                            _player.Close();
                        }
                        else
                        {
                            double d = _player.getSpeed();                            
                            if (klZeile != null)
                            {
                                if (!(klZeile.aPlaylistTitel.Speed > 0 && klZeile.aPlaylistTitel.Speed < 6))
                                    _player.setSpeed(klZeile != null && klZeile.aPlaylistTitel != null ? klZeile.aPlaylistTitel.Speed : 0);
                                _player.setPitch((klZeile != null && klZeile.aPlaylistTitel != null ? klZeile.aPlaylistTitel.Pitch : 0));
                                _player.setEcho((klZeile != null && klZeile.audioZeileVM != null ? klZeile.audioZeileVM.aPlayTitelEcho : 0));
                                //player.setScale((klZeile != null && klZeile.audioZeileVM != null ? klZeile.audioZeileVM.aPlayTitelScale : 1));
                                // .aPlaylistTitel != null ? klZeile.aPlaylistTitel.Pitch : 0));

                            }
                            else
                            {
                                _player.setSpeed(0);
                                _player.setPitch(0);
                                _player.setEcho(0);
                            }
                        }
                            
                    };

                    //Auf Start-Position stellen wenn manuell gesetzt und nicht fading Out
                    if (_listMusikFadingOut.FirstOrDefault(t => ((Fading)t.Tag).aData == _player) == null)
                    {
                        _player.setPosition(posStart);
                        //// Bis zu 1000ms warten um die Musikdatei auszulesen und die Laufzeit zu ermitteln
                        //if (SpinWait.SpinUntil(() => { return _player.NaturalDuration.HasTimeSpan; }, 1000))
                        //    _player.Position = TimeSpan.FromMilliseconds(posStart.Value);
                    }

                    if (fading)   // ist Musik-Playlist
                    {
                        _player.mute(BGPlayerIsMuted);
                        FadingIn(null, klZeile, _player, (!notMusikPlayer) ? vol / 100 : vol / 100);
                    }
                    else
                    {
                        if (!grpobj.visuell) _player.mute(GeräuscheIsMuted);
                        if (!GeräuscheIsMuted)
                        {
                            if (_timerFadingOut.IsEnabled || !grpobj.aPlaylist.Fading)
                                _player.setVolume(
                                    ((!notMusikPlayer || grpobj.visuell) ?
                                        vol / 100 :
                                            (grpobj.aPlaylist.DoForce) ?
                                            (double)grpobj.aPlaylist.ForceVolume / 100 :
                                            (vol / 100) * (FadingGeräuscheVolProzent / 100)));
                            else
                                if (grpobj.aPlaylist.Fading)
                                {
                                    //_player.Volume = 0;                                      //Fading In Geräusche Start
                                    _player.setVolume((klZeile.Aktuell_Volume / 100) *
                                            (!grpobj.aPlaylist.Fading ? 1 * (FadingGeräuscheVolProzent / 100) :
                                            (!grpobj.aPlaylist.Hintergrundmusik ? grpobj.Vol_PlaylistMod / 100 :
                                            0)));
                                }
                        }
                        _player.Play();
                    }
                    if (grpobj != null && !grpobj.visuell)
                        grpobj.mZeileVM.Min1SongWirdGespielt = true;

                }
                catch (Exception ex2)
                {
                    SelectedMusikTitelItem.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));   // Brushes.Yellow
                    SelectedMusikTitelItem.ToolTip = "Datei konnte nicht geöffnet werden (Datei abspielbar / Codec installiert?)" + ex2;
                    SpieleNeuenMusikTitel(Guid.Empty);
                    return null;
                }

                return _player;
            }
            catch (Exception ex)
            {
                ViewHelper.ShowError("Audio Fehler" + Environment.NewLine + "Der Audio Player hat einen Fehler verursacht.", ex);
                return null;
            }
        }

        #endregion
    }
}
