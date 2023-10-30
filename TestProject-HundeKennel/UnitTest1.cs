using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.ViewModel.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
namespace TestProject_HundeKennel
{
    [TestClass]
    public class UnitTest1
    {
        private Dog dog1;
        private Dog dog2;
        private Dog dog3;
        private DogsRepository dr;

        [TestInitialize]
        public void TestInitialize()
        {
            dog1 = new Dog("1", "Jon", new DateTime(2023 / 10 / 1), "123/41", "123/40", "Male", false);
            dog2 = new Dog("123/40", "Jane", new DateTime(2020 / 10 / 1), "456/13", "678/13", "Female", false);
            dog3 = new Dog("123/41", "Tim", new DateTime(2021 / 10 / 1), "456/11", "678/11", "Male", false);
            
            dr = new DogsRepository();
            dr.Add(dog1);
            dr.Add(dog2);
            dr.Add(dog3);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            
        }
        [TestMethod]
        public void Dog_BothPedigreeNumberAreEqual_ReturnsPedigreeNumber()
        {
            Assert.AreEqual("1", dog1.PedigreeNumber);
        }
        [TestMethod]
        public void GetById_NameIsEqualTo_ReturnedName()
        {
            Dog retrievedDog = dr.GetById(dog1.PedigreeNumber);
            Assert.AreEqual("Jon", retrievedDog.Name);
        }
        [TestMethod]
        public void GetAll_AllNamesAreEqualTo_ReturnedNamesFromList()
        {
            IEnumerable<Dog> result = dr.GetAll();
            List<Dog> results = result.ToList();

            //Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Jon", results[1].Name);
            //Assert.AreEqual("Jane", result[1].Name);
            //Assert.AreEqual("Tim", result[2].Name);
        }

    }
}