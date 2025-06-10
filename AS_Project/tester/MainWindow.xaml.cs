
using AsClass;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Media.Media3D;


namespace tester
{

        public partial class MainWindow : Window
        {
            private Point lastDragPoint;
            private bool isDragging = false;

            public MainWindow()
            {
                InitializeComponent();
            }

            private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
            {
                const double zoomSpeed = 0.1;

                double oldScale = canvasScale.ScaleX;
                double newScale = oldScale;

                if (e.Delta > 0)
                    newScale = oldScale + zoomSpeed;
                else if (e.Delta < 0)
                    newScale = Math.Max(0.1, oldScale - zoomSpeed);

                Point mousePos = e.GetPosition(MyCanvas);

                // Verschiebung anpassen, damit Mausposition fixiert bleibt
                canvasTranslate.X -= (mousePos.X) * (newScale - oldScale);
                canvasTranslate.Y -= (mousePos.Y) * (newScale - oldScale);

                canvasScale.ScaleX = newScale;
                canvasScale.ScaleY = newScale;
            }

            private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                isDragging = true;
                lastDragPoint = e.GetPosition(this);
                MyCanvas.CaptureMouse();
            }

            private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {
                isDragging = false;
                MyCanvas.ReleaseMouseCapture();
            }

            private void Canvas_MouseMove(object sender, MouseEventArgs e)
            {
                if (!isDragging) return;

                Point currentPoint = e.GetPosition(this);
                Vector delta = currentPoint - lastDragPoint;

                canvasTranslate.X += delta.X;
                canvasTranslate.Y += delta.Y;

                lastDragPoint = currentPoint;
            }
        }
    }
