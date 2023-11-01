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

        private readonly DogsRepository dogsRepository;
        private ObservableCollection<Dog> dogs;
        public ObservableCollection<Dog> Dogs
        {
            get { return dogs; }
            set { dogs = value; }
        }

        //public RelayCommand SelectedDog { get; set; }
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
            

            /*Dog dog4 = new Dog("123/42", "Lucy", new DateTime(2022, 5, 15), "456/12", "678/12", "Female", true, "9876", "DKK4", "Novice", false, true, new byte[0], "HD4", "AD4", "HZ4", "SP4", "Brown", false);
            Dog dog5 = new Dog("123/43", "Max", new DateTime(2019, 8, 20), "456/14", "678/14", "Male", false, "7654", "DKK5", "Elite", true, false, new byte[0], "HD5", "AD5", "HZ5", "SP5", "Yellow", true);
            Dog dog6 = new Dog("123/44", "Luna", new DateTime(2020, 3, 5), "456/15", "678/15", "Female", false, "6543", "DKK6", "Champion", true, false, new byte[0], "HD6", "AD6", "HZ6", "SP6", "Black", true);
            Dog dog7 = new Dog("123/45", "Rocky", new DateTime(2021, 11, 2), "456/16", "678/16", "Male", false, "5432", "DKK7", "Grand Champion", true, true, new byte[0], "HD7", "AD7", "HZ7", "SP7", "Gray", false);
            Dog dog8 = new Dog("123/46", "Daisy", new DateTime(2018, 12, 12), "456/17", "678/17", "Female", true, "4321", "DKK8", "Junior Champion", false, true, new byte[0], "HD8", "AD8", "HZ8", "SP8", "White", true);
            Dog dog9 = new Dog("123/47", "Buddy", new DateTime(2021, 6, 7), "456/18", "678/18", "Male", false, "3210", "DKK9", "Novice", false, true, new byte[0], "HD9", "AD9", "HZ9", "SP9", "Red", true);
            Dog dog10 = new Dog("123/48", "Molly", new DateTime(2022, 4, 25), "456/19", "678/19", "Female", false, "2109", "DKK10", "Elite", true, false, new byte[0], "HD10", "AD10", "HZ10", "SP10", "Black", true);
            Dog dog11 = new Dog("123/49", "Bentley", new DateTime(2020, 9, 15), "456/20", "678/20", "Male", false, "1098", "DKK11", "Champion", true, true, new byte[0], "HD11", "AD11", "HZ11", "SP11", "Brown", false);
            Dog dog12 = new Dog("123/50", "Zoe", new DateTime(2019, 7, 30), "456/21", "678/21", "Female", true, "0987", "DKK12", "Grand Champion", false, true, new byte[0], "HD12", "AD12", "HZ12", "SP12", "Yellow", true);
            Dog dog13 = new Dog("123/51", "Rocky", new DateTime(2021, 11, 2), "456/16", "678/16", "Male", false, "9876", "DKK13", "Junior Champion", false, false, new byte[0], "HD13", "AD13", "HZ13", "SP13", "Gray", true);

            _dogsRepository.Add(dog4);
            _dogsRepository.Add(dog5);
            _dogsRepository.Add(dog6);
            _dogsRepository.Add(dog9);
            _dogsRepository.Add(dog10);
            _dogsRepository.Add(dog11);
            _dogsRepository.Add(dog12);
            _dogsRepository.Add(dog13);*/
        }


    }
}
