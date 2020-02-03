using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Geocoder.Models;
using Geocoder.ViewModels.Commands;
using Geocoding;

namespace Geocoder.ViewModels
{
	class GeocoderViewModel : INotifyPropertyChanged
	{
		private readonly Models.IGeocoder _geocoder = new BingGeocoder();

		public string Address { get; set; }
		public string Location { get; set; }

		public IEnumerable<string> AddressHistory => _geocoder.AddressHistory;
		public IEnumerable<Location> LocationHistory => _geocoder.LocationHistory;

		public GeocodeCommand GeocodeCommand { get; } = new GeocodeCommand(() => true);


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
