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
    /// Interaction logic for PartnerMatchView.xaml
    /// </summary>
    public partial class PartnerMatchView : UserControl
    {
        public PartnerMatchView()
        {
            InitializeComponent();
        }
        private void Match_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PartnerMatchViewModel VM)
            {
                /*VM.SelectedText1 = tb_dog1Match.Text;
                VM.SelectedText2 = tb_dog2Match.Text;*/

                VM.BuildGrid1(tb_dog1Match.Text.ToString(), tb_dog2Match.Text.ToString());
            }
        }
    }
}
