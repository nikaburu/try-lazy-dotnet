using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Try.Lazy.Domain;

namespace Try.Lazy.Dal
{
	public class CarRepository : ICarRepository
	{
		public static ICarRepository Instance => _instance.Value;

		public List<(string color, Guid carId)> GetAvailableCars()
		{
			return _database.Cars.Select(car => (car.Color.ToLowerInvariant(), car.CarId)).ToList();
		}

		public Lazy<Car> Get(Guid carId)
		{
			var creationTreadId = Thread.CurrentThread.ManagedThreadId;
			return new Lazy<Car>(() =>
			{
				if (creationTreadId != Thread.CurrentThread.ManagedThreadId)
					throw new Exception("Value is called from another thread!");

				return _database.Cars.FirstOrDefault(car => car.CarId == carId);
			}, LazyThreadSafetyMode.PublicationOnly); // LazyThreadSafetyMode.PublicationOnly - says do not cache exception from another thread.
		}

		#region Fields

		private readonly Database _database;

		// Lazy<T> helps to make truly thread safe and lazy loaded implementation of Singleton pattern.
		// And we don't need empty static constructor because of http://csharpindepth.com/Articles/General/Beforefieldinit.aspx
		private static readonly Lazy<ICarRepository> _instance =
			new Lazy<ICarRepository>(() => new CarRepository(new Database()));

		private CarRepository(Database database)
		{
			_database = database;
		}

		#endregion
	}
}