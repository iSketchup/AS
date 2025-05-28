using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AsClass
{

    public class Frame
    {

        static readonly int defaultBgPixelSize = 32;

        static WriteableBitmap BackgroundMaker(int Width, int Height)
        {

            WriteableBitmap wb = new WriteableBitmap(Width, Height, 96, 96, PixelFormats.Bgra32, null);

            int stride = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;

                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {

                        byte* pixel = buffer + y * stride + x * 4;
                        pixel[3] = 255;

                        if (((x / defaultBgPixelSize) + (y / defaultBgPixelSize)) % 2 == 0)
                        {
                            pixel[0] = 123;
                            pixel[1] = 123;
                            pixel[2] = 123;
                        }
                        else
                        {
                            pixel[0] = 204;
                            pixel[1] = 204;
                            pixel[2] = 204;
                        }



                    }
                }

                wb.AddDirtyRect(new Int32Rect(0, 0, Width, Height));



            }
            wb.Unlock();

            return wb;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Layers { get; set; } = 1;


        public Image img;
        public WriteableBitmap wb;


        public Frame(int width, int height, Image img)
        {
            this.Width = width;
            this.Height = height;
            this.img = img;

            this.img.Source = wb;
        }

        
    }
}