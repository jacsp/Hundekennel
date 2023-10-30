using ModernDesign.Core;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.ViewModel.Repositories;
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
        private readonly DogsRepository _dogsRepository;
        public ObservableCollection<Dog> dogs;

        public DogsViewModel()
        {
            _dogsRepository = new DogsRepository();
            dogs = _dogsRepository.dogs;
            dogs = (ObservableCollection<Dog>)_dogsRepository.GetAll();
        }

        public void AddDog(Dog dog)
        {

        }
    }
}
