using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SI = SixLabors.ImageSharp;
using SWC = System.Windows.Controls;

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
            set
            {
                ((FrameButton)listview.SelectedItem).frame = value;
                Update();
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
                    value = listview.Items.Count - 1;
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
        public SWC.Image VisibleImg { get; set; }

        public bool running = false;
        private Settings setting;

        public Animation(ListView ListviewFramebutton, SWC.Image imageDraw, Settings setting)
        {
            VisibleImg = imageDraw;
            this.setting = setting;
            setting.FPS += 1;
           
            this.listview = ListviewFramebutton;
            ListviewFramebutton.Items.Clear();
            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), setting));

            Update();


        }

        public Animation(ListView ListviewFramebutton, SWC.Image imageDraw, string path)
        {
            VisibleImg = imageDraw;
            this.listview = ListviewFramebutton;
            ListviewFramebutton.Items.Clear();

            

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



            if (running && timePast >= 1000 / setting.FPS)
            {
                lasttime = now;
                NextFrame();

            }

        }

        public void SaveToSingleFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(SelectedFrame.wb));
                encoder.Save(stream);
            }
        }


        public void LoadFromSingleFile(string path)
        {
            listview.Items.Clear();

            listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), path));

            Update();
        }

        public void SaveToGif(string path)
        {
            if (listview.Items.Count == 0)
                return;

            FrameButton framebut = (FrameButton)listview.Items[0];
            SI.Image firstImage = framebut.frame.sharpImage();


            Image<Rgba32> gif = new(firstImage.Width, firstImage.Height);

            ImageFrame<Rgba32> firstFrame = (ImageFrame<Rgba32>)firstImage.Frames.RootFrame;
            GifFrameMetadata firstFrameMetadata = firstFrame.Metadata.GetGifMetadata();
            firstFrameMetadata.DisposalMethod = GifDisposalMethod.RestoreToBackground;
            firstFrameMetadata.FrameDelay = 100 / setting.FPS;
            gif.Frames.AddFrame(firstFrame);

            gif.Frames.RemoveFrame(0);



            for (int i = 1; i < listview.Items.Count; i++)
            {

                FrameButton currentFramebutton = (FrameButton)(listview.Items[i]);

                Image<Rgba32> img = (Image<Rgba32>)currentFramebutton.frame.sharpImage();
                ImageFrame<Rgba32> frame = img.Frames.RootFrame;

                GifFrameMetadata framemetadata = frame.Metadata.GetGifMetadata();
                framemetadata.DisposalMethod = GifDisposalMethod.RestoreToBackground;
                gif.Frames.AddFrame(frame);
            }


            gif.Metadata.GetGifMetadata().RepeatCount = 0;


            gif.Save(path, new GifEncoder());


        }


        public Settings LoadFromGIF(string path)
        { 
            Image<Rgba32> gif = SI.Image.Load<Rgba32>(path);



            int frameCount = gif.Frames.Count;

            int width = gif.Width;
            int heigth = gif.Height;


            ImageFrame<Rgba32> FirstFrame = gif.Frames[0];

            GifFrameMetadata metadata = FirstFrame.Metadata.GetGifMetadata();
            int frameDelay = metadata.FrameDelay;
            if (frameDelay == 0)
            {
                frameDelay = 1;
            }


            setting = new Settings(width, heigth, 100 / frameDelay);


            listview.Items.Clear();

            for (int i = 0; i < frameCount; i++)
            {


                ImageFrame<Rgba32> frame = gif.Frames[i];

                listview.Items.Add(new FrameButton(listview, (listview.Items.Count + 1).ToString(), setting));
                ((FrameButton)listview.Items[listview.Items.Count-1]).frame.wb = Frame.ToWritableBitmap(frame);
                
                

            }



            SelectedIndex = 0;

            gif.Dispose();

            setting.SaveToJsonFile("Settings.json");

            return setting;
        }
    }

}

