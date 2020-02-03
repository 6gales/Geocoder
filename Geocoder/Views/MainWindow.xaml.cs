using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;

namespace Geocoder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool _addressToPos = true;
		private readonly List<Pushpin> _pins = new List<Pushpin>();

		public MainWindow()
		{
			InitializeComponent();
			DisplayedMap.Focus();
		}

		private void ReverseGeocodeOnDoubleClick(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;

			foreach (var pin in _pins)
			{
				DisplayedMap.Children.Remove(pin);
			}
			_pins.Clear();

			if (_addressToPos)
			{
				_addressToPos = false;
				GeocodingMode.InverseContent();
			}

			var mousePosition = e.GetPosition(this);
			var pinLocation = DisplayedMap.ViewportPointToLocation(mousePosition);

			var newPin = new Pushpin {Location = pinLocation};
			_pins.Add(newPin);
			DisplayedMap.Children.Add(newPin);

			GeocoderViewModel.Location = "0 0"; //pinLocation.ToString();
		}

		private void ChangeGeocodingModeOnClick(object sender, RoutedEventArgs e)
		{
			_addressToPos = !_addressToPos;
		}

		private async void GeocodeOnClick(object sender, RoutedEventArgs e)
		{

		}

	}
}
