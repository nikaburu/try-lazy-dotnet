using System.Threading;

namespace Try.Lazy.Console.Extensions
{
	internal static class ProgressBar
	{
		public static bool IsInProgress
		{
			get => _isInProgress;
			set
			{
				lock (Mutex)
				{
					_isInProgress = value;
				}
			}
		}

		public static void Start()
		{
			IsInProgress = true;

			//GC aware
			_thread = new Thread(() =>
			{
				while (_isInProgress)
				{
					_sign = _sign == '|' ? '-' : '|';
					System.Console.Write("\r{0}", _sign);
					Thread.Sleep(100);
				}

				ClearCurrentConsoleLine();
			});

			_thread.Start();
		}

		public static void Stop()
		{
			IsInProgress = false;
		}

		#region Private Helpers

		public static void ClearCurrentConsoleLine()
		{
			var currentLineCursor = System.Console.CursorTop;
			System.Console.SetCursorPosition(0, System.Console.CursorTop);
			System.Console.Write(new string(' ', System.Console.WindowWidth));
			System.Console.SetCursorPosition(0, currentLineCursor);
		}

		#endregion

		#region Fields

		private static bool _isInProgress;
		private static char _sign = '|';
		private static readonly object Mutex = new object();
		private static Thread _thread;

		#endregion
	}
}