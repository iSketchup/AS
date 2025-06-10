using System.Windows.Controls;

namespace AsClass
{
    public class Animation
    {
        public Frame SelectedFrame
        {
            get
            {
                return ((FrameButton)listview.SelectedItem).frame;
            }
        }
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

        public ListView listview;
        public Image VisibleImg { get; set; }

        public bool running = false;
        private Settings setting;

        public Animation(ListView ListviewFramebutton, Image imageDraw, Settings setting)
        {
            VisibleImg = imageDraw;
            this.setting = setting;

            this.listview = ListviewFramebutton;
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), setting));

            Update();


        }

        public void Update()
        {
            FrameButton frameButton = (FrameButton)listview.SelectedItem;
            if (frameButton != null)
            {
                VisibleImg.Source = frameButton.frame.wb;

            }
        }

        public void Add()
        {
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), setting));
            SelectedIndex = listview.Items.Count - 1;

        }


        public void NextFrame()
        {
            SelectedIndex++;
        }


        public void PrevFrame()
        {
            SelectedIndex--;
        }

        public void Tick()
        {
            NextFrame();
        }
    }

}

