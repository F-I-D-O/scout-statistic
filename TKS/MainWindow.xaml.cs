using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TKS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Programe Program { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Program = new Programe();
            this.WindowState = WindowState.Maximized;
        }

        private void ButtonLoadDataClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "CSV soubor s daty ze skautisu |*.csv";
                if (fileDialog.ShowDialog() == true)
                {
                    Program.LoadData(fileDialog.FileName);
                    dataGridData.DataContext = Program.LoadedRows;
                    dataGridRecords.DataContext = Program.Records;
                }
                
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Přístup k souboru byl odepřen. Možná umístění neexistuje.");
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonComputeClick(object sender, RoutedEventArgs e)
        {
            graph.DataContext = Program.Graph.GraphModel;
            Program.compute(Convert.ToInt32(TestDataPercent.Text), float.Parse(Step.Text, System.Globalization.CultureInfo.InvariantCulture),
                float.Parse(StopCondition.Text, System.Globalization.CultureInfo.InvariantCulture));
        }

    }
}
