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
    class PartnerMatchViewModel
    {
        public ObservableCollection<GridCell> GridCells { get; set; }
        public ObservableCollection<GridCell> GridColumn0 { get; set; }
        public ObservableCollection<GridCell> GridColumn1 { get; set; }
        public ObservableCollection<GridCell> GridColumn2 { get; set; }
        public ObservableCollection<GridCell> GridColumn3 { get; set; }

        private readonly DogsRepository dogsRepository;

        public List<Dog> FamilyTree { get; set; }


        public PartnerMatchViewModel()
        {
            dogsRepository = new DogsRepository();
            dogsRepository.GetAll();

            BuildGrid1("1");
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

        public void BuildGrid(string id1, string id2)
        {
            FamilyTree = dogsRepository.MatchTwoDogsAndShowFamilyTree(id1, id2).ToList();

            GridCells = new ObservableCollection<GridCell>();

            int numRows = 2; // Initial number of rows

            for (int col = 0; col < 4; col++)
            {
                for (int i = 0; i < numRows; i++)
                {
                    GridCells.Add(new GridCell
                    {
                        BackgroundColor = (i + col) % 2 == 0 ? Brushes.DarkGray : Brushes.LightGray,
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Black
                    });
                }

                numRows *= 2; // Double the number of rows for the next column
            }
        }



    }
}
