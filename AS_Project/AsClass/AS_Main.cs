using System.Windows.Controls;

namespace AsClass
{
    public class AS_Main
    {
        public Image VisibleImg;
        public Frame frame;

        public AS_Main(Image imageDraw, Image imageBackground)
        {
            VisibleImg = imageDraw;
            frame = new Frame(500, 300, imageDraw);


            imageBackground.Source = Frame.BackgroundMaker(500, 300);
        }
    }
}
