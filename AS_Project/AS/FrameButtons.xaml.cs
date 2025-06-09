using Serilog;
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

        public bool isselected = true;

        private ListView ListView;
     
        public FrameButtons(ListView listview )
        {
            InitializeComponent();

            this.ListView = listview;


        }

        private void ButtonFrame_Click(object sender, RoutedEventArgs e)
        {
            ListView.SelectedItem = this;

        }
    }
}
