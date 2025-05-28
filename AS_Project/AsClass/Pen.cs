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

        public Brush Color { get; set; }= Brushes.Black; 


        public Pen()
        {
        }
        public Pen(double size, Brush color)
        {
            Size = size;
            Color = color;
        }

    }
}
