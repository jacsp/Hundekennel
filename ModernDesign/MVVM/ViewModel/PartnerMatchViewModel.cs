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

        // dog 1
        private ObservableCollection<GridCell> gridColumn0;
        private ObservableCollection<GridCell> gridColumn1;
        private ObservableCollection<GridCell> gridColumn2;
        private ObservableCollection<GridCell> gridColumn3;
        // dog 2
        private ObservableCollection<GridCell> gridColumn0Row1;
        private ObservableCollection<GridCell> gridColumn1Row1;
        private ObservableCollection<GridCell> gridColumn2Row1;
        private ObservableCollection<GridCell> gridColumn3Row1;

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
        // dog 1
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
        // dog 2
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

        public void BuildGrid1(string id1, string id2)
        {
            List<Dog> FamilyTree1 = dogsRepository.GetFamilyTree(id1).ToList();
            List<Dog> FamilyTree2 = dogsRepository.GetFamilyTree(id2).ToList();

            GridColumn0 = new ObservableCollection<GridCell>();
            GridColumn1 = new ObservableCollection<GridCell>();
            GridColumn2 = new ObservableCollection<GridCell>();
            GridColumn3 = new ObservableCollection<GridCell>();

            GridColumn0Row1 = new ObservableCollection<GridCell>();
            GridColumn1Row1 = new ObservableCollection<GridCell>();
            GridColumn2Row1 = new ObservableCollection<GridCell>();
            GridColumn3Row1 = new ObservableCollection<GridCell>();


            // Adding the 1 dog
            Dog dog1 = dogsRepository.GetById(id1);
            GridColumn0.Add(new GridCell
            {
                DogInstance = dog1,
            });

            int dogsAdded = 0;

            foreach (Dog dog in FamilyTree1)
            {
                if (dogsAdded < 2)
                {
                    GridColumn1.Add(new GridCell
                    {
                        DogInstance = dog,
                    });
                    dogsAdded++;
                }
                else if (dogsAdded < 6)
                {
                    GridColumn2.Add(new GridCell
                    {
                        DogInstance = dog,
                    });
                    dogsAdded++;
                }
                else if (dogsAdded < 14)
                {
                    GridColumn3.Add(new GridCell
                    {
                        DogInstance = dog,
                    });
                    dogsAdded++;
                }                
            }

            // Adding the 2 dog
            Dog dog2 = dogsRepository.GetById(id2);
            GridColumn0Row1.Add(new GridCell
            {
                DogInstance = dog2,
            });

            int dogsAdded2 = 0;

            foreach (Dog dog in FamilyTree2)
            {
                if (dogsAdded2 < 2)
                {
                    GridColumn1Row1.Add(new GridCell
                    {
                        DogInstance = dog,
                    });
                    dogsAdded2++;
                }
                else if (dogsAdded2 < 6)
                {
                    GridColumn2Row1.Add(new GridCell
                    {
                        DogInstance = dog,
                    });
                    dogsAdded2++;
                }
                else if (dogsAdded2 < 14)
                {
                    GridColumn3Row1.Add(new GridCell
                    {
                        DogInstance = dog,
                    });
                    dogsAdded2++;
                }
            }
        }
    }
}
