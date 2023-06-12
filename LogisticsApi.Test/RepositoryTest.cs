using LogisticsApi.DAL.Models;
using LogisticsApi.DAL.Repositories;

namespace LogisticsApi.Test
{
    [TestClass]
    public class RepositoryTest
    {
        private PackageRepository _repository;
        [TestInitialize]
        public void Init()
        {
            _repository = new PackageRepository();
        }
        [TestMethod]
        public void All_Contains_Two_Packages()
        {
            var result = _repository.All();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void GetById_Finds_Existing()
        {
            var result = _repository.GetById("999000000000000001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetById_Not_Found()
        {
            var action = new Action(() => _repository.GetById("999000000000000004"));
            Assert.ThrowsException<PackageRepository.NotFoundException>(action);
        }
        [TestMethod]
        public void Add_Valid_Package()
        {
            var p = new Package("999000000000000003", 1, 1, 1, 1);
            _repository.Add(p);

            var result = _repository.All();
            CollectionAssert.Contains(result.ToList(), p);
        }
        [TestMethod]
        public void Add_No_Dupes()
        {
            var p = new Package("999000000000000001", 1, 1, 1, 1);
            var action = new Action(() => _repository.Add(p));
            Assert.ThrowsException<PackageRepository.AlreadyExistsException>(action);


        }
    }
}