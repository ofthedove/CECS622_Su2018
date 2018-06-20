using CECS622_Project1.Classes;
using Microsoft.Win32;
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
using System.IO;

namespace CECS622_Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<StatusItem> log;
        Facility f;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            f = new Facility();
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

            felLogListView.ItemsSource = f.Log;

            saveButton.IsEnabled = true;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Data
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV File|*.csv";
            saveDialog.Title = "Save to CSV File";
            saveDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveDialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                FileStream fs = (System.IO.FileStream)saveDialog.OpenFile();

                AddText(fs, "CECS 622 Project 1\r\n");
                AddText(fs, "Andrew Combs\r\n");
                AddText(fs, "Result of Simulation Run\r\n");
                AddText(fs, "\r\n");
                AddText(fs, "\r\n");
                AddText(fs, "Utilization S1    : " + f.Server1Utilization + "\r\n");
                AddText(fs, "Utilization S2    : " + f.Server2Utilization + "\r\n");
                AddText(fs, "Max Queue S1      : " + f.MaxLengthQueue1 + "\r\n");
                AddText(fs, "Max Queue S2      : " + f.MaxLengthQueue2 + "\r\n");
                AddText(fs, "Average Wait S1   : " + f.AverageWaitQueue1 + "\r\n");
                AddText(fs, "Average Wait S2   : " + f.AverageWaitQueue2 + "\r\n");
                AddText(fs, "Number Departures : " + f.NumberDepartures + "\r\n");
                AddText(fs, "\r\n");
                AddText(fs, "\r\n");
                
                AddText(fs, "  Clock    " + "\t" + " Q1 " + "\t" + "  S1  " + "\t" + " Q2 " + "\t" + "  S2  " + "\t" + "FEL - " + "\r\n");

                foreach (StatusItem item in f.Log)
                {
                    AddText(fs, item.ClockTime.ToString("0.0000").PadLeft(10) + "\t");
                    AddText(fs, " " + item.Queue1Status.ToString().PadLeft(2) + " " + "\t");
                    AddText(fs, item.Server1Status.ToString().PadRight(5) + " " + "\t");
                    AddText(fs, " " + item.Queue2Status.ToString().PadLeft(2) + " " + "\t");
                    AddText(fs, item.Server2Status.ToString().PadRight(5) + " " + "\t");
                    AddText(fs, item.FELContents + "\r\n");
                }

                fs.Close();
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
