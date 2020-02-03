using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Geocoding;

namespace Geocoder.Models
{
	interface IGeocoder
	{
		Task<IEnumerable<Address>> GeocodeAsync(string address);
		Task<IEnumerable<Address>> ReverseGeocodeAsync(Location location);
		IEnumerable<string> AddressHistory { get; }
		IEnumerable<Location> LocationHistory { get; }
	}
}
