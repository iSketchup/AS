
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WriteableBitmap wb = new WriteableBitmap(100, 100, 96, 96, PixelFormats.Bgra32, null);

        public MainWindow()
        {
            InitializeComponent();

            ZeigeRotesBild((byte)beep.Value);
            img.Source = wb;
        }

        void ZeigeRotesBild(byte beep)
        {
            int stritde = wb.BackBufferStride;

            wb.Lock();

            unsafe
            {
                byte* buffer = (byte*)wb.BackBuffer;

                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {

                        byte* pixel = buffer + y * stritde + x * 4;

                        pixel[3] = beep;

                            if (((x / 10) + (y / 10)) % 2 == 0)
                        {
                            pixel[0] = 153;
                            pixel[1] = 153;
                            pixel[2] = 153;
                            }
                        else
                        {
                            pixel[0] = 204;
                            pixel[1] = 204;
                            pixel[2] = 204;
                        }


                            
                            }
                    }

            wb.AddDirtyRect(new Int32Rect(0, 0, 100, 100));



                    }
            wb.Unlock();
        }

        private void beep_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            ZeigeRotesBild((byte)beep.Value);
        }
    }
}