using System;
using System.Text;
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
using System.Windows.Threading;
using TwinCAT.Ads;
using TwinCAT.TypeSystem;
using System.Reactive.Linq;
using System;
using System.Net.Sockets;
using System.Diagnostics.Metrics;

namespace WpfADS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private TwinCAT.Ads.AdsClient tcClient;

        public MainWindow()
          
        {
            
            InitializeComponent();
            
            Console.WriteLine("Start Appli");
            LabelHorodatage.Content = "Start Appli";
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Start();

            tcClient = new TwinCAT.Ads.AdsClient();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Main();
        }

        public void Main()
        {
            Console.WriteLine("Start Loop");
            LabelHorodatage.Content = DateTime.Now.ToLongTimeString();
            LectureADS();
        }
        public void LectureADS()
        {
            uint hVar = 0;
            
            Console.WriteLine("LectureADS ADS");
            tcClient.Connect("10.68.0.35.2.1", 1009);    //AmsNetId.LocalHost
           if  (tcClient.IsConnected==true)
            {
                MessageBox.Show("Connected");
            }
            try
            {

                /*               hVar = tcClient.CreateVariableHandle("MAIN.nCounter");
                               LabelnCounterValue.Content = (int)tcClient.ReadAny(hVar, typeof(int));

                               hVar = tcClient.CreateVariableHandle("MAIN.sString1");
                               LabelsString1Value.Content = tcClient.ReadAny(hVar, typeof(String), new int[] { 80 }).ToString();

                               hVar = tcClient.CreateVariableHandle("MAIN.bCheckBox");
                               cbbCheckbox.IsChecked = (bool)tcClient.ReadAny(hVar, typeof(bool));

                               //lrealVal
                               hVar = tcClient.CreateVariableHandle("MAIN.lrealVal");
                               LabelsFloat11Value.Content = tcClient.ReadAny(hVar, typeof(double));*/

                LabelnCounterValue.Content=tcClient.ReadAny(0xF32, 0x80000015, typeof(UInt32));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                // Unregister VarHandle after Use
                tcClient.DeleteVariableHandle(hVar);
            }
        }

        private void btnnCounterValue(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(tbxnCounterPreparedValue.Text, out double numericValue))
            {
                tcClient.WriteValue("MAIN.nCounter", numericValue);
                tbxnCounterPreparedValue.Foreground = new SolidColorBrush(Colors.Black);
                tbxnCounterPreparedValue.FontWeight = FontWeights.Regular;
            }
            else 
            {
                tbxnCounterPreparedValue.Foreground = new SolidColorBrush(Colors.Red);
                tbxnCounterPreparedValue.FontWeight = FontWeights.Bold;
            }
            LabelHorodatage.Content = DateTime.Now.ToLongTimeString();
        }

        private void btnsSting1Value(object sender, RoutedEventArgs e)
        {
            tcClient.WriteValue("MAIN.sString1", tbxsString1PreparedValue.Text);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            uint hVar = 0;
            hVar = tcClient.CreateVariableHandle("MAIN.bCheckBox");
            if  ( cbbCheckbox.IsChecked ==true)
            { 
                tcClient.WriteAny(hVar, true); 
            }
            else
            { 
                tcClient.WriteAny(hVar, false);
            }
            tcClient.DeleteVariableHandle(hVar);
        }

        private void btnFloatValue_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(tbxsFloat1PreparedValue.Text, out double numericValue))
            {
                tcClient.WriteValue("MAIN.lrealVal", numericValue);
                tbxsFloat1PreparedValue.Foreground = new SolidColorBrush(Colors.Black);
                tbxsFloat1PreparedValue.FontWeight = FontWeights.Regular;
            }
            else
            {
                tbxsFloat1PreparedValue.Foreground = new SolidColorBrush(Colors.Red);
                tbxsFloat1PreparedValue.FontWeight = FontWeights.Bold;
            }
            LabelHorodatage.Content = DateTime.Now.ToLongTimeString();
        }

        private void WindowsClose(object sender, EventArgs e)
        {
            tcClient.Close();
        }
    }
}