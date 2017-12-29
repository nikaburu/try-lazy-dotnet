using System.Linq;
using System.Threading;
using Try.Lazy.Domain;

namespace Try.Lazy.Console
{
	internal sealed class ApplicationConsole
	{
		public static void HelloMessage()
		{
			System.Console.WriteLine("You're staying at car parking.");
			Wait();
		}

		public static (bool isContinue, string color) RequestCarSelection(string[] carNames)
		{
			System.Console.Write("You can see {0} cars around.\r\nWhich one did interested you? (type quit to leave): ",
				carNames.Aggregate((sum, item) => sum + ", " + item));

			var input = System.Console.ReadLine();
			EmptyLine();

			return (input?.ToLowerInvariant() != "quit", input);
		}

		public static void ByeMessage()
		{
			System.Console.WriteLine("You're leaving car parking..");
			Wait();
		}

		public static bool RequestIfToLoadBrand()
		{
			System.Console.Write("Would you like to know more? (yes/no): ");

			var input = System.Console.ReadLine();
			EmptyLine();

			return input?.ToLowerInvariant() == "yes";
		}

		public static void Print(Car car)
		{
			System.Console.WriteLine(
				$"Color: {car.Color}\r\nMileage: {car.Mileage}\r\n" +
				$"Owner: {car.Owner}\r\nPosition: {car.Position?.ToString() ?? "Currently unavailable"}");
			EmptyLine();

			Wait();
		}

		public static void Print(Brand brand)
		{
			System.Console.WriteLine($"Brand: {brand.Name}\r\n{brand.Description}");
			EmptyLine();

			Wait();
		}

		public static void CarDoesNotExist()
		{
			System.Console.WriteLine("Sorry, the car you selected is not in front of you.");
			EmptyLine();

			Wait();
		}

		#region Private helpers

		private static void Wait()
		{
			Thread.Sleep(500);
		}

		private static void EmptyLine()
		{
			System.Console.WriteLine();
		}

		#endregion
	}
}