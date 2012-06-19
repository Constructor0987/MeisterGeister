using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Markup;
// Eigene Usings
using Base = MeisterGeister.ViewModel.Base;
using InventarLogic = MeisterGeister.ViewModel.Inventar.Logic;

namespace MeisterGeister.Model
{
    public partial class Notizen
    {
        #region //---- KONSTRUKTOR ----

        public Notizen()
        {
            
        }

        #endregion

        #region //---- FELDER ----

        

        #endregion

        #region //---- EIGENSCHAFTEN ----

        private FlowDocument _document;
        public FlowDocument Document
        {
            get
            {
                if (_document == null)
                {
                    if (string.IsNullOrEmpty(Text))
                        _document = new FlowDocument();
                    else
                        ParseTextToFlowDoument(Text);
                }
                return _document;
            }
            set
            {
                _document = value;
            }
        }

        #endregion

        #region //---- METHODEN ----

        public void AppendText(string text)
        {
                try
                {
                    // Text als neuen Block anfüfen
                    Document.Blocks.Add(new Paragraph(new Run(text)));

                    // Notizen speichern
                    Global.ContextNotizen.Save();
                }
                catch (Exception) { throw; }
        }

        public void ParseTextToFlowDoument(string text)
        {
            try
            {
                // Umwandlung versuchen
                if (text.StartsWith("<FlowDocument"))
                    Document = XamlReader.Parse(text) as FlowDocument;
                else
                    TextToFlowDocument(text);
            }
            catch (XamlParseException)
            {
                TextToFlowDocument(text);
            }
        }

        private void TextToFlowDocument(string text)
        {
            // Text entspricht keinem gültigen FlowDoument-Format.
            // Neues Doument anlegen und Text als Paragraphen einfügen.
            FlowDocument tmpDoc = new FlowDocument(new Paragraph(new Run(text)));
            Document = tmpDoc;
        }

        public void ParseFlowDoumentToText()
        {
            try
            {
                // FlowDocument in String umwandeln
                StringWriter wr = new StringWriter();
                XamlWriter.Save(Document, wr);
                Text = wr.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}