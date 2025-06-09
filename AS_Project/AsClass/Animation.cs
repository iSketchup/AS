using System.Windows.Controls;

namespace AsClass
{
    public class Animation
    {
        public Frame SelctedFrame { get; set; }
        public Image SelectedImg { get; set; }

        public int Framecount = 0;

        private ListView listview;

        public Animation(ListView ListviewFramebutton, Image imageDraw)
        {
            this.listview = ListviewFramebutton;
            Add();

            SelctedFrame = new Frame(500, 240, imageDraw);

            SelectedImg = imageDraw;

        }

        public void Add()
        {
            Framecount++;
            listview.Items.Add(new FrameButton(listview, Framecount.ToString()));
        }


        public void NextFrame()
        {
            listview.SelectedIndex++;
        }


        public void PrevFrame()
        {
            listview.SelectedIndex--;
        }
    }

}

