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
    public class Eyedropper
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

        private WriteableBitmap wb;
        private int x;
        private int y;


        public Eyedropper(Frame frame, Point point )
        {
            int x = (int)point.X;
            int y = (int)point.Y;
            this.wb = frame.wb;

            
        }


        public void GetColor()
        {
            int stride = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;





                byte* pixel = buffer + y * stride + x * 4;

                pixel[3] = SelectedColor.A;


                pixel[0] = SelectedColor.B;
                pixel[1] = SelectedColor.G;
                pixel[2] = SelectedColor.R;




            }
            wb.Unlock();

            ;
        }

    }
}
