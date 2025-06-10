using AsClass;
using Serilog;
using System.IO;
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

    private Settings _settings;
    public Settings settings
    {
        get
        {
            return _settings;
        }
        set
        {
            if (File.Exists("Settings.Json"))
            {
                _settings = Settings.LoadFromJson("Settings.Json");
            }
            else
            {
                _settings = new Settings(500, 240, 10);
            }
        }

    }

    public bool dragging = false;

    private Point lastDragPoint;


    public MainWindow()
    {
        InitializeComponent();

        settings = new Settings();

        Sigma = new(imageDraw, imageBackground, WrapColorPallet, ListviewFramebuttons, settings);

        SetBackroundColor();


        Cursor = Cursors.Pen;

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("AS.log")
            .MinimumLevel.Debug()
            .CreateLogger();


        Sigma.colorpallet.initializeColorPallet(LabelActiveColor, colorPicker);

        CompositionTarget.Rendering += Loop;

    }

    private void ButtonExita_Click(object sender, RoutedEventArgs e)
    {
        settings.SaveToJsonFile("Settings.json");
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
        ButtonExita.Background = Brushes.White;
        Menue.Background = Brushes.White;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {

    }


    private void ButtonBrush_Click(object sender, RoutedEventArgs e)
    {

        Sigma.Eyedropper.active = false;
        Sigma.pen.active = true;
        Sigma.ButtonBrush_Click(sender, e);
        Cursor = Cursors.Pen;
        Log.Debug("PenClicked");
    }

    private void ButtonEraser_Click(object sender, RoutedEventArgs e)
    {
        Sigma.Eyedropper.active = false;
        Sigma.pen.active = true;
        Sigma.ButtonEraser_Click(sender, e);
        Cursor = Cursors.Cross;
        Log.Debug("EraserClicked");
    }


    private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        Sigma.colorpallet.Activecolor = new SolidColorBrush(colorPicker.SelectedColor.Value);
        Log.Debug("Color changed to: {Color}", colorPicker.SelectedColor.Value);
    }

    private void Loop(object s, EventArgs e)
    {
        Sigma.Tick();
    }

    private void MouseLeftDown(object sender, MouseButtonEventArgs e)
    {
        Sigma.MouseButtonPressed = true;
    }

    private void MouseLeftUp(object sender, MouseButtonEventArgs e)
    {
        Sigma.MouseButtonPressed = false;
    }

    private void ButtonNewFrame_Click(object sender, RoutedEventArgs e)
    {
        Sigma.AddnewFrame();
    }

    private void ListviewFramebuttons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int selectedIndex = ListviewFramebuttons.SelectedIndex;


        foreach (FrameButton button in ListviewFramebuttons.Items)
        {
            if (button == ListviewFramebuttons.Items[selectedIndex])
            {
                button.Background = Brushes.Orange;
                button.isselected = true;


            }
            else
            {
                button.Background = Brushes.Transparent;
                button.isselected = false;


            }
        }


    }

    private void ButtonInsertColorpallet_Click(object sender, RoutedEventArgs e)
    {
        Sigma.InserColorPallet(LabelActiveColor, colorPicker);


    }

    private void MenuItem_Click_1(object sender, RoutedEventArgs e)
    {
        WindowSettings windowSettings = new WindowSettings();

        if (windowSettings.ShowDialog() == true)
        {
            Sigma.settings = windowSettings.settings;

            Log.Debug("WindowSettings dialog closed with OK");
        }

    }

    private void ButtonsNextFrane_Click(object sender, RoutedEventArgs e)
    {
        Sigma.animation.NextFrame();
    }

    private void ButtonPreviuosFrame_Click(object sender, RoutedEventArgs e)
    {
        Sigma.animation.PrevFrame();
    }

    private void ButtonStopFrame_Click(object sender, RoutedEventArgs e)
    {
        switch (Sigma.animation.running)
        {
            case true:
                Sigma.animation.running = false;
                break;

            case false:
                Sigma.animation.running = true;
                break;
        }
    }

    private void ButtonEyedropper_Click(object sender, RoutedEventArgs e)
    {
        Sigma.pen.active = false;
        Sigma.Eyedropper.active = true;

        Cursor = Cursors.UpArrow;
    }

    private void MenuItem_Click_2(object sender, RoutedEventArgs e)
    {
        settings.SaveToJsonFile("Settings.json");
        this.Close();
    }

    private void ButtonAddFrame_Click(object sender, RoutedEventArgs e)
    {
        Sigma.AddnewFrame();
    }

    private void CanvDraw_MouseWheel(object sender, MouseWheelEventArgs e)

    {
        const double zoomSpeed = 0.1;

        double oldScale = canvasScale.ScaleX;
        double newScale = oldScale;

        if (e.Delta > 0)
            newScale = oldScale + zoomSpeed;
        else if (e.Delta < 0)
            newScale = Math.Max(0.1, oldScale - zoomSpeed);

        Point mousePos = e.GetPosition(CanvDraw);

        // Verschiebung anpassen, damit Mausposition fixiert bleibt
        canvasTranslate.X -= (mousePos.X) * (newScale - oldScale);
        canvasTranslate.Y -= (mousePos.Y) * (newScale - oldScale);

        canvasScale.ScaleX = newScale;
        canvasScale.ScaleY = newScale;
    }

    private void CanvDraw_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        dragging = true;
        lastDragPoint = e.GetPosition(this);
        CanvDraw.CaptureMouse();
    }

    private void CanvDraw_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        dragging = false;
        CanvDraw.ReleaseMouseCapture();
    }

    private void CanvDraw_MouseMove(object sender, MouseEventArgs e)
    {

        if (!dragging) return;

        Point currentPoint = e.GetPosition(this);

        //if (currentPoint.X < 0 || currentPoint.Y < 0 || currentPoint.X > CanvDraw.ActualWidth || currentPoint.Y > CanvDraw.ActualHeight)
        //   return;
        // bounds berechnung


        // Mausposition relativ zum Canvas (z.B. von MouseEventArgs)
        //Point currentPoint = e.GetPosition(myCanvas);

        // Mausposition relativ zum verschobenen Inhalt
        // double adjustedX = currentPoint.X - canvasTranslate.X;
        // double adjustedY = currentPoint.Y - canvasTranslate.Y;

        // Check, ob innerhalb der sichtbaren Canvas-Grenzen (z.B. Inhaltegröße)
        // if (adjustedX < 0 || adjustedY < 0 || adjustedX > myCanvas.ActualWidth || adjustedY > myCanvas.ActualHeight)
        // {
        //    return; // Maus außerhalb des sichtbaren Inhalts -> abbrechen
        // }




        Vector delta = currentPoint - lastDragPoint;

        canvasTranslate.X += delta.X;
        canvasTranslate.Y += delta.Y;

        lastDragPoint = currentPoint;
    }
}