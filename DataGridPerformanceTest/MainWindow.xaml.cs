using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace DataGridPerformanceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusinessDataObject BusinessData = new BusinessDataObject();
        ViewModel ViewModel = new ViewModel();
        DispatcherTimer _timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            TxtLog.Document.Blocks.Clear();
            _timer.Interval = TimeSpan.FromMilliseconds(250);
            _timer.Tick += (o, e) =>
            {
                _timer.Stop();
                Stopwatch stopwatch = Stopwatch.StartNew();
                ViewModel.FilterBusinessDataView(TxtFilter.Text);
                
                stopwatch.Stop();
                TxtLog.AppendText($"DG filter: {stopwatch.Elapsed.TotalMilliseconds}ms\r");
            };

            this.DataContext = ViewModel;
            ViewModel.UpdateViewModel(BusinessData.UsefulDataList);
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            _timer.Stop();
            _timer.Start();
        }
    }
}
