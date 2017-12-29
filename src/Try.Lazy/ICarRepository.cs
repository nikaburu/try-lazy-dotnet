using System;
using System.Collections.Generic;
using Try.Lazy.Domain;

namespace Try.Lazy
{
	public interface ICarRepository
	{
		/// <summary>
		///     Retrieves available cars.
		/// </summary>
		/// <returns>List of lower invariant car name and car id</returns>
		List<(string color, Guid carId)> GetAvailableCars();

		/// <summary>
		///     Returns car by car id.
		/// </summary>
		/// <param name="carId">Car id</param>
		/// <returns></returns>
		Lazy<Car> Get(Guid carId);
	}
}