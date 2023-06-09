﻿using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfAppCronometro
{
    /// <summary>
    /// Saves the exact value of the Clock
    /// </summary>
    public class Clock
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ClockController _clockController;
        public MainWindow()
        {
            InitializeComponent();
            //_clockView = clockView;
            //_clockView = new ClockView(this);
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
            labelHours.Content = "00";
            labelMinutes.Content = "00";
            labelSeconds.Content = "00";
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            _clockController = new ClockController(new ClockView(), new Clock());
        }
    }
    /// <summary>
    /// ClockView Interface 
    /// </summary>
    public interface IClockView
    {
        string LabelHours { get; set; }
        string LabelMinutes { get; set; }
        string LabelSeconds { get; set; }
    }
    /// <summary>
    /// Save all window labels 
    /// </summary>
    public class ClockView : IClockView
    {
        public string LabelHours
        {
            get { return ((MainWindow)Application.Current.MainWindow).labelHours.Content.ToString(); ; }
            set { ((MainWindow)Application.Current.MainWindow).labelHours.Content = value; }
        }
        public string LabelMinutes
        {
            get { return ((MainWindow)Application.Current.MainWindow).labelMinutes.Content.ToString(); ; }
            set { ((MainWindow)Application.Current.MainWindow).labelMinutes.Content = value; }
        }
        public string LabelSeconds
        {
            get { return ((MainWindow)Application.Current.MainWindow).labelSeconds.Content.ToString(); ; }
            set { ((MainWindow)Application.Current.MainWindow).labelSeconds.Content = value; }
        }
    }
    /// <summary>
    /// ClockController Interface 
    /// </summary>
    public interface IClockController
    {
        void Start();
        void Pause();
        void Stop();
    }
    /// <summary>
    /// Manages timer ticks 
    /// </summary>
    public class ClockController : IClockController
    {
        private readonly Clock _clock;
        private readonly IClockView _clockView;
        DispatcherTimer timer = new DispatcherTimer();

        public ClockController(IClockView clockView, Clock clock)
        {
            _clock = clock;
            _clockView = clockView;
            _clockView.LabelHours = "00";
            _clockView.LabelMinutes = "00";
            _clockView.LabelSeconds = "00";
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
            
            _clockView.LabelHours = _clock.Hours.ToString("00");
            _clockView.LabelMinutes = _clock.Minutes.ToString("00");
            _clockView.LabelSeconds = _clock.Seconds.ToString("00");
        }
    }
}
