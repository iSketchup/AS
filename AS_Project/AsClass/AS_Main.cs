using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace AsClass
{
    public class AS_Main
    {

        public Image VisibleImg;
        public Frame frame;
        public Pen pen = new();
        public Colorpallet colorpallet;



        public AS_Main(Image imageDraw, Image imageBackground, WrapPanel wrapPanel)
        {
            VisibleImg = imageDraw;
            frame = new Frame(500, 240, imageDraw);

            colorpallet = new Colorpallet(wrapPanel, pen);


            imageBackground.Source = Frame.BackgroundMaker(500, 240);
        }

        public void Tick()
        {
            Point pos = Mouse.GetPosition(VisibleImg);
            pen.Draw(frame, pos);
        }

        public void ButtonBrush_Click(object sender, RoutedEventArgs e)
        {
            pen.ChangeColor(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)));
        }
        public void ButtonEraser_Click(object sender, RoutedEventArgs e)
        {
            pen.ChangeColor(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)));
           
        }


        public void AddnewFrame()
        {
            // TODO für LUis neues Frame erstellen

            


              
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

                colorpallet.ColorList.Clear();

                colorpallet.ColorList = Colorpallet.LoadColorsFromGPL(dateipfad);
                colorpallet.initializeColorPallet(label,colorPicker);

                
            }
        }
    }
}
