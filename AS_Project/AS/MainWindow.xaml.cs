using AsClass;
using Serilog;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AS;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Brush BackroundColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
    private Brush ButtonColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

    private AS_Main Sigma;

    public MainWindow()
    {
        InitializeComponent();

        Sigma = new (imageDraw, imageBackground);

        // Set the background color of the window
        SetBackroundColor();


        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("AS.log")
            .MinimumLevel.Debug()
            .CreateLogger();


    }

    private void ButtonExita_Click(object sender, RoutedEventArgs e)
    {
       this.Close();
    }

    private void SliderPenSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        LabelPensize.Content = $"Pen Size: {Math.Floor( SliderPenSize.Value)}"; 
    }





    private void SetBackroundColor()
    {


       Grid.Background = BackroundColor;

        // Buttons

        Menue.Background = ButtonColor;
        ButtonExita.Background = ButtonColor;
        ButtonBrush.Background = ButtonColor;
        ButtonEaraser.Background = ButtonColor;
        ButtonFill.Background = ButtonColor;
        LabelPensize.Background = ButtonColor;
        listviewFrames.Background = ButtonColor;
        ButtonsNextFrane.Background = ButtonColor;
        ButtonPreviuosFrame.Background = ButtonColor;
        ButtonStopFrame.Background = ButtonColor;






    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        
    }


    private void MouseLeftDown(object sender, MouseButtonEventArgs e)
    {
        Point pos = e.GetPosition((IInputElement)sender);
        Sigma.frame.Pixelmade(pos.X, pos.Y);
    }
}