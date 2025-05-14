using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AS
{
    /// <summary>
    /// Interaction logic for TAB.xaml
    /// </summary>
    public partial class TAB : UserControl
    {

        private ListView listView;
        private List<Button> ButtonList;
        public TAB(List buttonlist)
        {
            InitializeComponent();
            listView = new ListView();
           

        }

        private void ButtonTAB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonTAB_GotMouseCapture(object sender, MouseEventArgs e)
        {
            
        }
    }
}
