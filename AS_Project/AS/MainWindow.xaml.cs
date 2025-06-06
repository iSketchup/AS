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
   

    private AS_Main Sigma;

   

    public MainWindow()
    {
        InitializeComponent();

        Sigma = new (imageDraw, imageBackground,WrapColorPallet);

        // Set the background color of the window
        SetBackroundColor();


        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("AS.log")
            .MinimumLevel.Debug()
            .CreateLogger();


        Sigma.colorpallet.initializeColorPallet(RectangleActiveColor,LabelHexCode, colorPicker);



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

        Log.Debug("Setting background color");
        Grid.Background = BackroundColor;
        WrapColorPallet.Background = Brushes.White;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        
    }


    private void MouseLeftDown(object sender, MouseButtonEventArgs e)
    {
        Sigma.MouseLeftDown(sender, e);
    }

    private void ButtonBrush_Click(object sender, RoutedEventArgs e)
    {
        Sigma.ButtonBrush_Click(sender, e);
        Log.Debug("PenClicked");
    }

    private void ButtonEraser_Click(object sender, RoutedEventArgs e)
    {
        Sigma.ButtonEraser_Click(sender, e);
        Log.Debug("EraserClicked");
    }

    private void MouseLeftUp(object sender, MouseButtonEventArgs e)
    {
        Sigma.MouseLeftUp(sender, e);
    }
}