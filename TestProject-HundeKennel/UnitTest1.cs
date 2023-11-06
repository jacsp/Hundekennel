    using Microsoft.Data.SqlClient;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ModernDesign.MVVM.Model;
    using ModernDesign.MVVM.Model.Repositories;
    using System;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Reflection.Emit;

    namespace TestProject_HundeKennel
    {
        [TestClass]
        public class UnitTest1
        {
            private Dog dog1;
            private Dog dog2;
            private Dog dog3;
            private DogsRepository dr;

            private DogOwner owner;
            private DogOwnerRepository dor;

            [TestInitialize]
            public void TestInitialize()
            {
                dog1 = new Dog("123/39", "Jon", new DateTime(2023, 1, 10), "123/41", "123/40", "Male", false, "1234", "DKK1", "Champion", true, false, new byte[0], "HD1", "AD1", "HZ1", "SP1", "Red", true);
                dog2 = new Dog("123/40", "Jane", new DateTime(2020, 10, 1), "456/13", "678/13", "Female", false, "5678", "DKK2", "Grand Champion", true, true, new byte[0], "HD2", "AD2", "HZ2", "SP2", "White", false);
                dog3 = new Dog("123/41", "Tim", new DateTime(2021, 10, 1), "456/11", "678/11", "Male", false, "7890", "DKK3", "Junior Champion", false, true, new byte[0], "HD3", "AD3", "HZ3", "SP3", "Black", true);

                dr = new DogsRepository();
                dr.Add(dog1);
                dr.Add(dog2);
                dr.Add(dog3);
            }

            [TestCleanup]
            public void TestCleanup()
            {
                dr.Remove(dog1);
                dr.Remove(dog2);
                dr.Remove(dog3);
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

                Assert.AreEqual(3, result.Count);
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

                //Assert.IsNotNull(updatedDog);
                Assert.AreEqual(updatedName, updatedDog.Name);
            }

            [TestMethod]        
            public void Add_AddDogOwner_ReturnOK()
            {
                // Arrange                
                owner = new DogOwner(1, "John Doe", "123 Main St", "12345", "Sample City", "555-123-4567", "johndoe@example.com");
               
                // Act
                dor = new DogOwnerRepository();
                dor.Add(owner);

                // Assert                
                Assert.AreNotEqual(0, owner.DogOwnerId);
            }

             [TestMethod]
            public void Remove_RemoveDogOwner_ReturnOK()
            {
                // Arrange: Create a new DogOwner object with test data
                owner = new DogOwner(1, "John Doe", "123 Main St", "12345", "Sample City", "555-123-4567", "johndoe@example.com");

                // Act: Add the DogOwner to the repository then delete it
                dor = new DogOwnerRepository();
                dor.Add(owner);                
                Assert.AreNotEqual(0, owner.DogOwnerId);    dor.Remove(owner);

                // Assert: Verify that the DogOwner is removed
                IEnumerable<DogOwner> allDogOwners = dor.GetAll();
                Assert.IsFalse(allDogOwners.Any(d => d.DogOwnerId == owner.DogOwnerId));
            }



            //[TestMethod]
            //public void GetAll_RetrieveAllDogOwners_ReturnOK()
            //{
            //    // Arrange: Add some DogOwners to the database
            //    var owners = new List<DogOwner>
            //    {
            //        new DogOwner(1, "John Doe", "123 Main St", "12345", "Sample City", "555-123-4567", "johndoe@example.com"),
            //        new DogOwner(2, "Jane Smith", "456 Elm St", "54321", "Another City", "555-987-6543", "janesmith@example.com")
            //    };
            //    dor = new DogOwnerRepository();
            //    owners.ForEach(owner => dor.Add(owner));

            //    // Act: Retrieve all dog owners from the repository
            //    var allDogOwners = dor.GetAll();

            //    // Assert: Verify that the dog owners were retrieved as expected
            //    CollectionAssert.AreEquivalent(owners, allDogOwners.ToList());

            //}
        }
    }


        
    
