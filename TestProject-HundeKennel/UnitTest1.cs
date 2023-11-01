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
            dog1 = new Dog("123/39", "Jon", new DateTime(2023, 1, 10), "123/41", "123/40", "Male", false, "1234", "DKK1", "Champion", true, false, new byte[0], "HD1", "AD1", "HZ1", "SP1", "Red", true);
            dog2 = new Dog("123/40", "Jane", new DateTime(2020, 10, 1), "456/13", "678/13", "Female", false, "5678", "DKK2", "Grand Champion", true, true, new byte[0], "HD2", "AD2", "HZ2", "SP2", "White", false);
            dog3 = new Dog("123/41", "Tim", new DateTime(2021, 10, 1), "456/11", "678/11", "Male", false, "7890", "DKK3", "Junior Champion", false, true, new byte[0], "HD3", "AD3", "HZ3", "SP3", "Black", true);
            dog4 = new Dog("123/42", "OldDog", new DateTime(2000, 1, 1), "456/12", "678/12", "Male", false, "1111", "DKK4", "Champion", true, false, new byte[0], "HD4", "AD4", "HZ4", "SP4", "Brown", true);

            dr = new DogsRepository();
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
            string dogId = dog1.PedigreeNumber;
            Dog retrievedDog = dr.GetById(dogId);
            string retrievedDogName = retrievedDog.Name;

            Assert.AreEqual("Jon", retrievedDogName);
        }

        [TestMethod]
        public void GetAll_AllNamesAreEqualTo_ReturnedNamesFromList()
        {
            Collection<Dog> result = new Collection<Dog>(dr.GetAll().ToList());

            Assert.AreEqual("Jon", result[0].Name);
            Assert.AreEqual("Jane", result[1].Name);
            Assert.AreEqual("Tim", result[2].Name);
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
    }
}
