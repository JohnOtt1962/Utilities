using Utilities.Repo.Model;

namespace Utilities.Repo
{
    public interface IRepo
    {
        Task<List<Dictionary<string, dynamic>>> GetCollection(EntityOps ops);
        //Task<string> GetCollectionJson(EntityOps ops);
        Task<Dictionary<string, dynamic>> Delete(EntityOps ops);
        Task<int> Insert(EntityOps ops);
        Task Update(EntityOps ops);
        Task<Dictionary<string, dynamic>> GetScalarValues(EntityOps ops);
    }
}