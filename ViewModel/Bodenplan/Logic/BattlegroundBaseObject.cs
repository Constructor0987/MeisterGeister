﻿using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using System;

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
                Set(ref _objektName, value);
            }
        }

        //is visible?
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                Set(ref _isVisible, value);
            }
        }

        //is selected? 
        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                Set(ref _isHighlighted, value);
            }
        }

        //is New? 
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                Set(ref _isNew, value);
            }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set
            {
                Set(ref _isMoving, value);
            }
        }

        //color
        public SolidColorBrush ObjectColor
        {
            get { return _objectColor; }
            set
            {
                Set(ref _objectColor, value);
            }
        }

        //fillcolor if there is any
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                Set(ref _fillColor, value);
            }
        }

        //strokethickness
        public double StrokeThickness
        {
            get { return _strokethickness; }
            set
            {
                Set(ref _strokethickness, value);
            }
        }

        //opacity
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                Set(ref _opacity, value);
            }
        }

        //ZLevel
        public double ZLevel
        {
            get { return _zLevel; }
            set
            {
                Set(ref _zLevel, value);
            }
        }

        public double ZDisplayX
        {
            get { return _zDisplayX; }
            set
            {
                Set(ref _zDisplayX, value);
            }
        }

        public double ZDisplayY
        {
            get { return _zDisplayY; }
            set
            {
                Set(ref _zDisplayY, value);
            }
        }

        protected bool Set<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnChanged(propertyName);
            return true;
        }

        protected void OnChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
