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
        public int layers { get; set; }
        public byte[,] board { get; set; }

        public Image img = new Image();
        public WriteableBitmap wb;
        public Frame(int width, int height, int layers)
        {
            this.width = width;
            this.height = height;
            this.layers = layers;
            this.board = new byte[height * width, 4];

            BoardInit();
        }

        public Frame(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.layers = 1;
            this.board = new byte[height * width, 4];

            BoardInit();
        }

        public void BoardInit()
        {

            wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

            int stritde = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;

                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {

                        byte* pixel = buffer + y * stritde + x * 4;
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

                wb.AddDirtyRect(new Int32Rect(0, 0, 100, 100));



            }
            wb.Unlock();
        }
    }
}