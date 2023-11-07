using ModernDesign.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
		public HomeViewModel HomeVM { get; set; }
        public DogsViewModel DogsVM { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DogsViewCommand { get; set; }


        private object _currentView;

		public object CurrentView
		{
			get { return _currentView; }
			set 
			{ 
				_currentView = value;
				OnPropertyChanged();
			}
		}

        public MainViewModel()
        {
			HomeVM = new HomeViewModel();
            DogsVM = new DogsViewModel();	

			CurrentView = HomeVM;

			HomeViewCommand = new RelayCommand(o =>
			{
				CurrentView = HomeVM;
			});
            DogsViewCommand = new RelayCommand(o =>
			{
				CurrentView = DogsVM;
			});
        }

    }
}
