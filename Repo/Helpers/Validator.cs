using Utilities.Repo.Model;

namespace Utilities.Repo.Helpers
{
    public class Validator
    {
        public void ValidateEntityOperation(EntityOps entityOps, Boolean paramRequired)
        {
            ValidateCommand(entityOps);

            if (paramRequired)
                ValidateRequiredParam(entityOps);

            if (entityOps.Params != null && entityOps.Params.Count > 0)
                ValidateParams(entityOps);
        }

        private void ValidateCommand(EntityOps entityOps)
        {
            if (entityOps == null || string.IsNullOrEmpty(entityOps.CommandText) || string.IsNullOrEmpty(entityOps.ConnectionString))
            {
                throw new Exception("Entity Ops CommandText or ConnectionString is empty");
            }
        }

        private void ValidateRequiredParam(EntityOps ops)
        {
            if (ops.Params == null || ops.Params.Count == 0)
                throw new Exception("Update op must have at least 1 param");

            bool hasInputParam = false;
            foreach (ParamItem parm in ops.Params)
            {
                if (!parm.IsOutput)
                {
                    hasInputParam = true;
                    break;
                }
            }

            if (!hasInputParam)
                throw new Exception("Operation requires input parameters");
        }

        private void ValidateParams(EntityOps ops)
        {
            for (int i = 0; i < ops.Params.Count; i++)
            {

                if (string.IsNullOrEmpty(ops.Params[i].Name))
                    throw new Exception("Param Name at index " + Convert.ToString(i) + " is empty");

                if (string.IsNullOrEmpty(ops.Params[i].Value) && ops.Params[i].IsRequired)
                    throw new Exception("Param Value at index " + Convert.ToString(i) + " is required");

                if (ops.Params[i].IsOutput && string.IsNullOrEmpty(ops.Params[i].Type))
                    throw new Exception("Output Param at index " + Convert.ToString(i) + " requires a type");

            }

        }
    }
}