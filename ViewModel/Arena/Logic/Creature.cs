using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Arena.Logic
{
    public abstract class Creature : ICloneable {
        
        private double _width;  //from aerial view in meters
        private double _height; //from aerial view in meters

        private Color _color;
        private String _portraitFilename;
        private String _name;

        private int _GS;

        public Creature() { }

        public Creature(double width, double height, Color color, String name) {
            _width = width;
            _height = height;
            _color = color;
            _name = name;
        }

        public Creature(double width, double height, Color color, String name, String portraitFileName)
            : this(width, height, color, name) {
                _portraitFilename = portraitFileName;
        }

        public int GS { 
            get { return _GS; } 
            set { _GS = value; } 
        }

        public Color CreatureColor {
            get { return _color; }
            set { _color = value; }
        }

        public double Width {
            get { return _width; }
        }

        public double Height {
            get { return _height; }
        }

        public String PortraitFileName {
            get { return _portraitFilename; }
            set { _portraitFilename = value; }
        }

        public String Name{
            get { return _name; }
        }


        public override string ToString() {
            return _name;
        }

        public object Clone() {
            return this.MemberwiseClone();    
        }
    }
}
