using AsClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public WindowSettings()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WidthTextBox.Background = null;
            HeightTextBox.Background = null;
            FPSTextBox.Background = null;

            bool isValidWidth = int.TryParse(WidthTextBox.Text, out int width);
            bool isValidHeight = int.TryParse(HeightTextBox.Text, out int height);
            bool isValidFPS = int.TryParse(FPSTextBox.Text, out int FPS);

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
                FPSTextBox.Text = "FPS must be between 0 and 60";
            }


            if(string.IsNullOrEmpty(WidthTextBox.Text)==false && isValidWidth == true  && string.IsNullOrEmpty(HeightTextBox.Text) == false && isValidHeight == true && string.IsNullOrEmpty(FPSTextBox.Text) && isValidFPS == true && FPS >0 || FPS <= 60){
               
                settings = new Settings(width, height, FPS);


                this.DialogResult = true;
            }


        }
    }
}
