using LogisticsApi.DAL.Models;
using LogisticsApi.DAL.Repositories;
using System.Text.RegularExpressions;

namespace LogisticsApi.Services
{
    public interface IPackageService
    {
        IEnumerable<Package> GetAll();
        Package GetPackage(string id);
        public void AddPackage(Package package);
    }
    public class PackageService : IPackageService
    {
        private PackageRepository packageRepository;
        public PackageService(PackageRepository repository)
        {
            packageRepository = repository;
        }

        public IEnumerable<Package> GetAll()
        {
            return packageRepository.All();
        }

        public Package GetPackage(string id)
        {
            if (!Regex.IsMatch(id, Package.Limitations.KolliIdRegexPattern))
            {
                throw new Package.KolliIdException();
            }
            var package = packageRepository.GetById(id);
            if (package is not null)
            {
                return package;
            }
            else
            {
                throw new PackageRepository.NotFoundException(id);
            }
        }

        public void AddPackage(Package package)
        {
            packageRepository.Add(package);
        }
    }
}
