using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Repositories;
using System;
using System.Diagnostics;
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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            dr.Remove(dog1);
            dr.Remove(dog2);
            dr.Remove(dog3);
            dr.Remove(dog4);

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
            // Arrange
            Dog d1 = dr.GetById("1");
            d1.DadPedigreeNumber = "3";
            d1.MomPedigreeNumber = "5";
            dr.Update(d1);

            Dog d2 = dr.GetById("2");
            d2.DadPedigreeNumber = "4";
            d2.MomPedigreeNumber = "6";
            dr.Update(d2);

            Dog d3 = dr.GetById("3");
            d3.DadPedigreeNumber = "7";
            d3.MomPedigreeNumber = "9";
            dr.Update(d3);

            Dog d4 = dr.GetById("4");
            d4.DadPedigreeNumber = "8";
            d4.MomPedigreeNumber = "10";
            dr.Update(d4);

            // Act
            Collection<Dog> familyTree = new Collection<Dog>(dr.MatchTwoDogsAndShowFamilyTree(d1, d2).ToList());

            // Debug
            string ancestorPedigreeNumberString = "";
            foreach (Dog dog in familyTree)
            {
                if (dog != null)
                {
                    ancestorPedigreeNumberString += "-" + dog.PedigreeNumber;
                }
            }
            Debug.WriteLine(ancestorPedigreeNumberString);
            Debug.WriteLine(familyTree.Count);
            // Debug

            // Assert
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "3"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "5"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "7"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "9"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "4"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "6"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "8"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "10"));
        }*/
    }
}
