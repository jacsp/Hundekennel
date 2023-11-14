using ModernDesign.Core;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.ViewModel
{
    class DogsViewModel : ObservableObject
    {
        private Dog selectedDog;
        public DogsRepository dogsRepository;
        private ObservableCollection<Dog> dogs;

        public ObservableCollection<Dog> Dogs
        {
            get { return dogs; }
            set 
            {
                dogs = value;
                OnPropertyChanged(nameof(dogs));
            }
        }

        public Dog SelectedDog
        {
            get { return selectedDog; }
            set
            {
                selectedDog = value;
                OnPropertyChanged(nameof(SelectedDog));
            }
        }

        public DogsViewModel()
        {
            dogsRepository = new DogsRepository();

            dogsRepository.GetAll();
            dogs = dogsRepository.dogs;

        }

        public void LoadData()
        {
            dogsRepository.GetAll();
            dogs = dogsRepository.dogs;

        }

    }
}
