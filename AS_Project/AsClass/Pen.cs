using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsClass
{
    
     public class Pen
    {
        public double Size { get; set; } = 5;

        public Brush color { get; set; } =
            new SolidColorBrush(Color.FromRgb(0, 0, 0));       // Black; 

        public Pen()
        {

        }
        public Pen(int size, Brush color)
        {
            Size = size;
            this.color = color;
        }

        public void ChangeColor(SolidColorBrush newColor)
        {
            color = newColor;
        }
    }
}
