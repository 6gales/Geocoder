using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Geocoding;
using Geocoding.Microsoft;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace Geocoder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool _addressToPos = true;
		private Pushpin _pin;

		public MainWindow()
		{
			InitializeComponent();
			DisplayedMap.Focus();
		}

		private void AddPushpinOnDoubleClick(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;

			Point mousePosition = e.GetPosition(this);
			Location pinLocation = DisplayedMap.ViewportPointToLocation(mousePosition);

			_pin = new Pushpin {Location = pinLocation};

			DisplayedMap.Children.Add(_pin);
		}

		private async void ChangeGeocodingModeOnClick(object sender, RoutedEventArgs e)
		{
			_addressToPos = !_addressToPos;
			ViewModel.Add("afaaf");
		}

		private async void GeocodeOnClick(object sender, RoutedEventArgs e)
		{

		}
    }
}
