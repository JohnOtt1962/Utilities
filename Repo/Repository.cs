using Microsoft.Extensions.Logging;
using Utilities.Repo.Implementations;
using Utilities.Repo.Model;
using Validator = Utilities.Repo.Helpers.Validator;
//using Utilities.Logger;

namespace Utilities.Repo
{
    public class Repository : IRepo
    {
        private readonly IRepo _repo = new Repos();
        private readonly Validator _validateEntityOperation = new();

        public List<Dictionary<string, dynamic>> GetCollection(EntityOps ops)
        {
            _validateEntityOperation.ValidateEntityOperation(ops, false);
            List<Dictionary<string, dynamic>> result = null;

            try
            {
                result = _repo.GetCollection(ops);
            }
            catch (Exception ex)
            {
                //No action at this time
                //_logger.Error("Failed to retrieve record set.", ex, this.GetType().FullName);
                throw;
            }

            return result;
        }

        public Dictionary<string, dynamic> Delete(EntityOps ops)
        {
            Dictionary<string, dynamic> returnValues = null;
            _validateEntityOperation.ValidateEntityOperation(ops, true);

            try
            {
                returnValues = _repo.Delete(ops);
            }
            catch (Exception ex)
            {
                //No action at this time
                //_logger.Error("Failed to delete record.", ex, this.GetType().FullName);
                throw;
            }

            return returnValues;
        }

        public int Insert(EntityOps ops)
        {
            _validateEntityOperation.ValidateEntityOperation(ops, true);

            int id = -1;

            try
            {
                id = _repo.Insert(ops);
            }
            catch (Exception ex)
            {
                //no action at this time
                //_logger.Error("Failed to insert row.", ex, this.GetType().FullName);
                throw;
            }

            return id;
        }

        public void Update(EntityOps ops)
        {
            _validateEntityOperation.ValidateEntityOperation(ops, true);

            try
            {
                _repo.Update(ops);
            }
            catch (Exception ex)
            {
                //no action at this time
                //_logger.Error("Failed to update row.", ex, this.GetType().FullName);
                throw;
            }
        }

        public Dictionary<string, dynamic> GetScalarValues(EntityOps ops)
        {
            Dictionary<string, dynamic> scalars = null;

            try
            {
                scalars = _repo.GetScalarValues(ops);
            }
            catch (Exception ex)
            {
                //no action at this time
                //_logger.Error("Failed to get scalar values.", ex, this.GetType().FullName);
                throw;
            }

            return scalars;
        }
    }
}