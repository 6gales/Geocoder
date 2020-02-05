using System.Collections.Generic;
using System.Threading.Tasks;
using Geocoding;
using Geocoding.Microsoft;

namespace Geocoder.Models
{
	class BingGeocoder : IGeocoder
	{
		private readonly Geocoding.IGeocoder _geocoder = new BingMapsGeocoder("AtqMAUT-Eij5g-md0f59bgwwjovGn3c53GqAqd1cfY27XJmiNaU4tBIdIK4hSnTZ");
		private readonly List<string> _addressHistory = new List<string>();
		private readonly List<Location> _locationHistory = new List<Location>();

		public async Task<IEnumerable<Address>> GeocodeAsync(string address)
		{
			_addressHistory.Add(address);
			return await _geocoder.GeocodeAsync(address);
		}

		public async Task<IEnumerable<Address>> ReverseGeocodeAsync(Location location)
		{
			_locationHistory.Add(location);
			return await _geocoder.ReverseGeocodeAsync(location);
		}

		public IEnumerable<string> AddressHistory => _addressHistory;

		public IEnumerable<Location> LocationHistory => _locationHistory;
	}
}
