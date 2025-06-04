using System.Windows.Controls;
using System.Windows.Media;

namespace AsClass
{
    public class Colorpallet
    {

        private List<SolidColorBrush> brushes = new List<SolidColorBrush>
{
            new SolidColorBrush(Color.FromRgb(0, 0, 0)),       // Black
            new SolidColorBrush(Color.FromRgb(255, 255, 255)), // White
            new SolidColorBrush(Color.FromRgb(255, 0, 0)),     // Red
            new SolidColorBrush(Color.FromRgb(0, 128, 0)),     // Green
            new SolidColorBrush(Color.FromRgb(0, 0, 255)),     // Blue
            new SolidColorBrush(Color.FromRgb(255, 255, 0)),   // Yellow
            new SolidColorBrush(Color.FromRgb(255, 165, 0)),   // Orange
            new SolidColorBrush(Color.FromRgb(128, 0, 128)),   // Purple
            new SolidColorBrush(Color.FromRgb(255, 192, 203)), // Pink
            new SolidColorBrush(Color.FromRgb(165, 42, 42)),   // Brown
            new SolidColorBrush(Color.FromRgb(128, 128, 128))  // Gray
};

        public Brush Activecolor { get; set; }

        private WrapPanel wrapPanel;

        public Colorpallet(WrapPanel wrapPanel)
        {
            this.wrapPanel = wrapPanel;
        }


        public void initializeColorPallet()
        {
            foreach (var color in brushes)
            {
                Button button = new Button
                {
                    Background = color,
                    Width = 10,
                    Height = 10,

                };

                button.Click += (s, e) =>
                {
                    Activecolor = button.Background;
                };

                wrapPanel.Children.Add(button);
            }

            // Set the first color as active
            if (wrapPanel.Children.Count > 0)
            {
                Activecolor = ((Button)wrapPanel.Children[0]).Background;
            }
        }


    }
}
