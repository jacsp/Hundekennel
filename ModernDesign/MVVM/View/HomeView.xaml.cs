using DocumentFormat.OpenXml.Spreadsheet;
using ModernDesign.Core;
using ModernDesign.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private readonly MainViewModel MainVM;
        private readonly HomeViewModel HomeVM;


        public Object CurrentView;

        public HomeView()
        {
            MainVM = new MainViewModel();
            HomeVM = new HomeViewModel();
            
            DataContext = HomeVM;


            InitializeComponent();

        }

        private void bt_AddFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HomeVM.AddFile(tb_AddFile.Text.ToString());
                MessageBox.Show("Databasen er opdateret", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            tb_AddFile.Text = "";
        }

        private void tb_AddFile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tb_AddFile.Text = "";
        }

        private void bt_ShowOverview_Click(object sender, RoutedEventArgs e)
        {
            MainVM.DogsViewChange();
        }
    }
}
