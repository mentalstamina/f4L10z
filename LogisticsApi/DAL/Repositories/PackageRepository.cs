using LogisticsApi.DAL.Models;

namespace LogisticsApi.DAL.Repositories
{
    public class PackageRepository : Repository<string ,Package>
    {
        public PackageRepository() {
            Add(new Package("999000000000000001", 10, 10, 10, 250));
            Add(new Package("999000000000000002", 100, 50, 50, 25000));
        }

    }
}
