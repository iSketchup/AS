using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AS
{
    /// <summary>
    /// Interaction logic for FrameButtons.xaml
    /// </summary>
    public partial class FrameButtons : UserControl
    {

        public bool isselected = false;
        private StackPanel StackPanel;
        public FrameButtons(StackPanel stackPanelButtons)
        {
            InitializeComponent();

            ButtonFrame.BorderBrush = Brushes.Transparent;
            this.StackPanel = stackPanelButtons;



            if(isselected == false)
            {
                ButtonFrame.BorderBrush = Brushes.Transparent;
            }
        }

        private void ButtonFrame_Click(object sender, RoutedEventArgs e)
        {
            isselected = true;

            ButtonFrame.BorderBrush = Brushes.Orange;


            FrameButtons ClickedButton = sender as FrameButtons;

            foreach (var child in StackPanel.Children)
            {
                if (child is FrameButtons btn && btn != ClickedButton)
                {
                    isselected = false;
                   this.BorderBrush = Brushes.Transparent;
                }
            }
        }
    }
}
