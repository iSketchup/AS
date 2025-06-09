using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace AsClass
{
    public class AS_Main
    {
        public Animation Animation;
        public Pen pen = new();
        public Colorpallet colorpallet;

        public Settings settings;

        public Point pos;
        public Eyedropper eyedropper;
        public AS_Main(Image imageDraw, Image imageBackground, WrapPanel wrapPanel, ListView listView,Settings settings)
        {
            Animation = new(listView, imageDraw);

            colorpallet = new Colorpallet(wrapPanel, pen);
            
            this.settings = settings;

            imageBackground.Source = Frame.BackgroundMaker(500, 240);
        }

        public void Tick()
        {
            pos = Mouse.GetPosition(Animation.VisibleImg);
            pen.Draw(Animation.SelectedFrame, pos);
        }

        public void ButtonBrush_Click(object sender, RoutedEventArgs e)
        {
            pen.ChangeColor(colorpallet.Activecolor);
        }
        public void ButtonEraser_Click(object sender, RoutedEventArgs e)
        {
            pen.ChangeColor(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)));
           
        }

        public void Eydropper_click(object sender, RoutedEventArgs e)
        {
            eyedropper = new Eyedropper((Frame)Animation.SelectedFrame, pos);

            eyedropper.GetColor();

            colorpallet.Activecolor = new SolidColorBrush(eyedropper.SelectedColor);

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

            if (dialog.ShowDialog() ==true)
            {
                string dateipfad = dialog.FileName;

            

                colorpallet.ColorList = Colorpallet.LoadColorsFromGPL(dateipfad);
                colorpallet.initializeColorPallet(label,colorPicker);

                
            }
        }
    }
}
