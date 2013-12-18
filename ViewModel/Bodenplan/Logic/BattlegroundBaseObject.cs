using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    [DataContract(IsReference = true)]
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
                OnChanged("ObjektName");
            }
        }

        //is visible?
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnChanged("IsVisible");
            }
        }

        //is selected? 
        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                _isHighlighted = value;
                OnChanged("IsHighlighted");
            }
        }

        //is New? 
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                OnChanged("IsNew");
            }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set
            {
                _isMoving = value;
                OnChanged("IsMoving");
            }
        }

        //color
        public SolidColorBrush ObjectColor
        {
            get { return _objectColor; }
            set
            {
                _objectColor = value;
                OnChanged("ObjectColor");
            }
        }

        //fillcolor if there is any
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                OnChanged("FillColor");
            }
        }

        //strokethickness
        public double StrokeThickness
        {
            get { return _strokethickness; }
            set
            {
                _strokethickness = value;
                OnChanged("StrokeThickness");
            }
        }

        //opacity
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnChanged("Opacity");
            }
        }

        //ZLevel
        public double ZLevel
        {
            get { return _zLevel; }
            set
            {
                _zLevel = value;
                OnChanged("ZLevel");
            }
        }

        public double ZDisplayX
        {
            get { return _zDisplayX; }
            set
            {
                _zDisplayX = value;
                OnChanged("ZDisplayX");
            }
        }

        public double ZDisplayY
        {
            get { return _zDisplayY; }
            set
            {
                _zDisplayY = value;
                OnChanged("ZDisplayY");
            }
        }

        public virtual void OnChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
