using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AsClass
{

    public class Frame
    {
        public int width { get; set; }
        public int height { get; set; }
        public int layers { get; set; } = 1;


        public Image img;
        public WriteableBitmap wb;
        public Frame(int width, int height, Image img)
        {
            this.width = width;
            this.height = height;
            this.img = img;

            BoardInit();
            this.img.Source = wb;
        }

        public void BoardInit()
        {

            wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

            int stride = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {

                        byte* pixel = buffer + y * stride + x * 4;
                        pixel[3] = 255;

                        if (((x / 10) + (y / 10)) % 2 == 0)
                        {
                            pixel[0] = 153;
                            pixel[1] = 153;
                            pixel[2] = 153;
                        }
                        else
                        {
                            pixel[0] = 204;
                            pixel[1] = 204;
                            pixel[2] = 204;
                        }



                    }
                }

                wb.AddDirtyRect(new Int32Rect(0, 0, width, height));



            }
            wb.Unlock();
        }
    }
}