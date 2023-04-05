using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.Windows.Threading;

namespace WpfAppCronometro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clock myClock;
        private readonly IClockController _clockController;
        public MainWindow(IClockController clockController)
        {
            InitializeComponent();
            myClock = new Clock(this);
            _clockController = clockController;
        }
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            _clockController.Start();
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            _clockController.Pause();
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            _clockController.Stop();
            myClock.LabelHours = "00";
            myClock.LabelMinutes = "00";
            myClock.LabelSeconds = "00";
        }
    }
    public interface IClock
    {
        string LabelHours { get; set; }
        string LabelMinutes { get; set; }
        string LabelSeconds { get; set; }
    }
    public class Clock
    {
        private MainWindow _mainWindow;
        public Clock(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        public string LabelHours
        {
            get { return _mainWindow.labelHours.Content.ToString(); ; }
            set { _mainWindow.labelHours.Content = value; }
        }
        public string LabelMinutes
        {
            get { return _mainWindow.labelMinutes.Content.ToString(); ; }
            set { _mainWindow.labelMinutes.Content = value; }
        }
        public string LabelSeconds
        {
            get { return _mainWindow.labelSeconds.Content.ToString(); ; }
            set { _mainWindow.labelSeconds.Content = value; }
        }
    }
    public interface IClockController
    {
        void Start();
        void Pause();
        void Stop();
    }
    public class ClockController : IClockController
    {
        DispatcherTimer timer = new DispatcherTimer();

        public ClockController()
        {
            _clock = clock;
            _clock.Hours = 0;
            _clock.Minutes = 0;
            _clock.Seconds = 0;    
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += GetTimerTicks;
        }
        public void Start()
        {
            timer.Start();
        }
        public void Pause()
        {
            timer.Stop();
        }
        public void Stop()
        {
            timer.Stop();
            _clock.Hours = 0;
            _clock.Minutes = 0;
            _clock.Seconds = 0;
        }
        public void GetTimerTicks(object sender, EventArgs e)
        {
            _clock.Seconds++;

            if (_clock.Seconds == 60)
            {
                _clock.Seconds = 0;
                _clock.Minutes++;
            }

            if (_clock.Minutes == 60)
            {
                _clock.Minutes = 0;
                _clock.Hours++;
            }
            LabelHours _clock.Hours.ToString("00");
            //labelMinutes.Content = _clock.Minutes.ToString("00");
            //labelSeconds.Content = _clock.Seconds.ToString("00");
        }
    }
}
