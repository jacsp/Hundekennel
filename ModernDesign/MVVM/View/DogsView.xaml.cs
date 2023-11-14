using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernDesign.MVVM.View
{
    /// <summary>
    /// Interaction logic for MoviesView.xaml
    /// </summary>
    public partial class DogsView : UserControl
    {
        private DogsViewModel DogsVM;
        public DogsView()
        {
            DogsVM = new DogsViewModel();
            DataContext = DogsVM;
            InitializeComponent();
        }

        private void lv_Dogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_Dogs.SelectedItem != null)
            {
                DogsVM.SelectedDog = (Dog)lv_Dogs.SelectedItem;
            }
            rb_Edit.IsChecked = false;
            rb_New.IsChecked = false;
        }

        private void rb_New_Checked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = true;
            sp_Column1.IsEnabled = true;
            sp_Column2.IsEnabled = true;
            tb_PedigreeNumber.IsEnabled = true;
            bt_Save.IsEnabled = true;

            lv_Dogs.SelectedItem = null;
            ClearAllTextBoxes();
        }



        private void rb_Edit_Checked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = true;
            sp_Column1.IsEnabled = true;
            sp_Column2.IsEnabled = true;
            bt_Update.IsEnabled = true;
        }

        private void rb_Edit_Unchecked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = false;
            sp_Column1.IsEnabled = false;
            sp_Column2.IsEnabled = false;
            bt_Update.IsEnabled = false;
        }

        private void rb_New_Unchecked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = false;
            sp_Column1.IsEnabled = false;
            sp_Column2.IsEnabled = false;
            tb_PedigreeNumber.IsEnabled = false;
            bt_Save.IsEnabled = false;
        }

        private void bt_Load_Click(object sender, RoutedEventArgs e)
        {
            DogsVM.LoadData();
            lv_Dogs.ItemsSource = DogsVM.Dogs;
        }

        private void bt_Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Dog instance and set its properties based on TextBox values
            Dog newDog = new Dog
            {
                PedigreeNumber = tb_PedigreeNumber.Text,
                Name = tb_Name.Text,
                DOB = DateTime.ParseExact(tb_DOB.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DadPedigreeNumber = tb_Dad.Text,
                MomPedigreeNumber = tb_Mom.Text,
                Gender = tb_Gender.Text,
                IsDead = rb_IsDead.IsChecked ?? false,
                ChipNumber = tb_ChipNumber.Text,
                DKKTitles = tb_DKKTitles.Text,
                Titles = tb_Titles.Text,
                BreedingStatus = rb_BreedingStatus.IsChecked ?? false,  
                MentalDescription = rb_MentalDescription.IsChecked ?? false, 
                Picture = new byte[0],
            };

            // Call the Add method from DogsRepository to add the new dog to the database
            DogsVM.dogsRepository.Add(newDog);

        }

        // Add this method to clear text in all TextBoxes
        private void ClearAllTextBoxes()
        {
            // Iterate through all children of the UserControl
            foreach (var child in GetAllChildren(Grid))
            {
                // If the child is a TextBox, set its Text property to an empty string
                if (child is TextBox textBox)
                {
                    textBox.Text = "";
                }
            }
        }

        // Recursive method to get all children of a parent control
        private static System.Collections.IEnumerable GetAllChildren(Panel parent)
        {
            foreach (var child in parent.Children)
            {
                yield return child;

                if (child is Panel nestedPanel)
                {
                    foreach (var nestedChild in GetAllChildren(nestedPanel))
                    {
                        yield return nestedChild;
                    }
                }
            }
        }

    }
}
