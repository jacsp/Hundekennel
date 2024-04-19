
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Repositories;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net;


namespace TestProject_HundeKennel
{
    [TestClass]
    public class UnitTest1
    {
        private Dog dog1;
        private Dog dog2;
        private Dog dog3;
        private Dog dog4;

        private DogOwner owner1; 
        private DogOwner owner2;
        private DogOwner owner3;
        private DogOwner owner4;

        private DogsRepository dr;
        private DogOwnerRepository or;

        [TestInitialize]
        public void TestInitialize()
        {
            dr = new DogsRepository();
            or = new DogOwnerRepository();
            /*
                         owner1 = new DogOwner("name1", "address1", "postalCode1", "city1", "phone1", "email1");
                         owner2 = new DogOwner("name2", "address2", "postalCode2", "city2", "phone2", "email2");
                         owner3 = new DogOwner("name3", "address3", "postalCode3", "city3", "phone3", "email3");
                         owner4 = new DogOwner("name4", "address4", "postalCode4", "city4", "phone4", "email4");

                        *//*            or.Remove(owner1);
                                    or.Remove(owner2);
                                    or.Remove(owner3);
                                    or.Remove(owner4);*/
            /*            dr.Remove(dog1);
                        dr.Remove(dog2);
                        dr.Remove(dog3);
                        dr.Remove(dog4);*/

            dog1 = new Dog("123/39", "Jon", new DateTime(2023, 1, 10), "123/41", "123/40", "Male", false, "1234", "DKK1", "Champion", true, false, new byte[0], "HD1", "AD1", "HZ1", "SP1", "Red", true, "email1");
            dog2 = new Dog("123/40", "Jane", new DateTime(2020, 10, 1), "456/13", "678/13", "Female", false, "5678", "DKK2", "Grand Champion", true, true, new byte[0], "HD2", "AD2", "HZ2", "SP2", "White", false, "email2");
            dog3 = new Dog("123/41", "Tim", new DateTime(2021, 10, 1), "456/11", "678/11", "Male", false, "7890", "DKK3", "Junior Champion", false, true, new byte[0], "HD3", "AD3", "HZ3", "SP3", "Black", true, "email3");
            dog4 = new Dog("123/42", "OldDog", new DateTime(2000, 1, 1), "456/12", "678/12", "Male", false, "1111", "DKK4", "Champion", true, false, new byte[0], "HD4", "AD4", "HZ4", "SP4", "Brown", true, "email4");

        }

        [TestCleanup]
        public void TestCleanup()
        {
/*
            dr.Remove(dog1);
            dr.Remove(dog2);
            dr.Remove(dog3);
            dr.Remove(dog4);
            or.Remove(owner1);
            or.Remove(owner2);
            or.Remove(owner3);
            or.Remove(owner4);
*/

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
        public void MatchTwoDogsAndShowFamilyTree_ShouldReturnRelatedDogs()
        {
            // Arrange
            Dog d1 = dr.GetById("Dog1");
            d1.DadPedigreeNumber = "Dog3";
            d1.MomPedigreeNumber = "Dog5";
            dr.Update(d1);

            Dog d2 = dr.GetById("Dog2");
            d2.DadPedigreeNumber = "Dog4";
            d2.MomPedigreeNumber = "Dog6";
            dr.Update(d2);

            Dog d3 = dr.GetById("Dog3");
            d3.DadPedigreeNumber = "Dog7";
            d3.MomPedigreeNumber = "Dog9";
            dr.Update(d3);

            Dog d4 = dr.GetById("Dog4");
            d4.DadPedigreeNumber = "Dog8";
            d4.MomPedigreeNumber = "Dog10";
            dr.Update(d4);

            // Act
            Collection<Dog> familyTree = new Collection<Dog>(dr.MatchTwoDogsAndShowFamilyTree(d1.PedigreeNumber, d2.PedigreeNumber).ToList());

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
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog3"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog5"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog7"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog9"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog4"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog6"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog8"));
            CollectionAssert.Contains(familyTree, familyTree.FirstOrDefault(d => d.PedigreeNumber == "Dog10"));
        }
    }
}