﻿using System;
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
