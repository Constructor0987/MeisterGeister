namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class TextLabel : BattlegroundBaseObject
    {
        private double _labelHeight = 100;
        private double _labelPositionX = 50;
        private double _labelPositionY = 50;
        private double _labelWidth = 100;
        private string _textInLabel = "";

        public TextLabel()
        { }

        public TextLabel(string text, double x, double y)
        {
            _labelPositionX = x;
            _labelPositionX = y;
            _textInLabel = text ?? string.Empty;

            ZDisplayX = x - 10;
            ZDisplayY = y - 10;
        }

        public double LabelHeight
        {
            get { return _labelHeight; }

            set
            {
                _labelHeight = value;
                OnChanged(nameof(LabelHeight));
            }
        }

        public double LabelPositionX
        {
            get { return _labelPositionX; }

            set
            {
                _labelPositionX = value;
                OnChanged(nameof(LabelPositionX));
            }
        }

        public double LabelPositionY
        {
            get { return _labelPositionY; }

            set
            {
                _labelPositionY = value;
                OnChanged(nameof(LabelPositionY));
            }
        }

        public double LabelWidth
        {
            get { return _labelWidth; }

            set
            {
                _labelWidth = value;
                OnChanged(nameof(LabelWidth));
            }
        }

        public override string ObjectName
        {
            get { return "Label as Text"; }
        }

        public string TextInLabel
        {
            get { return _textInLabel; }

            set
            {
                _textInLabel = value;
                OnChanged(nameof(TextInLabel));
            }
        }

        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
        {
            LabelPositionX = LabelPositionX + deltaX;
            LabelPositionY = LabelPositionY + deltaY;
            ZDisplayX = LabelPositionX - 10;
            ZDisplayY = LabelPositionY - 10;
        }

        public override void RunAfterXMLDeserialization()
        {
        }

        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }
    }
}
