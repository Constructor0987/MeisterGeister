using System.ComponentModel;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public abstract class BattlegroundBaseObject:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isNew;
        private bool _isHighlighted = false;
        private bool _isMoving = false;
        private double _strokethickness = 6;
        private SolidColorBrush _objectColor;
        private Color _fillColor;
        private double _opacity = 1;
        private double _zLevel = 10;
        private double _zDisplayX = 0, _zDisplayY = 0;
        private bool _isVisible = true;
        private string _objektName = "";

        //Name
        public string ObjektName
        {
            get { return _objektName; }
            set
            {
                _objektName = value;
                OnPropertyChanged("ObjektName");
            }
        }

        //is visible?
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        //is selected? 
        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                _isHighlighted = value;
                OnPropertyChanged("IsHighlighted");
            }
        }

        //is New? 
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                OnPropertyChanged("IsNew");
            }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set
            {
                _isMoving = value;
                OnPropertyChanged("IsMoving");
            }
        }

        //color
        public SolidColorBrush ObjectColor
        {
            get { return _objectColor; }
            set
            {
                _objectColor = value;
                OnPropertyChanged("ObjectColor");
            }
        }

        //fillcolor if there is any
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                OnPropertyChanged("FillColor");
            }
        }

        //strokethickness
        public double StrokeThickness
        {
            get { return _strokethickness; }
            set
            {
                _strokethickness = value;
                OnPropertyChanged("StrokeThickness");
            }
        }

        //opacity
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnPropertyChanged("Opacity");
            }
        }

        //ZLevel
        public double ZLevel
        {
            get { return _zLevel; }
            set
            {
                _zLevel = value;
                OnPropertyChanged("ZLevel");
            }
        }

        public double ZDisplayX
        {
            get { return _zDisplayX; }
            set
            {
                _zDisplayX = value;
                OnPropertyChanged("ZDisplayX");
            }
        }

        public double ZDisplayY
        {
            get { return _zDisplayY; }
            set
            {
                _zDisplayY = value;
                OnPropertyChanged("ZDisplayY");
            }
        }



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
