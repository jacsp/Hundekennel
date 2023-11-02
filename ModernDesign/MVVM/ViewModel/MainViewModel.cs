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
        public DogsViewModel MoviesVM { get; set; }
        public PartnerMatchViewModel PartnerMatchVM { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand MoviesViewCommand { get; set; }
        public RelayCommand PartnerMatchCommand { get; set; }


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
			MoviesVM = new DogsViewModel();
			PartnerMatchVM = new PartnerMatchViewModel();

			CurrentView = HomeVM;

			HomeViewCommand = new RelayCommand(o =>
			{
				CurrentView = HomeVM;
			});
			MoviesViewCommand = new RelayCommand(o =>
			{
				CurrentView = MoviesVM;
			});
            PartnerMatchCommand = new RelayCommand(o =>
            {
                CurrentView = PartnerMatchVM;
            });
        }

    }
}
