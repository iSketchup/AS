using System.Windows.Controls;

namespace AsClass
{
    public class AS_Main
    {
        public Image VisibleImg;
        public Frame frame;
        public Colorpallet colorpallet;

        public AS_Main(Image imageDraw, Image imageBackground,WrapPanel wrapPanel)
        {
            VisibleImg = imageDraw;
            frame = new Frame(500, 150, imageDraw);

            colorpallet = new Colorpallet(wrapPanel);


            imageBackground.Source = Frame.BackgroundMaker(500, 300);
        }
    }
}
