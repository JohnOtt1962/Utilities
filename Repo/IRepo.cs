using Utilities.Repo.Model;

namespace Utilities.Repo
{
    public interface IRepo
    {
        List<Dictionary<string, dynamic>> GetCollection(EntityOps ops);
        Dictionary<string, dynamic> Delete(EntityOps ops);
        int Insert(EntityOps ops);
        void Update(EntityOps ops);
        Dictionary<string, dynamic> GetScalarValues(EntityOps ops);
    }
}