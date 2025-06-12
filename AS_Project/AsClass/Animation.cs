using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
                    value = listview.Items.Count-1;
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
        private TimeOnly lasttime = TimeOnly.FromDateTime(DateTime.Now);

        public ListView listview;
        public Image VisibleImg { get; set; }

        public bool running = false;
        private Settings setting;

        public Animation(ListView ListviewFramebutton, Image imageDraw, Settings setting)
        {
            VisibleImg = imageDraw;
            this.setting = setting;

            this.listview = ListviewFramebutton;
            ListviewFramebutton.Items.Clear();
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), setting));

            Update();


        }

        public Animation(ListView ListviewFramebutton, Image imageDraw, string path)
        {
            VisibleImg = imageDraw;
            this.listview = ListviewFramebutton;
            ListviewFramebutton.Items.Clear();
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), path));

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

            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);

            int timePast = (int)(now - lasttime).TotalMilliseconds;



            if (running && timePast >= 1000/ setting.FPS)
            {
                lasttime = now;
                NextFrame();

            }

        }

        internal void SaveTo(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(SelectedFrame.wb));
                encoder.Save(stream);
            }
        }
    }

}

