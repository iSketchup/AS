using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace AsClass
{
    public class Colorpallet
    {
        private List<Color> Brushes = new List<Color>
        {
            Colors.Black,
            Colors.White,
            Colors.Red,
            Colors.Green,
            Colors.Blue,
            Colors.Yellow,
            Colors.Orange,
            Colors.Purple,
            Colors.Pink,
            Colors.Brown,
            Colors.Gray
        };

        public Brush Activecolor { get; set; }

        private WrapPanel wrapPanel;

        public Colorpallet(WrapPanel wrapPanel)
        {
          this.wrapPanel = wrapPanel;
        }


        public void initializeColorPallet()
        {
            foreach (var color in Brushes)
            {
                Button button = new Button
                {
                    Background = new SolidColorBrush(color),
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
