using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Audio_Theme
    {
        public Audio_Theme()
        {
            Audio_ThemeGUID = Guid.NewGuid();
        }

        public ICollection<Audio_Theme> Parents
        {
            get { return Audio_Theme1; }
            set { Audio_Theme1 = value; OnChanged("Parents"); }
        }

        public ICollection<Audio_Theme> Children
        {
            get { return Audio_Theme2; }
            set { Audio_Theme2 = value; OnChanged("Children"); }
        }
    }
}
