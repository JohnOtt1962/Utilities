using Utilities.Repo.Implementations;
using Utilities.Repo.Model;
using Validator = Utilities.Repo.Helpers.Validator;

namespace Utilities.Repo
{
    public class Repository : IRepo
    {
        private readonly IRepo _repo = new Repos();
        private readonly Validator _validateEntityOperation = new();

        public async Task<List<Dictionary<string, dynamic>>> GetCollection(EntityOps ops)
        {
            _validateEntityOperation.ValidateEntityOperation(ops, false);
            return await _repo.GetCollection(ops);
        }

        public async Task<Dictionary<string, dynamic>> Delete(EntityOps ops)
        {
            bool paramRequired = false;
            _validateEntityOperation.ValidateEntityOperation(ops, paramRequired);
            return await _repo.Delete(ops);
        }

        public async Task<int> Insert(EntityOps ops)
        {
            bool paramRequired = ops.IsStoredProc;
            _validateEntityOperation.ValidateEntityOperation(ops, paramRequired);
            return await _repo.Insert(ops);
        }

        public async Task Update(EntityOps ops)
        {
            bool paramRequired = ops.IsStoredProc;
            _validateEntityOperation.ValidateEntityOperation(ops, paramRequired);
            await _repo.Update(ops);
        }

        public async Task<Dictionary<string, dynamic>> GetScalarValues(EntityOps ops)
        {
            return await _repo.GetScalarValues(ops);
        }
    }
}