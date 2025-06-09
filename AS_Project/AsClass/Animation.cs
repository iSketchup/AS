using System.Windows.Controls;

namespace AsClass
{
    public class Animation
    {
        public Frame SelectedFrame
        {
            get
            {
                return (Frame)((FrameButton)listview.SelectedItem).frame;
            }
        }
        public Image VisibleImg { get; set; }

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (value < 0)
                {
                    value = listview.Items.Count;
                }
                if (value >= listview.Items.Count)
                {
                    value = 0;
                }

                listview.SelectedIndex = value;
                _selectedIndex = value;
                Update();
            }
        }

        private ListView listview;

        public Animation(ListView ListviewFramebutton, Image imageDraw)
        {
            VisibleImg = imageDraw;
            this.listview = ListviewFramebutton;
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString()));


        }

        public void Update()
        {
            FrameButton frameButton = (FrameButton)listview.SelectedItem;
            VisibleImg.Source = frameButton.frame.wb;
        }

        public void Add()
        {
            SelectedIndex = listview.Items.Count;
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString()));
        }


        public void NextFrame()
        {
            SelectedIndex++;
        }


        public void PrevFrame()
        {
            SelectedIndex--;
        }
    }

}

