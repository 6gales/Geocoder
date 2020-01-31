using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Geocoder.ViewModels
{
	class GeocoderViewModel : INotifyPropertyChanged
	{
		private List<string> _history = new List<string>();

		public IEnumerable<string> AddressHistory => _history;
		public string UserInput { get; set; }

		public void Add(string a)
		{
			_history.Add(a);
			OnPropertyChanged($"History");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
