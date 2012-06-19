using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Arena.Logic
{
    public class Hero : Creature
    {

        public Hero(double width, double height, Color color, String name)
            : base(width, height, color, name) { }

        public Hero(double width, double height, Color color, String name, String portraitFileName)
            : base(width, height, color, name, portraitFileName) { }

    }
}
