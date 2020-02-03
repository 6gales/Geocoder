using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Geocoder.ViewModels.Commands
{
	class GeocodeCommand : ICommand
	{

		public GeocodeCommand(Func<bool> isAddrToPos)
		{

		}

		public bool CanExecute(object parameter)
		{
			return false;
		}

		public void Execute(object parameter)
		{
			throw new NotImplementedException();
		}

		public event EventHandler CanExecuteChanged;
	}
}
