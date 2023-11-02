using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Repositories;
using System;
using System.Collections.ObjectModel;

namespace TestProject_HundeKennel
{
    [TestClass]
    public class UnitTest1
    {
        private Dog dog1;
        private Dog dog2;
        private Dog dog3;
        private Dog dog4;
        private DogsRepository dr;

        [TestInitialize]
        public void TestInitialize()
        {
            dr = new DogsRepository();

            dog1 = new Dog("123/39", "Jon", new DateTime(2023, 1, 10), "123/41", "123/40", "Male", false, "1234", "DKK1", "Champion", true, false, new byte[0], "HD1", "AD1", "HZ1", "SP1", "Red", true);
            dog2 = new Dog("123/40", "Jane", new DateTime(2020, 10, 1), "456/13", "678/13", "Female", false, "5678", "DKK2", "Grand Champion", true, true, new byte[0], "HD2", "AD2", "HZ2", "SP2", "White", false);
            dog3 = new Dog("123/41", "Tim", new DateTime(2021, 10, 1), "456/11", "678/11", "Male", false, "7890", "DKK3", "Junior Champion", false, true, new byte[0], "HD3", "AD3", "HZ3", "SP3", "Black", true);
            dog4 = new Dog("123/42", "OldDog", new DateTime(2000, 1, 1), "456/12", "678/12", "Male", false, "1111", "DKK4", "Champion", true, false, new byte[0], "HD4", "AD4", "HZ4", "SP4", "Brown", true);

            dr.Add(dog1);
            dr.Add(dog2);
            dr.Add(dog3);
            dr.Add(dog4);

            /*var testDogs = new List<Dog>
            {
                new Dog("1", "Dog1", DateTime.Now, "3", "4", "Male", false, "1234", "DKK1", "Champion", true, false, new byte[0], "HD1", "AD1", "HZ1", "SP1", "Red", true),
                new Dog("2", "Dog2", DateTime.Now, "5", "6", "Female", false, "5678", "DKK2", "Grand Champion", true, true, new byte[0], "HD2", "AD2", "HZ2", "SP2", "White", false),
                new Dog("3", "Dog3", DateTime.Now, "7", "8", "Male", false, "9101", "DKK3", "Junior Champion", false, true, new byte[0], "HD3", "AD3", "HZ3", "SP3", "Black", true),
                new Dog("4", "Dog4", DateTime.Now, "9", "10", "Female", false, "1121", "DKK4", "Champion", true, false, new byte[0], "HD4", "AD4", "HZ4", "SP4", "Brown", true),
                new Dog("5", "Dog5", DateTime.Now, "11", "12", "Male", false, "1314", "DKK5", "Champion", true, false, new byte[0], "HD5", "AD5", "HZ5", "SP5", "Red", true),
                new Dog("6", "Dog6", DateTime.Now, "15", "16", "Female", false, "1415", "DKK6", "Grand Champion", true, true, new byte[0], "HD6", "AD6", "HZ6", "SP6", "White", false),
                new Dog("7", "Dog7", DateTime.Now, "17", "18", "Male", false, "1517", "DKK7", "Junior Champion", false, true, new byte[0], "HD7", "AD7", "HZ7", "SP7", "Black", true),
                new Dog("8", "Dog8", DateTime.Now, "19", "20", "Female", false, "1620", "DKK8", "Champion", true, false, new byte[0], "HD8", "AD8", "HZ8", "SP8", "Brown", true),
                new Dog("9", "Dog9", DateTime.Now, "21", "22", "Male", false, "2023", "DKK9", "Champion", true, false, new byte[0], "HD9", "AD9", "HZ9", "SP9", "Red", true),
                new Dog("10", "Dog10", DateTime.Now, "23", "24", "Female", false, "2225", "DKK10", "Grand Champion", true, true, new byte[0], "HD10", "AD10", "HZ10", "SP10", "White", false),
                new Dog("11", "Dog11", DateTime.Now, "25", "26", "Male", false, "2427", "DKK11", "Junior Champion", false, true, new byte[0], "HD11", "AD11", "HZ11", "SP11", "Black", true),
                new Dog("12", "Dog12", DateTime.Now, "27", "28", "Female", false, "2629", "DKK12", "Champion", true, false, new byte[0], "HD12", "AD12", "HZ12", "SP12", "Brown", true),
                new Dog("13", "Dog13", DateTime.Now, "28", "29", "Male", false, "2830", "DKK13", "Champion", true, false, new byte[0], "HD13", "AD13", "HZ13", "SP13", "Red", true),
                new Dog("14", "Dog14", DateTime.Now, "31", "32", "Female", false, "2933", "DKK14", "Grand Champion", true, true, new byte[0], "HD14", "AD14", "HZ14", "SP14", "White", false),
                new Dog("15", "Dog15", DateTime.Now, "33", "34", "Male", false, "3035", "DKK15", "Junior Champion", false, true, new byte[0], "HD15", "AD15", "HZ15", "SP15", "Black", true),
                new Dog("16", "Dog16", DateTime.Now, "35", "36", "Female", false, "3337", "DKK16", "Champion", true, false, new byte[0], "HD16", "AD16", "HZ16", "SP16", "Brown", true),
                new Dog("17", "Dog17", DateTime.Now, "37", "38", "Male", false, "3639", "DKK17", "Champion", true, false, new byte[0], "HD17", "AD17", "HZ17", "SP17", "Red", true),
                new Dog("18", "Dog18", DateTime.Now, "3940", "4142", "Female", false, "3743", "DKK18", "Grand Champion", true, true, new byte[0], "HD18", "AD18", "HZ18", "SP18", "White", false),
                new Dog("19", "Dog19", DateTime.Now, "4144", "4345", "Male", false, "3945", "DKK19", "Junior Champion", false, true, new byte[0], "HD19", "AD19", "HZ19", "SP19", "Black", true),
                new Dog("20", "Dog20", DateTime.Now, "4546", "4748", "Female", false, "4547", "DKK20", "Champion", true, false, new byte[0], "HD20", "AD20", "HZ20", "SP20", "Brown", true)
            };

            // Add the test dogs to the database
            foreach (var dog in testDogs)
            {
                dr.Add(dog);
            }*/

        }

