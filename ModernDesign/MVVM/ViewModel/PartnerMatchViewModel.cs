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
using System.Windows.Navigation;

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
                public RelayCommand ShowFamilyTreeCommand { get; set; }
        public List<Dog> FamilyTree { get; set; }
        public ObservableCollection<GridCell> GridCells { get; set; }

        private ObservableCollection<GridCell> gridColumn0 { get; set; }
        private ObservableCollection<GridCell> gridColumn1 { get; set; }
        private ObservableCollection<GridCell> gridColumn2 { get; set; }
        private ObservableCollection<GridCell> gridColumn3 { get; set; }

        public ObservableCollection<GridCell> GridColumn0
        {
            get { return gridColumn0; }
            set
            {
                gridColumn0 = value;
                OnPropertyChanged(nameof(gridColumn0));
            }
        }
        public ObservableCollection<GridCell> GridColumn1
        {
            get { return gridColumn1; }
            set
            {
                gridColumn1 = value;
                OnPropertyChanged(nameof(gridColumn1));
            }
        }
        public ObservableCollection<GridCell> GridColumn2
        {
            get { return gridColumn2; }
            set
            {
                gridColumn2 = value;
                OnPropertyChanged(nameof(gridColumn2));
            }
        }
        public ObservableCollection<GridCell> GridColumn3
        {
            get { return gridColumn3; }
            set
            {
                gridColumn3 = value;
                OnPropertyChanged(nameof(gridColumn3));
            }
        }

        private ObservableCollection<GridCell> gridColumn0Row1 { get; set; }
        private ObservableCollection<GridCell> gridColumn1Row1 { get; set; }
        private ObservableCollection<GridCell> gridColumn2Row1 { get; set; }
        private ObservableCollection<GridCell> gridColumn3Row1 { get; set; }

        public ObservableCollection<GridCell> GridColumn0Row1
        {
            get { return gridColumn0Row1; }
            set
            {
                gridColumn0Row1 = value;
                OnPropertyChanged(nameof(gridColumn0Row1));
            }
        }
        public ObservableCollection<GridCell> GridColumn1Row1
        {
            get { return gridColumn1Row1; }
            set
            {
                gridColumn1Row1 = value;
                OnPropertyChanged(nameof(gridColumn1Row1));
            }
        }
        public ObservableCollection<GridCell> GridColumn2Row1
        {
            get { return gridColumn2Row1; }
            set
            {
                gridColumn2Row1 = value;
                OnPropertyChanged(nameof(gridColumn2Row1));
            }
        }
        public ObservableCollection<GridCell> GridColumn3Row1
        {
            get { return gridColumn3Row1; }
            set
            {
                gridColumn3Row1 = value;
                OnPropertyChanged(nameof(gridColumn3Row1));
            }
        }

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

            GridColumn0Row1 = new ObservableCollection<GridCell>();
            GridColumn1Row1 = new ObservableCollection<GridCell>();
            GridColumn2Row1 = new ObservableCollection<GridCell>();
            GridColumn3Row1 = new ObservableCollection<GridCell>();

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
