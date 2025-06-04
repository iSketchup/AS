using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AsClass
{
    public class AS_Main
    {
        public Image VisibleImg;
        public Frame frame;
        public Colorpallet colorpallet;
        public Pen pen = new();

        public AS_Main(Image imageDraw, Image imageBackground,WrapPanel wrapPanel)
        {
            VisibleImg = imageDraw;
            frame = new Frame(500, 240, imageDraw);

            colorpallet = new Colorpallet(wrapPanel);


            imageBackground.Source = Frame.BackgroundMaker(500, 300);
        }
        public void MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition((IInputElement)sender);
            frame.ChangePixelColor(pos, new SolidColorBrush(Color.FromArgb(255, 255, 255, 245)));
        }
    }
}
