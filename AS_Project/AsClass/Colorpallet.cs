using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using Xceed.Wpf.Toolkit;

namespace AsClass
{
    public class Colorpallet
    {

        public List<SolidColorBrush> ColorList = new List<SolidColorBrush>
{
            new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),       // Black
            new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)), // White
            new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)),     // Red
            new SolidColorBrush(Color.FromArgb(255, 0, 128, 0)),     // Green
            new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)),     // Blue
            new SolidColorBrush(Color.FromArgb(255, 255, 255, 0)),   // Yellow
            new SolidColorBrush(Color.FromArgb(255, 255, 165, 0)),   // Orange
            new SolidColorBrush(Color.FromArgb(255, 128, 0, 128)),   // Purple
            new SolidColorBrush(Color.FromArgb(255, 255, 192, 203)), // Pink
            new SolidColorBrush(Color.FromArgb(255, 165, 42, 42)),   // Brown
            new SolidColorBrush(Color.FromArgb(255, 128, 128, 128))  // Gray
};  

        public Pen pen;
        private SolidColorBrush _Activecolo;
        public SolidColorBrush Activecolor { get { return _Activecolo; }
            set {
                pen.ChangeColor(value);
                _Activecolo = value; } }

        private WrapPanel wrapPanel;

      

        public Colorpallet(WrapPanel wrapPanel, Pen pen)
        {
            this.wrapPanel = wrapPanel;
            this.pen = pen;
        }


        public void initializeColorPallet( Label label , ColorPicker colorPicker)
        {
            foreach (SolidColorBrush color in ColorList)
            {
                Button button = new Button
                {
                    Background = color,
                    Width = wrapPanel.Width/7,
                    Height = 12,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new System.Windows.Thickness(0.5),


                };

                button.Click += (s, e) =>
                {
                    Activecolor = color;
                    label.Content = Activecolor.Color.ToString();
                    colorPicker.SelectedColor = Activecolor.Color;


                };

                wrapPanel.Children.Add(button);
                
            }
        }


        public static List<SolidColorBrush> LoadColorsFromGPL(string fiepath)
        {
            List<SolidColorBrush> colorList = new List<SolidColorBrush>();


            using (StreamReader stream = new StreamReader(fiepath))
            {
                string line = stream.ReadToEnd();

                while ((line = stream.ReadLine()) != null)
                {
                    string trimmed= line.Trim();


                    if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#") || trimmed.StartsWith("GIMP") || trimmed.StartsWith("Name:"))
                        continue;


                    string[] colorsplit = trimmed.Split(' ');


                    if (colorsplit.Length >= 3 &&
                   byte.TryParse(colorsplit[0], out byte r) &&
                   byte.TryParse(colorsplit[1], out byte g) &&
                   byte.TryParse(colorsplit[2], out byte b))
                    {
                        Color color = Color.FromRgb(r, g, b);
                        colorList.Add(new SolidColorBrush(color));
                    }
                }
            }

            return colorList;

            
        }

      

    }
}
