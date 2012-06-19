using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Arena.Logic
{
    public class Enemy : Creature
    {

        public Enemy(double width, double height, Color color, String name)
            : base(width, height, color, name) { }

        public Enemy(double width, double height, Color color, String name, String portraitFileName)
            : base(width, height, color, name, portraitFileName) { }

    }
}
