using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace AsClass
{
    public class ColorPicker
    {
        private Color _selectedColor;

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
               
            }
        }

        public ColorPicker( double FrameHeight, double Framewidth)
        {
          
        }


        public void Pixelmade(int x, int y,WriteableBitmap wb)
        {
            int stride = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;





                byte* pixel = buffer + y * stride + x * 4;
                pixel[3] = 255;


                pixel[0] = 0;
                pixel[1] = 0;
                pixel[2] = 255;


                wb.AddDirtyRect(new Int32Rect(0, 0,wb.PixelWidth,wb.PixelHeight));



            }
            wb.Unlock();

            ;
        }

    }
}
