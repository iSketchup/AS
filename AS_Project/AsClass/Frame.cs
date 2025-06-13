using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace AsClass
{

    public class Frame
    {

        static readonly int defaultBgPixelSize = 8;
        static int PixelSize = 4;

        static public WriteableBitmap BackgroundMaker(int Width, int Height)
        {

            WriteableBitmap wb = new(Width, Height, 96, 96, PixelFormats.Pbgra32, null);

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
        public static Frame LoadFrameFrom(string path)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage);

            return new Frame(writeableBitmap);
        }


        public int Width { get; set; }
        public int Height { get; set; }
        public int Layers { get; set; } = 1;


        public WriteableBitmap wb;


        public Frame(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            wb = new(Width, Height, 96, 96, PixelFormats.Pbgra32, null);

        }

        public Frame(WriteableBitmap wbn)
        {
            this.Width = wbn.PixelWidth;
            this.Height = wbn.PixelHeight;

            wb = wbn;
        }

        public void ChangePixelColor(Point pos, SolidColorBrush brush, int Size)
        {
            Frame.PixelSize = Size;

            Color col = brush.Color;

            int x_true = (int)pos.X;
            int y_true = (int)pos.Y;

            int stride = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {

                byte* buffer = (byte*)wb.BackBuffer;



                for (int y = y_true; y < y_true + PixelSize; y++)
                {
                    for (int x = x_true; x < x_true + PixelSize; x++)
                    {
                        byte* pixel = buffer + y * stride + x * 4;

                        if (pixel < buffer || pixel > buffer + Height * stride)
                            continue;

                        pixel[3] = col.A;


                        pixel[0] = col.B;
                        pixel[1] = col.G;
                        pixel[2] = col.R;
                    }
                }

                wb.AddDirtyRect(new Int32Rect(0, 0, Width, Height));



            }
            wb.Unlock();

        }

        public Image sharpImage()
        {
            int width = wb.PixelWidth;
            int height = wb.PixelHeight;
            int stride = wb.BackBufferStride;

            byte[] pixels = new byte[height * stride];
            wb.CopyPixels(pixels, stride, 0);


            Image image = Image.LoadPixelData<Bgra32>(pixels, width, height)
                             .CloneAs<Rgba32>();

            return image;
        }



    }
}