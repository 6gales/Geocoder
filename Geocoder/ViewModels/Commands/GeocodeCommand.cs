using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Geocoder.ViewModels.Commands
{
	class GeocodeCommand : ICommand
	{
		public bool CanExecute(object parameter)
		{
			throw new NotImplementedException();
		}

		public void Execute(object parameter)
		{
			throw new NotImplementedException();
		}

		public event EventHandler CanExecuteChanged;
	}
}
