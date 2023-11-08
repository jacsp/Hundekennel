using ModernDesign.Core;
using ModernDesign.MVVM.Model.Repositories;
using ModernDesign.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.ViewModel
{
    class PartnerMatchViewModel : ObservableObject
    {
        private Dog selectedDog = null;

        private readonly DogsRepository dogsRepository;
        private ObservableCollection<Dog> dogs;

        public IEnumerable<Dog> FamilyTree { get; set; }

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

        public PartnerMatchViewModel()
        {
            dogsRepository = new DogsRepository();
            dogsRepository.GetAll();
            dogs = dogsRepository.dogs;

            FamilyTree = dogsRepository.GetFamilyTree("1");
        }

        public void ShowPartnerMatch(string dog1Id, string Dog2Id)
        {
            FamilyTree = dogsRepository.MatchTwoDogsAndShowFamilyTree(dog1Id, Dog2Id);
        }
    }
}
