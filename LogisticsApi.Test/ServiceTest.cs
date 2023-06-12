using LogisticsApi.DAL.Models;
using LogisticsApi.DAL.Repositories;
using LogisticsApi.Services;

namespace LogisticsApi.Test
{
    [TestClass]
    public class ServiceTest
    {
        private PackageService _service;
        [TestInitialize]
        public void Init()
        {
            _service = new PackageService(new PackageRepository());
        }
        [TestMethod]
        public void GetAll_Contains_Two_Packages()
        {
            var result = _service.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void GetPackage_Finds_Existing()
        {
            var result = _service.GetPackage("999000000000000001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetPackage_Not_Found()
        {
            var action = new Action(() => _service.GetPackage("999000000000000004"));
            Assert.ThrowsException<PackageRepository.NotFoundException>(action);
        }
        [TestMethod]
        public void GetPackage_Invalid_Id()
        {
            var action = new Action(() => _service.GetPackage("id"));
            Assert.ThrowsException<Package.KolliIdException>(action);
        }
        [TestMethod]
        public void Add_Valid_Package()
        {
            var p = new Package("999000000000000003", 1, 1, 1, 1);
            _service.AddPackage(p);

            var result = _service.GetAll();
            CollectionAssert.Contains(result.ToList(), p);
        }
        [TestMethod]
        public void Add_Package_With_Invalid_Measurements()
        {
            var p = new Package("999000000000000003", 100, 1, 1, 1);
            _service.AddPackage(p);

            var result = _service.GetAll();
            CollectionAssert.Contains(result.ToList(), p);
        }
        [TestMethod]
        public void Add_No_Dupes()
        {
            var p = new Package("999000000000000001", 1, 1, 1, 1);
            var action = new Action(() => _service.AddPackage(p));
            Assert.ThrowsException<PackageRepository.AlreadyExistsException>(action);


        }
    }
}