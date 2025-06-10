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

       
        public bool active = false;

        private Color newColor;

        public Eyedropper()
        {
           
            
        }


        public void GetColor(int x, int y, WriteableBitmap wb)
        {
            int stride = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;





                byte* pixel = buffer + y * stride + x * 4;

             
                newColor.A = pixel[3];

             
                newColor.B = pixel[0];
               
                newColor.G = pixel[1];
             
                newColor.R = pixel[2];


                SelectedColor = newColor;

            }
            wb.Unlock();

            ;
        }

    }
}
