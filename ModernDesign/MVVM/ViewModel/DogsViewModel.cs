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
        private readonly DogOwnerRepository ownerRepository;
        private ObservableCollection<Dog> dogs;

        public ObservableCollection<Dog> Dogs
        {
            get { return dogs; }
            set { dogs = value; }
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
            ownerRepository = new DogOwnerRepository();

            // Add mock data
            //AddMockDogOwners();
            //AddMockDogs();


            ownerRepository.GetAll();
            dogsRepository.GetAll();
            dogs = dogsRepository.dogs;



        }

        private void AddMockDogOwners()
        {
            for (int i = 1; i <= 20; i++)
            {
                var owner = new DogOwner
                {
                    Name = $"Owner{i}",
                    Address = $"Address{i}",
                    PostalCode = $"1234{i}",
                    City = $"City{i}",
                    Phone = $"123-456-789{i}",
                    Email = $"owner{i}@example.com"
                };

                ownerRepository.Add(owner);
            }
        }

        private void AddMockDogs()
        {
            var random = new Random();
            for (int i = 1; i <= 60; i++)
            {
                var dadNumber = random.Next(1, 21) * 2;  // Random even number between 2 and 40
                var momNumber = random.Next(1, 21) * 2 + 1;  // Random odd number between 1 and 39

                var dog = new Dog
                {
                    PedigreeNumber = $"Dog{i}",
                    Name = $"DogName{i}",
                    DOB = DateTime.Now.AddYears(-random.Next(1, 5)),
                    DadPedigreeNumber = $"Dog{dadNumber}",
                    MomPedigreeNumber = $"Dog{momNumber}",
                    Gender = (i % 2 == 0) ? "Male" : "Female",
                    IsDead = false,
                    ChipNumber = $"Chip{i}",
                    DKKTitles = $"Titles{i}",
                    Titles = $"Titles{i}",
                    BreedingStatus = true,
                    MentalDescription = true,
                    Picture = new byte[0],
                    HD = $"HD{i}",
                    AD = $"AD{i}",
                    HZ = $"HZ{i}",
                    SP = $"SP{i}",
                    Color = $"Color{i}",
                    BreedingApproval = true,
                    //OwnerId = 146 + i // Assuming OwnerId is sequential for simplicity
                };

                dogsRepository.Add(dog);
            }
        }

    }
}
