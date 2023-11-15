using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Repositories;
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
            rb_NewOwner.IsChecked = false;
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
            rb_NewOwner.IsChecked = false;
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

        private void bt_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {            
            DogsVM.UpdateSelectedDog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DogsVM.SelectedDog = new Dog
                {
                    PedigreeNumber = string.IsNullOrEmpty(tb_PedigreeNumber.Text) ? string.Empty : tb_PedigreeNumber.Text,
                    Name = string.IsNullOrEmpty(tb_Name.Text) ? string.Empty : tb_Name.Text,
                    DOB = string.IsNullOrEmpty(tb_DOB.Text) ? DateTime.MinValue : DateTime.ParseExact(tb_DOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DadPedigreeNumber = string.IsNullOrEmpty(tb_Dad.Text) ? string.Empty : tb_Dad.Text,
                    MomPedigreeNumber = string.IsNullOrEmpty(tb_Mom.Text) ? string.Empty : tb_Mom.Text,
                    Gender = string.IsNullOrEmpty(tb_Gender.Text) ? string.Empty : tb_Gender.Text,
                    IsDead = rb_IsDead.IsChecked ?? false,
                    ChipNumber = string.IsNullOrEmpty(tb_ChipNumber.Text) ? string.Empty : tb_ChipNumber.Text,
                    DKKTitles = string.IsNullOrEmpty(tb_DKKTitles.Text) ? string.Empty : tb_DKKTitles.Text,
                    Titles = string.IsNullOrEmpty(tb_Titles.Text) ? string.Empty : tb_Titles.Text,
                    BreedingStatus = rb_BreedingStatus.IsChecked ?? false,
                    MentalDescription = rb_MentalDescription.IsChecked ?? false,
                    Picture = new byte[0],

                    HD = string.IsNullOrEmpty(tb_HD.Text) ? string.Empty : tb_HD.Text,
                    AD = string.IsNullOrEmpty(tb_AD.Text) ? string.Empty : tb_AD.Text,
                    HZ = string.IsNullOrEmpty(tb_HZ.Text) ? string.Empty : tb_HZ.Text,
                    SP = string.IsNullOrEmpty(tb_SP.Text) ? string.Empty : tb_SP.Text,
                    Color = string.IsNullOrEmpty(tb_Color.Text) ? string.Empty : tb_Color.Text,
                    BreedingApproval = rb_BreedingApproval.IsChecked ?? false,

                };

                DogsVM.AddSelectedDog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ClearAllTextBoxes();
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

        private void rb_NewOwner_Checked(object sender, RoutedEventArgs e)
        {
            bt_SaveOwner.IsEnabled = true;
            sp_OwnerInfo.IsEnabled = true;
        }
        private void rb_NewOwner_Unchecked(object sender, RoutedEventArgs e)
        {
            bt_SaveOwner.IsEnabled = false;
            sp_OwnerInfo.IsEnabled = false;
        }

        private void bt_SaveOwner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
