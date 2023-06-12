using LogisticsApi.DAL.Models;

namespace LogisticsApi.Test
{
    [TestClass]
    public class PackageTest
    {
        [TestMethod]
        public void IsValid_WithinRange()
        {
            var p = new Package("999000000000001", 1, 1, 1, 1);
            Assert.IsTrue(p.IsValid);
        }
        [TestMethod]
        public void IsValid_InvalidWidth()
        {
            var p = new Package("999000000000001", 1, -1, 1, 1);
            Assert.IsFalse(p.IsValid);
        }
        [TestMethod]
        public void IsValid_InvalidHeight()
        {
            var p = new Package("999000000000001", 61, 1, 1, 1);
            Assert.IsFalse(p.IsValid);
        }
        [TestMethod]
        public void IsValid_InvalidLenght()
        {
            var p = new Package("999000000000001", 1, 1, 100, 1);
            Assert.IsFalse(p.IsValid);

        }
        [TestMethod]
        public void IsValid_InvalidWeight()
        {
            var p = new Package("999000000000001", 1, 1, 1, 50950);
            Assert.IsFalse(p.IsValid);
        }
    }
}