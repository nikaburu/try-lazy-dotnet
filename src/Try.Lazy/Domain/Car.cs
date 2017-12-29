using System;

namespace Try.Lazy.Domain
{
	public sealed class Car
	{
		public Guid CarId { get; set; }

		public string Color { get; set; }
		public int Mileage { get; set; }
		public string Owner { get; set; }
		public int? Position { get; set; }

		public Lazy<Brand> Brand { get; set; }
	}
}