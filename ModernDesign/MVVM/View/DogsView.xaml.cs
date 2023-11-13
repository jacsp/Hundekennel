using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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
        private DogsViewModel DogsVM = new DogsViewModel();
        public DogsView()
        {
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


        }

        private void rb_Edit_Checked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = true;
            sp_Column1.IsEnabled = true;
            sp_Column2.IsEnabled = true;
        }

        private void rb_Edit_Unchecked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = false;
            sp_Column1.IsEnabled = false;
            sp_Column2.IsEnabled = false;
            rb_New.IsChecked = false;
        }

        private void rb_New_Unchecked(object sender, RoutedEventArgs e)
        {
            sp_Column0.IsEnabled = false;
            sp_Column1.IsEnabled = false;
            sp_Column2.IsEnabled = false;
            rb_Edit.IsChecked = false;
        }
    }
}
