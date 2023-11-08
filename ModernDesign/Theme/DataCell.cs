using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using ModernDesign.MVVM.Model;

namespace ModernDesign.Theme
{
    public class GridCell
    {

        public Dog DogInstance { get; set; }  
        public Brush BackgroundColor { get; set; }
        public Thickness BorderThickness { get; set; }
        public Brush BorderBrush { get; set; }
        
    }

}
