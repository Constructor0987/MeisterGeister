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
using System.Windows.Shapes;

namespace DgSuche
{
    /// <summary>
    /// Interaktionslogik für MsgWindow.xaml
    /// </summary>
    public partial class MsgWindow : Window
    {
        public MsgWindow(string header, string msg, bool showHilfe = true)
        {
            InitializeComponent();
            Title = header;
            if (showHilfe)
                msg += "\n\n";
            _textBoxMsg.Text = msg;
        }

        public MsgWindow(string header, string msg, Exception ex, bool showHilfe = true)
        {
            InitializeComponent();
            Title = header;
            msg += "\n\nSource: " + ex.Source;
            msg += "\nMessage: " + ex.Message;
            msg += "\nType: " + ex.GetType().ToString();
            if (ex is System.Net.WebException)
            {
                System.Net.WebException webEx = (System.Net.WebException)ex;
                if (webEx.Response != null)
                {
                    msg += "\nResponse: " + webEx.Response.ToString();
                    if (webEx.Response.Headers != null)
                    {
                        msg += "\n   Headers: ";
                        for (int i = 0; i < webEx.Response.Headers.Count; i++)
                        {
                            msg += string.Format("{0}: {1}; ", webEx.Response.Headers.Keys[i], webEx.Response.Headers[i]);
                        }
                    }
                    if (webEx.Response.ResponseUri != null)
                        msg += "\n   ResponseUri: " + webEx.Response.ResponseUri.ToString();
                }
                msg += "\nStatus: " + webEx.Status.ToString();
            }
            msg += "\nStackTrace: " + ex.StackTrace;
            if (showHilfe)
                msg += "\n\n";
            _textBoxMsg.Text = msg;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_textBoxMsg.Text);
        }
    }
}
