using AsClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AS
{
    /// <summary>
    /// Interaction logic for WindowSettings.xaml
    /// </summary>
    public partial class WindowSettings : Window
    {

        public Settings settings;

        public int tileSize = 8;
        public WindowSettings()
        {
            InitializeComponent();

            if (settings != null && File.Exists("Settings.json"))
            {
                settings = Settings.LoadFromJson("Settings.json");

                
            }
            else
            {
                settings = new Settings();
            }

            WidthTextBox.Text = settings.FrameWidth.ToString();
            HeightTextBox.Text = settings.FrameHeight.ToString();
            FPSTextBox.Text = settings.FPS.ToString();
            SliderTileSize.Value = settings.TileSize;
            LabelTileSize.Content = $"{settings.TileSize}";
        }

        public WindowSettings(Settings settings)
        {
            InitializeComponent();

            this.settings = settings;

            WidthTextBox.Text = settings.FrameWidth.ToString();
            HeightTextBox.Text = settings.FrameHeight.ToString();
            FPSTextBox.Text = settings.FPS.ToString();
            SliderTileSize.Value = settings.TileSize;
            LabelTileSize.Content = $"{settings.TileSize}";
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WidthTextBox.Background = null;
            HeightTextBox.Background = null;
            FPSTextBox.Background = null;
           

            bool isValidWidth = int.TryParse(WidthTextBox.Text, out int width);
            bool isValidHeight = int.TryParse(HeightTextBox.Text, out int height);
            bool isValidFPS = int.TryParse(FPSTextBox.Text, out int FPS);
            tileSize = (int)SliderTileSize.Value;

            if (string.IsNullOrEmpty(WidthTextBox.Text) || isValidWidth == false)
            {
                WidthTextBox.Background = Brushes.Red;
            } 

            if (string.IsNullOrEmpty(HeightTextBox.Text) || isValidHeight == false)
            {
                HeightTextBox.Background = Brushes.Red;
            }
            
            if (string.IsNullOrEmpty(FPSTextBox.Text) ||isValidFPS == false || FPS < 0 || FPS > 60)
            {
                FPSTextBox.Background = Brushes.Red;
                FPSTextBox.Foreground  = Brushes.Maroon;
                FPSTextBox.Text = "MUST BE 1-60!";
            }


            if(string.IsNullOrEmpty(WidthTextBox.Text)==false && isValidWidth == true  && string.IsNullOrEmpty(HeightTextBox.Text) == false && isValidHeight == true && string.IsNullOrEmpty(FPSTextBox.Text) && isValidFPS == true && FPS > 0  || FPS > 0 && FPS <= 60){
               
                settings = new Settings(width, height, FPS,tileSize);


               settings.SaveToJsonFile("Settings.json");



                this.DialogResult = true;
            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void SliderTileSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(!this.IsLoaded)
                return;



            LabelTileSize.Content = $"{Math.Floor(SliderTileSize.Value)}";
          

        }
    }
}
