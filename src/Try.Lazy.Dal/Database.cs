using System;
using System.Collections.Generic;
using System.Threading;
using Try.Lazy.Domain;

namespace Try.Lazy.Dal
{
	internal sealed class Database
	{
		public IEnumerable<Car> Cars
		{
			get
			{
				Thread.Sleep(1500);
				return _cars;
			}
		}

		private IEnumerable<Car> Seed()
		{
			return new List<Car>
			{
				new Car
				{
					CarId = Guid.NewGuid(),
					Mileage = 14323,
					Color = "Red",
					Owner = "Iva Grue",
					Brand = new Lazy<Brand>(() =>
					{
						Thread.Sleep(1500);
						return new Brand
						{
							Name = "BMV",
							Description =
								"Typically, BMW introduces many of their innovations first in the 7 Series, such as the somewhat controversial iDrive system."
						};
					})
				},
				new Car
				{
					CarId = Guid.NewGuid(),
					Mileage = 5433,
					Color = "Green",
					Owner = "Sean O'Connel",
					Brand = new Lazy<Brand>(() =>
					{
						Thread.Sleep(1500);
						return new Brand
						{
							Name = "Toyota",
							Description =
								"Toyota established the Toyota Technological Institute in 1981, as Sakichi Toyoda had planned to establish a university as soon as he and Toyota became successful."
						};
					})
				},
				new Car
				{
					CarId = Guid.NewGuid(),
					Mileage = 565,
					Color = "Brown",
					Owner = "John Bore"
				}
			};
		}

		#region Fields

		private readonly IEnumerable<Car> _cars;

		public Database()
		{
			_cars = Seed();
		}

		#endregion
	}
}