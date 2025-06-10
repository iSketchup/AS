using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace AsClass
{
    public class AS_Main
    {
        public Animation animation;
        public Pen pen = new();
        public Eyedropper Eyedropper ;
        public Colorpallet colorpallet;

        public Settings settings;

        private int tickcount = 0;

        public bool MouseButtonPressed { get; set; } = false;

        public AS_Main(Image imageDraw, Image imageBackground, WrapPanel wrapPanel, ListView listView, Settings settings)
        {
            animation = new(listView, imageDraw);

            colorpallet = new Colorpallet(wrapPanel, pen);

            Eyedropper = new Eyedropper(colorpallet);

            this.settings = settings;

            imageBackground.Source = Frame.BackgroundMaker(300, 240);

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
