﻿using AsClass;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AS;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
     
   

    public MainWindow()
    {
        InitializeComponent();

        AS_Main Sigma = new AsClass.AS_Main(image);

        /*ControlTabFile.TabContent.Content = "File";

        foreach (Button button in As.FileButtons)
        {
            ControlTabFile.StackPanelButtons.Children.Add(button);
        }
        */
    }

    private void ButtonExita_Click(object sender, RoutedEventArgs e)
    {
       this.Close();
    }
}