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


        public bool MouseButtonPressed { get; set; } = false;

        private Settings _settings;
        public Settings settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                animation = new(animation.listview, animation.VisibleImg, value);

                imageBackground.Source = Frame.BackgroundMaker(value.FrameWidth, value.FrameHeight);
               
            }
        }
        public Image imageBackground { get; set; }

        public AS_Main(Image imageDraw, Image imageBackground, WrapPanel wrapPanel, ListView listView, Settings settings)
        {
            animation = new(listView, imageDraw, settings);
            _settings = settings;
            colorpallet = new Colorpallet(wrapPanel, pen);

            Eyedropper = new Eyedropper(colorpallet);


            imageBackground.Source = Frame.BackgroundMaker(settings.FrameWidth, settings.FrameHeight);
            this.imageBackground = imageBackground;





        }

        public void Tick()
        {
            Point pos = Mouse.GetPosition(animation.VisibleImg);

            if (MouseButtonPressed &&( pen.active|| pen.isEraser))
            {
                pen.Draw(animation.SelectedFrame, pos);
            }

            else if (MouseButtonPressed && Eyedropper.active)
            {
                Eyedropper.GetColor((int)pos.X, (int)pos.Y, animation.SelectedFrame.wb);
            }
                animation.Tick();
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

        public void MarkActiveTool(Button button, bool isactiv)
        {
            if (isactiv)
            {
                button.BorderBrush = Brushes.Orange;
                button.BorderThickness = new Thickness(2);
            }
            else
            {
                button.BorderBrush = Brushes.Black;
                button.BorderThickness = new Thickness(0.5);
            }
        }

        public void LoadFrom(string path)
        {
            animation = new(animation.listview, animation.VisibleImg, path);
        }
    }
}
