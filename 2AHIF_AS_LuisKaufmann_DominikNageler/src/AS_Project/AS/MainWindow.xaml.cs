using AsClass;
using Microsoft.Win32;
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
    private Brush BackroundColor = new SolidColorBrush(Color.FromArgb(255, 136, 121, 105));


    private AS_Main Sigma;

    private Settings _settings;

    private AsClass.Frame CopiedFrame;
    public Setter test;
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
                _settings = new Settings(500, 240, 10, 8);
            }
        }

    }

    public bool dragging = false;

    private Point lastDragPoint;



    public MainWindow()
    {
        InitializeComponent();

        settings = new Settings();
        AsClass.Frame.defaultBgPixelSize = settings.TileSize;
        Sigma = new(imageDraw, imageBackground, WrapColorPallet, ListviewFramebuttons, settings);

        SetBackroundColor();



        Cursor = Cursors.Pen;

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("AS.log")
            .MinimumLevel.Debug()
            .CreateLogger();


        Sigma.colorpallet.initializeColorPallet(LabelActiveColor, colorPicker, ScrollviewerColorpallet);

        CompositionTarget.Rendering += Loop;



    }

    private void ButtonExita_Click(object sender, RoutedEventArgs e)
    {

        settings = Settings.LoadFromJson("Settings.json");
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
        Sigma.pen.isEraser = false;
        Sigma.ButtonBrush_Click(sender, e);
        Cursor = Cursors.Pen;
        Log.Debug("PenClicked");
    }

    private void ButtonEraser_Click(object sender, RoutedEventArgs e)
    {
        Sigma.Eyedropper.active = false;
        Sigma.pen.active = false;
        Sigma.pen.isEraser = true;
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

        Sigma.MarkActiveTool(ButtonBrush, Sigma.pen.active);
        Sigma.MarkActiveTool(ButtonEraser, Sigma.pen.isEraser);
        Sigma.MarkActiveTool(ButtonEyedropper, Sigma.Eyedropper.active);
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
        if (!this.IsLoaded)
            return;



        Sigma.animation.Update();





    }

    private void ButtonInsertColorpallet_Click(object sender, RoutedEventArgs e)
    {
        Sigma.InserColorPallet(LabelActiveColor, colorPicker, ScrollviewerColorpallet);


    }

    private void MenuItem_Click_1(object sender, RoutedEventArgs e)
    {
        if (File.Exists("Settings.json"))
        {

            WindowSettings windowSettings = new WindowSettings(Settings.LoadFromJson("Settings.json"));

            if (windowSettings.ShowDialog() == true)
            {

                Sigma.settings = windowSettings.settings;
                AsClass.Frame.defaultBgPixelSize = settings.TileSize;

                Log.Debug("WindowSettings dialog closed with OK");


            }
        }
        else
        {
            WindowSettings windowSettings = new WindowSettings();

            if (windowSettings.ShowDialog() == true)
            {

                Sigma.settings = windowSettings.settings;
                AsClass.Frame.defaultBgPixelSize = settings.TileSize;
                Log.Debug("WindowSettings dialog closed with OK");

            }
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
                System.Windows.Shapes.Path pathStart = new System.Windows.Shapes.Path
                {
                    Data = Geometry.Parse("m11.596 8.697-6.363 3.692c-.54.313-1.233-.066-1.233-.697V4.308c0-.63.692-1.01 1.233-.696l6.363 3.692a.802.802 0 0 1 0 1.393"),
                    Fill = Brushes.Black,
                    Margin = new Thickness(0, 0, 5, 2)

                };

                ButtonStopFrame.Content = pathStart;
                break;

            case false:
                Sigma.animation.running = true;

                System.Windows.Shapes.Path pathstop = new System.Windows.Shapes.Path
                {
                    Data = Geometry.Parse("M6.25 5C5.56 5 5 5.56 5 6.25v3.5a1.25 1.25 0 1 0 2.5 0v-3.5C7.5 5.56 6.94 5 6.25 5m3.5 0c-.69 0-1.25.56-1.25 1.25v3.5a1.25 1.25 0 1 0 2.5 0v-3.5C11 5.56 10.44 5 9.75 5"),
                    Fill = Brushes.Black,
                    Margin = new Thickness(0, 0, 5, 2),

                };


                ButtonStopFrame.Content = pathstop;
                break;
        }
    }

    private void ButtonEyedropper_Click(object sender, RoutedEventArgs e)
    {
        Sigma.pen.active = false;
        Sigma.Eyedropper.active = true;
        Sigma.pen.isEraser = false;

        Cursor = Cursors.UpArrow;
    }

    private void MenuItem_Click_2(object sender, RoutedEventArgs e)
    {
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

    private void ButtonResetCanv_Click(object sender, RoutedEventArgs e)
    {
        canvasTranslate.X = 1;
        canvasTranslate.Y = 1;


        canvasScale.ScaleX = 1;
        canvasScale.ScaleY = 1;
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

        if (!dragging)
            return;

        Point currentPoint = e.GetPosition(this);

        Vector delta = currentPoint - lastDragPoint;

        canvasTranslate.X += delta.X;
        canvasTranslate.Y += delta.Y;

        lastDragPoint = currentPoint;
    }

    private void KeyHandler(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.B:
                ButtonBrush_Click(sender, e);
                break;
            case Key.E:
                ButtonEraser_Click(sender, e);
                break;
            case Key.I:
                ButtonEyedropper_Click(sender, e);
                break;
            case Key.Space:
                ButtonStopFrame_Click(sender, e);
                break;
            case Key.Left:
                ButtonPreviuosFrame_Click(sender, e);
                break;
            case Key.Right:
                ButtonsNextFrane_Click(sender, e);
                break;





        }

        if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt && e.SystemKey == Key.N)
        {
            ButtonAddFrame_Click(sender, e);
            return;
        }

        if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
        {
            Sigma.settings = settings;
        }
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
        {
            ButtonGIFSave_Click(sender, e);
        }
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.C)
        {
            ButtonCopy_Click(sender, e);
        }
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.V)
        {
            ButtonPaste_Click(sender, e);
        }

    }

    [STAThread]
    private void ButtonGIFSave_Click(object sender, RoutedEventArgs e)
    {

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Speichern unter...";
        dialog.Filter = "GIF-Dateien (*.gif)|*.gif";


        if (dialog.ShowDialog() == true)
        {
            string path = dialog.FileName;

            Sigma.SaveToGif(path);

        }
    }
    private void ButtonPNGSave_Click(object sender, RoutedEventArgs e)
    {

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Speichern unter...";
        dialog.Filter = "PNG-Dateien (*.png)|*.png";

        if (dialog.ShowDialog() == true)
        {
            string path = dialog.FileName;

            Sigma.SaveCurrentFrame(path);

        }
    }
    private void ButtonLoad_Click(object sender, RoutedEventArgs e)

    {

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Bitte wähle eine Datei aus";
        dialog.Filter = "GIF-Dateien (*.gif)|*.gif";
        //        dialog.Filter = "Png-Dateien (*.png)|*.png";

        if (dialog.ShowDialog() == true)
        {
            string path = dialog.FileName;

            Sigma.LoadFromGIF(path);

            System.Windows.Shapes.Path pathStart = new System.Windows.Shapes.Path
            {
                Data = Geometry.Parse("m11.596 8.697-6.363 3.692c-.54.313-1.233-.066-1.233-.697V4.308c0-.63.692-1.01 1.233-.696l6.363 3.692a.802.802 0 0 1 0 1.393"),
                Fill = Brushes.Black,
                Margin = new Thickness(0, 0, 5, 2)

            };

            ButtonStopFrame.Content = pathStart;

        }
    }

    private void ButtonLoadasPng(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Bitte wähle eine Datei aus";
        dialog.Filter = "png-Dateien (*.png)|*.png";

        if (dialog.ShowDialog() == true)
        {
            string path = dialog.FileName;

            Sigma.LoadFrom(path);

            System.Windows.Shapes.Path pathStart = new System.Windows.Shapes.Path
            {
                Data = Geometry.Parse("m11.596 8.697-6.363 3.692c-.54.313-1.233-.066-1.233-.697V4.308c0-.63.692-1.01 1.233-.696l6.363 3.692a.802.802 0 0 1 0 1.393"),
                Fill = Brushes.Black,
                Margin = new Thickness(0, 0, 5, 2)

            };

            ButtonStopFrame.Content = pathStart;

        }
    }



    private void ButtonCopy_Click(object sender, RoutedEventArgs e)
    {

        Sigma.SaveCurrentFrame("CopiedFrame.png");

    }

    private void ButtonPaste_Click(object sender, RoutedEventArgs e)
    {

        if (File.Exists("CopiedFrame.png"))
        {
            Sigma.animation.SelectedFrame = AsClass.Frame.LoadFrameFrom("CopiedFrame.png");

        }


    }


    private void ButtonDeleteFrame_Click(object sender, RoutedEventArgs e)
    {
        int selectedIndex = ListviewFramebuttons.SelectedIndex;
        if (Sigma.animation.listview != null && ListviewFramebuttons.Items.Count != 1)
        {
            Sigma.animation.listview.Items.RemoveAt(ListviewFramebuttons.SelectedIndex);
            Sigma.animation.SelectedIndex -= 1;
            for (int i = 0; i < ListviewFramebuttons.Items.Count; i++)
            {
                FrameButton frameButton = (FrameButton)ListviewFramebuttons.Items[i];
                frameButton.Content = $"{i + 1}";


            }
            ListviewFramebuttons.SelectedIndex = selectedIndex - 1;

        }
    }

    private void myListView_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space || e.Key == Key.Enter)
        {
            e.Handled = true;
            ButtonStopFrame_Click(sender, e);
        }
    }


    private void Slider_PreviewKeydown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Left || e.Key == Key.Right)
        {
            e.Handled = true;

        }
    }

}