using ModernDesign.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.ViewModel
{
    class HomeViewModel
    {
        private DataImporter dataImporter;

        public HomeViewModel()
        {
            dataImporter = new DataImporter();
        }

        public void AddFile(string filePath)
        {

            dataImporter.AddOwnerFromFile(filePath);
            dataImporter.AddDogFromFile(filePath);
        }
    }
}
