using Try.Lazy.Dal;

namespace Try.Lazy.Console
{
	internal class Program
	{
		private static void Main()
		{
			var app = new Application(CarRepository.Instance);
			app.Run();
		}
	}
}