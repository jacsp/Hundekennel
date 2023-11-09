using ModernDesign.Core;
using ModernDesign.MVVM.Model.Repositories;
using ModernDesign.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernDesign.Theme;
using System.Windows.Media;
using System.Windows;

namespace ModernDesign.MVVM.ViewModel
{
    class PartnerMatchViewModel : ObservableObject
    {
        private readonly DogsRepository dogsRepository;
        private Object selectedText1;
        private Object selectedText2;
        public Object SelectedText1
        {
            get { return selectedText1; }
            set
            {
                selectedText1 = value;
                OnPropertyChanged(nameof(selectedText1));
            }
        }
        public Object SelectedText2
        {
            get { return selectedText2; }
            set
            {
                selectedText2 = value;
                OnPropertyChanged(nameof(selectedText2));
            }
        }

        public ObservableCollection<GridCell> GridCells { get; set; }
        public ObservableCollection<GridCell> GridColumn0 { get; set; }
        public ObservableCollection<GridCell> GridColumn1 { get; set; }
        public ObservableCollection<GridCell> GridColumn2 { get; set; }
        public ObservableCollection<GridCell> GridColumn3 { get; set; }
        public RelayCommand ShowFamilyTreeCommand { get; set; }
        public List<Dog> FamilyTree { get; set; }


        public PartnerMatchViewModel()
        {
            dogsRepository = new DogsRepository();
            dogsRepository.GetAll();
        }

        public void BuildGrid1(string id)
        {
            FamilyTree = dogsRepository.GetFamilyTree(id).ToList();

            GridColumn0 = new ObservableCollection<GridCell>();
            GridColumn1 = new ObservableCollection<GridCell>();
            GridColumn2 = new ObservableCollection<GridCell>();
            GridColumn3 = new ObservableCollection<GridCell>();
            
            Dog dog1 = dogsRepository.GetById(id);
            GridColumn0.Add(new GridCell
            {
                DogInstance = dog1,
                BackgroundColor = Brushes.DarkGray,
                BorderThickness = new Thickness(0.5),
                BorderBrush = Brushes.Black
            });

            int dogsAdded = 0;

            foreach (Dog dog in FamilyTree)
            {
                if (dogsAdded < 2)
                {
                    GridColumn1.Add(new GridCell
                    {
                        DogInstance = dog,
                        BackgroundColor = Brushes.DarkGray,
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Black
                    });
                    dogsAdded++;
                }
                else if (dogsAdded < 6)
                {
                    GridColumn2.Add(new GridCell
                    {
                        DogInstance = dog,
                        BackgroundColor = Brushes.DarkGray,
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Black
                    });
                    dogsAdded++;
                }
                else if (dogsAdded < 14)
                {
                    GridColumn3.Add(new GridCell
                    {
                        DogInstance = dog,
                        BackgroundColor = Brushes.DarkGray,
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Black
                    });
                    dogsAdded++;
                }                
            }
        }
    }
}
