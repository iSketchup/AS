using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace AsClass
{
    public class Colorpallet
    {
        public static List<SolidColorBrush> ColorList = new List<SolidColorBrush>
{
    new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
    new SolidColorBrush(Color.FromArgb(255, 49, 58, 145)),
    new SolidColorBrush(Color.FromArgb(255, 76, 52, 53)),
    new SolidColorBrush(Color.FromArgb(255, 177, 72, 99)),
    new SolidColorBrush(Color.FromArgb(255, 72, 84, 84)),
    new SolidColorBrush(Color.FromArgb(255, 118, 85, 162)),
    new SolidColorBrush(Color.FromArgb(255, 146, 86, 43)),
    new SolidColorBrush(Color.FromArgb(255, 131, 133, 207)),
    new SolidColorBrush(Color.FromArgb(255, 128, 128, 120)),
    new SolidColorBrush(Color.FromArgb(255, 80, 148, 80)),
    new SolidColorBrush(Color.FromArgb(255, 205, 147, 115)),
    new SolidColorBrush(Color.FromArgb(255, 143, 191, 213)),
    new SolidColorBrush(Color.FromArgb(255, 156, 171, 177)),
    new SolidColorBrush(Color.FromArgb(255, 187, 200, 64)),
    new SolidColorBrush(Color.FromArgb(255, 156, 204, 71)),
    new SolidColorBrush(Color.FromArgb(255, 237, 230, 200))
};
        public Pen pen;
        private SolidColorBrush _Activecolor;

        public Label label;
        public SolidColorBrush Activecolor
        {
            get { return _Activecolor; }
            set
            {
                pen.ChangeColor(value);
                string hexColor = $"#{value.Color.A:X2}{value.Color.R:X2}{value.Color.G:X2}{value.Color.B:X2}";
                label.Background = value;
                label.Content = hexColor;
                _Activecolor = value;
            }
        }

        private WrapPanel wrapPanel;





        public Colorpallet(WrapPanel wrapPanel, Pen pen)
        {
            this.wrapPanel = wrapPanel;
            this.pen = pen;
        }


        public void initializeColorPallet(Label label, ColorPicker colorPicker,ScrollViewer scrollViewer)
        {
            this.label = label;

            wrapPanel.Children.Clear();

            wrapPanel.Width = scrollViewer.Width -20;

            foreach (SolidColorBrush color in ColorList)
            {
                Button button = new Button
                {
                    Background = color,
                    Width = wrapPanel.Width / 7,
                    Height = 12,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new System.Windows.Thickness(0.5),



                };



                button.Click += (s, e) =>
                {
                    Activecolor = color;
                    pen.active = true;
                    pen.isEraser = false;
                    colorPicker.SelectedColor = Activecolor.Color;
                };

                wrapPanel.Children.Add(button);

                Activecolor = ColorList[0];
            }
        }


        public static List<SolidColorBrush> LoadColorsFromGPL(string fiepath)
        {
            List<SolidColorBrush> colorList = new List<SolidColorBrush>();


            using (StreamReader stream = new StreamReader(fiepath))
            {
                string line;

                while ((line = stream.ReadLine()) != null)
                {
                    string trimmed = line.Trim();


                    if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#") || trimmed.StartsWith("GIMP") || trimmed.StartsWith("Name:"))
                        continue;


                    string[] colorsplit = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);



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
