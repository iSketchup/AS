using System.Windows.Controls;

namespace AsClass
{
    public class AS_Main
    {
        public Image VisibleImg;

        public AS_Main(Image img)
        {
            VisibleImg = img;

            Frame frame = new Frame(500, 300, img);
        }
    }
}
