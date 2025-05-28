using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsClass
{
    public class Colorpallet
    {
        public List<Brush> Brushes = new List<Brush>();

        public Brush Activecolor { get; set; }


        public Colorpallet()
        {

        }

        public Colorpallet(List<Brush> brushes, Brush activecolor)
        {
            Brushes = brushes;
            Activecolor = activecolor;
        }

    }
}
