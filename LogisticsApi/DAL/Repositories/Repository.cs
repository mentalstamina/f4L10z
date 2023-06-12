using System.Collections.Concurrent;

namespace LogisticsApi.DAL.Repositories
{
    public interface IRepositoryEntity<TKey>
    {
        TKey Id { get; }
    }
    public interface IRepository<TKey, TValue> where TKey : notnull where TValue : IRepositoryEntity<TKey>
    {
        IEnumerable<TValue> All();
        TValue? GetById(TKey id);
        void Add(TValue entity);
    }
    public class Repository<TKey, TValue> : IRepository<TKey, TValue> where TKey : notnull where TValue : IRepositoryEntity<TKey>
    {
        public Repository()
        {
            data = new ConcurrentDictionary<TKey, TValue>();
        }
        private readonly ConcurrentDictionary<TKey, TValue> data;
        public void Add(TValue entity)
        {
            if (!data.TryAdd(entity.Id, entity))
            {
                throw new AlreadyExistsException(entity.Id);
            }
        }

        public IEnumerable<TValue> All()
        {
            return data.Values;
        }

        public TValue? GetById(TKey id)
        {
            if (!data.TryGetValue(id, out var entity))
            {
                throw new NotFoundException(id);
            }
            return entity;
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(TKey id) : base($"{typeof(TValue).Name} not found, id: {id}") { }
        }
        public class AlreadyExistsException : Exception
        {
            public AlreadyExistsException(TKey id) : base($"{typeof(TValue).Name} already exists, id: {id}") { }
        }
    }
}
