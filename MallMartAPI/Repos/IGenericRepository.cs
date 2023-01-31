namespace MallMartAPI.Repos
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Delete(object id);
        void Dispose();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        void Save();
        Task<bool> Update(object id, T obj);
    }
}