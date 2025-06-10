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
        public Animation Animation;
        public Pen pen = new();
        public Eyedropper Eyedropper = new();
        public Colorpallet colorpallet;

        public Settings settings;

        private int tickcount = 0;

        public bool MouseButtonPressed { get; set; } = false;

        public AS_Main(Image imageDraw, Image imageBackground, WrapPanel wrapPanel, ListView listView, Settings settings)
        {
            Animation = new(listView, imageDraw);

            colorpallet = new Colorpallet(wrapPanel, pen);

            this.settings = settings;

            imageBackground.Source = Frame.BackgroundMaker(500, 240);

        }

        public void Tick()
        {
            tickcount++;

            Point pos = Mouse.GetPosition(Animation.VisibleImg);

            if (MouseButtonPressed && pen.active)
            {
                pen.Draw(Animation.SelectedFrame, pos);
            }

            else if (MouseButtonPressed && Eyedropper.active)
            {
                Eyedropper.GetColor((int)pos.X, (int)pos.Y, Animation.SelectedFrame.wb);
            }
            if (tickcount % settings.FPS == 0 && Animation.running)
            {
                Animation.Tick();
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
            Animation.Add();
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
