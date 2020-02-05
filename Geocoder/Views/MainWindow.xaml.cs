using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Geocoding;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace Geocoder.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly List<Pushpin> _pins = new List<Pushpin>();

		public MainWindow()
		{
			InitializeComponent();
			DisplayedMap.Focus();
		}

		private void AddPins(IEnumerable<Address> addresses)
		{
			foreach (var address in addresses)
			{
				var pin = new Pushpin { Location = new Location(address.Coordinates.Latitude, address.Coordinates.Longitude) };
				_pins.Add(pin);
				DisplayedMap.Children.Add(pin);
			}

			try
			{
				DisplayedMap.Center = _pins.First().Location;
				DisplayedMap.ZoomLevel = 4;
			}
			catch
			{
				// ignored
			}
		}

		private void RemovePins()
		{
			foreach (var pin in _pins)
			{
				DisplayedMap.Children.Remove(pin);
			}
			_pins.Clear();
		}

		private async void GeocodeOnEnterKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter || !GeocoderViewModel.CanGeocode)
			{
				return;
			}

			RemovePins();
			var addresses = await GeocoderViewModel.Geocode();
			if (addresses == null)
			{
				ShowErrorDialog();
				return;
			}
			AddPins(addresses);
		}

		private void ShowErrorDialog()
		{
			var message = $"Sorry, we couldn't understand your position: {GeocoderViewModel.Location}. "
			              + "Consider using space or semicolon as separators";
			MessageBox.Show(message, "Reverse geocoding failed");
		}

		private async void ReverseGeocodeOnDoubleClick(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;

			RemovePins();

			if (GeocoderViewModel.AddressToPos)
			{
				GeocoderViewModel.AddressToPos = false;
				GeocodingMode.InverseContent();
			}

			var mousePosition = e.GetPosition(this);
			var pinLocation = DisplayedMap.ViewportPointToLocation(mousePosition);

			var newPin = new Pushpin {Location = pinLocation};
			_pins.Add(newPin);
			DisplayedMap.Children.Add(newPin);

			GeocoderViewModel.Location = $"{pinLocation.Latitude} {pinLocation.Longitude}";

			await GeocoderViewModel.Geocode();
		}

		private void ChangeGeocodingModeOnClick(object sender, RoutedEventArgs e)
		{
			GeocoderViewModel.AddressToPos = !GeocoderViewModel.AddressToPos;
		}

		private async void GeocodeOnClick(object sender, RoutedEventArgs e)
		{
			RemovePins();
			var addresses = await GeocoderViewModel.Geocode();
			if (addresses == null)
			{
				ShowErrorDialog();
				return;
			}
			AddPins(addresses);
		}
	}
}
