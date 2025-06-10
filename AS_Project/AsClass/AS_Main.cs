using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace AsClass
{
    public class AS_Main
    {
        public Animation animation;
        public Pen pen = new();
        public Eyedropper Eyedropper;
        public Colorpallet colorpallet;

        private int tickcount = 0;


        public bool MouseButtonPressed { get; set; } = false;

        private Settings _settings;
        public Settings settings
        {
            get { return _settings; }
            set
            {

              
               
                animation = new(animation.listview, animation.VisibleImg, _settings);

                imageBackground.Source = Frame.BackgroundMaker(settings.FrameWidth, settings.FrameHeight);
                
            }
        }
        public Image imageBackground { get; set; }

        public AS_Main(Image imageDraw, Image imageBackground, WrapPanel wrapPanel, ListView listView)
        {
            if (File.Exists("Settings.Json"))
            {
                _settings = Settings.LoadFromJson("Settings.Json");
            }
            else
            {
                _settings = new Settings();
            }

            animation = new(listView, imageDraw, settings);

            colorpallet = new Colorpallet(wrapPanel, pen);

            Eyedropper = new Eyedropper(colorpallet);


            this.imageBackground = imageBackground;

       


        }

        public void Tick()
        {
            tickcount++;


            Point pos = Mouse.GetPosition(animation.VisibleImg);

            if (MouseButtonPressed && pen.active)
            {
                pen.Draw(animation.SelectedFrame, pos);
            }

            else if (MouseButtonPressed && Eyedropper.active)
            {
                Eyedropper.GetColor((int)pos.X, (int)pos.Y, animation.SelectedFrame.wb);
            }
            if (tickcount % settings.FPS == 0 && animation.running)
            {
                animation.Tick();
            }
        }

        public void ButtonBrush_Click(object sender, RoutedEventArgs e)
        {
            pen.ChangeColor(colorpallet.Activecolor);
        }
        public void ButtonEraser_Click(object sender, RoutedEventArgs e)
        {
            pen.ChangeColor(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)));

        }









        public void AddnewFrame()
        {
            animation.Add();
        }

        [STAThread]

        public void InserColorPallet(Label label, ColorPicker colorPicker)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Bitte wähle eine Datei aus";
            dialog.Filter = "GPL-Dateien (*.gpl)|*.gpl";

            if (dialog.ShowDialog() == true)
            {
                string dateipfad = dialog.FileName;



                Colorpallet.ColorList = Colorpallet.LoadColorsFromGPL(dateipfad);
                colorpallet.initializeColorPallet(label, colorPicker);


            }
        }
    }
}
