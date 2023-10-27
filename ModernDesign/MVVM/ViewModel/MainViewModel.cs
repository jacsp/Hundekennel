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
        public MoviesViewModel MoviesVM { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand MoviesViewCommand { get; set; }


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
			MoviesVM = new MoviesViewModel();	

			CurrentView = HomeVM;

			HomeViewCommand = new RelayCommand(o =>
			{
				CurrentView = HomeVM;
			});
			MoviesViewCommand = new RelayCommand(o =>
			{
				CurrentView = MoviesVM;
			});
        }

    }
}
