using System;
using System.Linq;
using System.Threading;
using Try.Lazy.Console.Extensions;
using Try.Lazy.Domain;

namespace Try.Lazy.Console
{
	public class Application
	{
		public void Run()
		{
			ApplicationConsole.HelloMessage();

			while (true)
			{
				var availableCars = _carRepository.GetAvailableCars();
				var input =
					ApplicationConsole.RequestCarSelection(availableCars.Select(x => x.color).ToArray());
				if (!input.isContinue)
				{
					ApplicationConsole.ByeMessage();
					break;
				}

				var selectedCarId = availableCars.FirstOrDefault(x => x.color == input.color).carId;
				if (selectedCarId == default(Guid))
				{
					ApplicationConsole.CarDoesNotExist();
					continue;
				}

				var lazyCar = _carRepository.Get(selectedCarId);
				if (lazyCar == null)
				{
					ApplicationConsole.CarDoesNotExist();
				}
				else
				{
					RequestCarCurrentPositionFromAnotherThread(lazyCar);

					//Does not throw exception because of LazyThreadSafetyMode.PublicationOnly was set for Lazy<CarInformation> in CarsFinder.
					var car = lazyCar.GetValueProgressBarAware();
					ApplicationConsole.Print(car);

					if (car.Brand != null && ApplicationConsole.RequestIfToLoadBrand())
						ApplicationConsole.Print(car.Brand.GetValueProgressBarAware());
				}
			}
		}

		private void RequestCarCurrentPositionFromAnotherThread(Lazy<Car> lazyCar)
		{
			var thread = new Thread(() =>
			{
				Thread.Sleep(300);

				try
				{
					lazyCar.Value.Position = _random.Next(0, 100);
				}
				catch
				{
					//Exception from car value
				}
			});

			thread.Start();

			while (thread.IsAlive) Thread.Sleep(10);
		}

		#region Fields

		private readonly ICarRepository _carRepository;
		private readonly Random _random;

		public Application(ICarRepository carRepository)
		{
			_carRepository = carRepository;
			_random = new Random();
		}

		#endregion
	}
}