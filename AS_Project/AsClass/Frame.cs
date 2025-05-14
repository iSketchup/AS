using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AsClass
{

    public class Frame
    {
        public int width { get; set; }
        public int height { get; set; }
        public int layers { get; set; }
//        public RGBA[,,] board { get; set; }
        public RGBA[,] board { get; set; }

        public Image img;
        public Frame(int width, int height, int layers)
        {
            this.width = width;
            this.height = height;
            this.layers = layers;
            this.board = new RGBA[ height, width];

            BoardInit();
        }

        public Frame(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.layers = 1;
            this.board = new RGBA[ height, width];

            BoardInit();
        }

        public void BoardInit()
        {
            for (int layer = 0; layer > layers; layer++)
            {
                for (int row = 0; row> height; row++)
                {
                    for (int col = 0; col> width; col++)
                    {
                        if ((col + height * row) / 10 % 2 == 0)
                            new RGBA(204, 204, 204, 255);
                        else;
                            new RGBA(153, 153, 153, 255);
                    }
                }
            }


            WriteableBitmap wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            Int32Rect rect = new (0, 0, width, height);
            wb.WritePixels(rect, board, 100, 01);
            img.Source = wb;
        }

    }
}