using ModernDesign.MVVM.Model;
using System.Windows.Input;
namespace TestProject_HundeKennel
{
    [TestClass]
    public class UnitTest1
    {
        Dog dog;

        [TestInitialize]
        public void TestInitialize()
        {
            dog = new Dog("PedigreeNumber", "Name", new DateTime(2023 / 10 / 1), "Dad", "Mom", GenderType.Male, false);
        }
        /*
        + PedigreeNumber : string
        + Name : string
        + Dad : string
        + Mom : string
        + DOB : DateTime
        + Gender : GenderType
        + Dead : bool 
        */
        [TestCleanup]
        public void TestCleanup()
        {
            
        }
        [TestMethod]
        public void Dog_BothPedigreeNumberAreEquel_Returns123()
        {
            Assert.AreEqual("PedigreeNumber", dog.PedigreeNumber);
        }
    }
}