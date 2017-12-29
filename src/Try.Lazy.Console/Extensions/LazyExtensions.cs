using System;

namespace Try.Lazy.Console.Extensions
{
	public static class LazyExtensions
	{
		public static T GetValueProgressBarAware<T>(this Lazy<T> lazyObject)
		{
			if (!lazyObject.IsValueCreated)
				ProgressBar.Start();

			var actualObject = lazyObject.Value;
			ProgressBar.Stop();

			return actualObject;
		}
	}
}