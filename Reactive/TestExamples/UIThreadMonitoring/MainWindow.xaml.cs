using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            MonitorUIThread();
        }

        private void MonitorUIThread()
        {
            Task.Factory.StartNew(() =>
            {
                bool messageIsShown = false;
                while (true)
                {
                    Thread.Sleep(1000);
                    if (UIThreadIsHanging())
                    {
                        if (!messageIsShown)
                        {
                            MessageBox.Show("UI thread has been frozen");
                        }
                    }
                    else
                    {
                        messageIsShown = false;
                    }
                }
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public bool UIThreadIsHanging()
        {
            var mre = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(_ => {
                Dispatcher.Invoke(() => { });
                mre.Set();
            });

            return !mre.WaitOne(TimeSpan.FromSeconds(2));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(5000);
        }
    }
}
