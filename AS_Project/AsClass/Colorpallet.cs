using System.Windows.Controls;
using System.Windows.Media;

namespace AsClass
{
    public class Colorpallet
    {

        private List<SolidColorBrush> brushes = new List<SolidColorBrush>
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


        public void initializeColorPallet()
        {
            foreach (SolidColorBrush color in brushes)
            {
                Button button = new Button
                {
                    Background = color,
                    Width = 12,
                    Height = 12,

                };

                button.Click += (s, e) =>
                {
                    Activecolor = (SolidColorBrush)button.Background;
                };

                wrapPanel.Children.Add(button);
            }
        }

    }
}
