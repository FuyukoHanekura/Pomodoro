using System;
using System.Windows;
using System.Windows.Threading;


namespace Pomodoro
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const double TIMER_START = 25;
		private DispatcherTimer timer;
		private TimeSpan timeLeft;
		private bool isRunning;

		public MainWindow()
		{
			InitializeComponent();

			timeLeft = TimeSpan.FromMinutes(TIMER_START);

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += Timer_Tick;

			UpdateTimerDisplay();
		}

		private void UpdateTimerDisplay()
		{
			TimerLabel.Content = timeLeft.ToString(@"mm\:ss");
		}

		private void Timer_Tick(object? sender, EventArgs e)
		{
			if (timeLeft.TotalSeconds > 0)
			{
				timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
				UpdateTimerDisplay();
			}
			else
			{
				timer.Stop();
				isRunning = false;
				MessageBox.Show("Pomodoro Complete!");
			}
		}

		private void OnStartBtn(object? sender, EventArgs e)
		{
			if (!isRunning)
			{
				timer.Start();
				isRunning = true;
			}
		}

		private void OnPauseBtn(object? sender, EventArgs e)
		{
			if (isRunning)
			{
				timer.Stop();
				isRunning = false;
				StartBtn.Content = "Resume";
			}
		}

		private void OnResetBtn(object? sender, EventArgs e)
		{
			if (!isRunning)
			{
				timer.Stop();
				isRunning = false;
				timeLeft = TimeSpan.FromMinutes(TIMER_START);
				UpdateTimerDisplay();
				StartBtn.Content = "Start";
			}
		}

	}
}