        [TestCleanup]
        public void TestCleanup()
        {
            dr.Remove(dog1);
            dr.Remove(dog2);
            dr.Remove(dog3);
            dr.Remove(dog4);

            /*// Remove the test dogs from the database
            foreach (var dog in dr.GetAll().ToList())
            {
                dr.Remove(dog);
            }*/
        }

        [TestMethod]
        public void Dog_BothPedigreeNumberAreEqual_ReturnsPedigreeNumber()
        {
            Assert.AreEqual("123/39", dog1.PedigreeNumber);
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
            Collection<Dog> resultsFromDatabase = new Collection<Dog>(dr.GetAll().ToList());

            CollectionAssert.Contains(resultsFromDatabase, resultsFromDatabase.FirstOrDefault(d => d.Name == "Jon"));
            CollectionAssert.Contains(resultsFromDatabase, resultsFromDatabase.FirstOrDefault(d => d.Name == "Jane"));
            CollectionAssert.Contains(resultsFromDatabase, resultsFromDatabase.FirstOrDefault(d => d.Name == "Tim"));
        }

        [TestMethod]
        public void Update_DogInRepository_ShouldUpdateDatabaseAndCollection()
        {
            string updatedName = "UpdatedName";
            dog1.Name = updatedName;

            dr.Update(dog1);

            Dog updatedDog = dr.GetById(dog1.PedigreeNumber);

            Assert.AreEqual(updatedName, updatedDog.Name);
        }

        [TestMethod]
        public void IsDead_WhenDogIsMoreThan15YearsOld_ShouldBeTrue()
        {
            Dog retrievedDog = dr.GetById("123/42");

            Assert.IsTrue(retrievedDog.IsDead);
        }

        [TestMethod]
        public void BreedingStatus_WhenIsDeadIsTrue_ShouldBeFalse()
        {
            Assert.IsFalse(dog4.BreedingStatus);
        }

        /*[TestMethod]
        public void MatchTwoDogsAndShowFamilyTree_ShouldReturnRelatedDogs()
        {
            var dog1 = dr.GetById("1"); // Use the correct ID of dog1
            var dog2 = dr.GetById("2"); // Use the correct ID of dog2

            var familyTree = dr.MatchTwoDogsAndShowFamilyTree(dog1, dog2);

            // Assert the family tree contains the related dogs
            CollectionAssert.Contains(familyTree.ToList(), dog1);
            CollectionAssert.Contains(familyTree.ToList(), dog2);
            // Add more assertions as needed...
        }*/
    }
}
