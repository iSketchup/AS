using System.Windows.Controls;
using System.Windows.Documents;

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

        private ListView listview;
        public Image VisibleImg { get; set; }


        public Animation(ListView ListviewFramebutton, Image imageDraw)
        {
            VisibleImg = imageDraw;


            this.listview = ListviewFramebutton;
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString()));

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

