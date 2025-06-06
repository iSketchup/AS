using AsClass;
using Serilog;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace AS;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Brush BackroundColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));


    private AS_Main Sigma;
    private bool MousButtonPressed = false;




    public MainWindow()
    {
        InitializeComponent();

        Sigma = new(imageDraw, imageBackground, WrapColorPallet);

        // Set the background color of the window
        SetBackroundColor();


        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("AS.log")
            .MinimumLevel.Debug()
            .CreateLogger();


        Sigma.colorpallet.initializeColorPallet(RectangleActiveColor, LabelHexCode, colorPicker);

        CompositionTarget.Rendering += Loop;

    }

    private void ButtonExita_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void SliderPenSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        LabelPensize.Content = $"Pen Size: {Math.Floor(SliderPenSize.Value)}";
        if (this.IsLoaded)
            Sigma.pen.Size = (int)(SliderPenSize.Value);

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


    private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        Sigma.pen.color = new SolidColorBrush(colorPicker.SelectedColor.Value);
        LabelActiveColor.Background = Sigma.pen.color;
    }

    private void Loop(object s, EventArgs e)
    {
        if (MousButtonPressed)
            Sigma.Tick();
    }

    private void MouseLeftDown(object sender, MouseButtonEventArgs e)
    {
        MousButtonPressed = true;
    }

    private void MouseLeftUp(object sender, MouseButtonEventArgs e)
    {
        MousButtonPressed = false;
    }
}