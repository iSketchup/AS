using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AsClass
{
    
     public class Pen
    {
        public bool active = true;

        public int Size { get; set; } = 1;

        public bool isEraser { get; set; } = false; 

        public SolidColorBrush color { get; set; } =
            new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));       // Black; 

        public Pen()
        {

        }
        public Pen(int size, SolidColorBrush color)
        {
            Size = size;
            this.color = color;
        }

        public void ChangeColor(SolidColorBrush newColor)
        {
            color = newColor;
        }

        public void Draw(Frame frame, Point point)
        {
            if (point.X < 0 || point.X >= frame.Width )
                return;
            if (point.Y < 0 || point.Y >= frame.Height)
                return; 

            frame.ChangePixelColor(point, color, Size);
        }
    }
}
