using CECS622_Project1.Classes;
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

namespace CECS622_Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<StatusItem> log;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Facility f = new Facility();
            f.RunSimulation();

            string info = "";

            info += "Utilization S1    : " + f.Server1Utilization + "\r\n";
            info += "Utilization S2    : " + f.Server2Utilization + "\r\n";
            info += "Max Queue S1      : " + f.MaxLengthQueue1 + "\r\n";
            info += "Max Queue S2      : " + f.MaxLengthQueue2 + "\r\n";
            info += "Average Wait S1   : " + f.AverageWaitQueue1 + "\r\n";
            info += "Average Wait S2   : " + f.AverageWaitQueue2 + "\r\n";
            info += "Number Departures : " + f.NumberDepartures + "\r\n";

            statsTextBox.Text = info;

            FELLogListView.ItemsSource = f.Log;
        }
    }
}
