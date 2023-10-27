
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Hundekennel_TestProject

{

    [TestClass]
    public class UnitTest1
    {


        [TestInitialize]
        public void TestInitialize()
        {
            Dog dog = new Dog(pedigreeNumber, name, DOB,
                health, dad, mom, dKKTitles, titles, gender,
                dead, breedingStatus, mentalDescription,
                color, picture, breedingApproval)
            {

            };
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestMethod]
        public void CreateDog_Test()
        {
            Assert.AreEqual(dog.PedigreeNumber = pedigreeNumber);
        }

    }


}