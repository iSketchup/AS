using System.Windows.Controls;

namespace AsClass
{
    /// <summary>
    /// Interaction logic for FrameButton.xaml
    /// </summary>
    public partial class FrameButton : UserControl
    {
        public ListView ListView;
        public bool isselected = true;
        public Frame frame;

        public FrameButton(ListView listview, string name, Settings settings)
        {
            InitializeComponent();

            this.Content = name;
            this.ListView = listview;

            frame = new Frame(settings.FrameWidth, settings.FrameHeight);

            ListView.SelectedItem = this;


        }
        public FrameButton(ListView listview, string name, string path)
        {
            InitializeComponent();

            this.Content = name;
            this.ListView = listview;

            frame = Frame.LoadFrameFrom(path);

            ListView.SelectedItem = this;



        }
    }
}
