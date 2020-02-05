using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Geocoder.Models;
using Geocoding;

namespace Geocoder.ViewModels
{
	class GeocoderViewModel : INotifyPropertyChanged
	{
		private readonly Models.IGeocoder _geocoder = new BingGeocoder();
		private string _address;
		private string _location;
		private string _actualInput;
		private bool _addressToPos = true;
		
		public string Address
		{
			get => _address;
			set
			{
				_address = value;
				if (AddressToPos)
				{
					_actualInput = _address;
					OnPropertyChanged(nameof(CanGeocode));
				}

				OnPropertyChanged(nameof(Address));
			}
		}

		public string Location
		{
			get => _location;
			set
			{
				_location = value;
				if (!AddressToPos)
				{
					_actualInput = _location;
					OnPropertyChanged(nameof(CanGeocode));
				}
				OnPropertyChanged(nameof(Location));
			}
		}

		public bool AddressToPos
		{
			get => _addressToPos;
			set
			{
				_addressToPos = value;
				_actualInput = (value ? _address : _location);
				OnPropertyChanged(nameof(CanGeocode));
			}
		}

		public bool CanGeocode => !string.IsNullOrEmpty(_actualInput);
		
		public IEnumerable<string> AddressHistory => _geocoder.AddressHistory;
		public IEnumerable<Location> LocationHistory => _geocoder.LocationHistory;

		private async Task<IEnumerable<Address>> ForwardGeocode()
		{
			var addresses = (await _geocoder.GeocodeAsync(_address)).ToList();
			var first = addresses.FirstOrDefault();
			if (first == null)
			{
				Location = "";
				return addresses;
			}

			Location = $"{first.Coordinates.Latitude} {first.Coordinates.Longitude}";
			return addresses;
		}
		private async Task<IEnumerable<Address>> ReverseGeocode()
		{
			var values = _location.Split(' ', ';');
			if (values.Length < 2 || !double.TryParse(values[0], out var lat) || !double.TryParse(values[1], out var lng))
			{
				return null;
			}
			
			var addresses = (await _geocoder.ReverseGeocodeAsync(new Location(lat, lng))).ToList();
			var first = addresses.FirstOrDefault();
			if (first == null)
			{
				Address = "";
				return addresses;
			}

			Address = first.FormattedAddress;
			return addresses;
		}

		public async Task<IEnumerable<Address>> Geocode()
		{
			if (AddressToPos)
			{
				return await ForwardGeocode();
			}

			return await ReverseGeocode();
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		public void TriggerCanGeocodeChanged()
		{
			OnPropertyChanged(nameof(CanGeocode));
		}
	}
}
