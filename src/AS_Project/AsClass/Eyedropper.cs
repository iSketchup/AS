using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AsClass
{


    public class Eyedropper
    {

        Colorpallet colorpallet;


        public bool active = false;

        private Color newColor;

        public Eyedropper(Colorpallet colorpallet)
        {
            this.colorpallet = colorpallet;


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


                colorpallet.Activecolor = new SolidColorBrush(newColor);

            }
            wb.Unlock();

            ;
        }

    }
}
