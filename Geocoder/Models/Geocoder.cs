using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Geocoding;
using Geocoding.Microsoft;

namespace Geocoder.Models
{
	class Geocoder
	{
		IGeocoder _geocoder = new BingMapsGeocoder("AtqMAUT-Eij5g-md0f59bgwwjovGn3c53GqAqd1cfY27XJmiNaU4tBIdIK4hSnTZ");

		public async Task<IEnumerable<Address>> GeocodeAsync(string address)
		{
			return await _geocoder.GeocodeAsync(address);
		}

		public async Task<IEnumerable<Address>> ReverseGeocodeAsync()
		{
			return await _geocoder.ReverseGeocodeAsync(0, 0);
		}
	}
}
