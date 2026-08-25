using Microsoft.Data.SqlClient;
using System.Data;
using Utilities.Repo.Helpers;
using Utilities.Repo.Model;

namespace Utilities.Repo.Implementations
{
    public class Repos : IRepo
    {
        private readonly CollectionHelper collectionHelper = new();

        public List<Dictionary<string, dynamic>> GetCollection(EntityOps ops)
        {
            List<Dictionary<string, dynamic>> returnCollection = null;

            using (SqlConnection conn = new SqlConnection(ops.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ops.CommandText, conn))
            {
                cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                if (ops.Params?.Count > 0)
                {
                    cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
                }

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                returnCollection = collectionHelper.GetDictionaryCollection(reader);
            }

            return returnCollection;
        }

        public Dictionary<string, dynamic> Delete(EntityOps ops)
        {
            Dictionary<string, dynamic> returnValues = null;

            using (SqlConnection conn = new SqlConnection(ops.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ops.CommandText, conn))
            {
                cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
                cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
                conn.Open();
                cmd.ExecuteNonQuery();

                returnValues = CheckForOutputParams(cmd);
            }

            return returnValues;
        }

        private Dictionary<string, dynamic> CheckForOutputParams(SqlCommand cmd)
        {
            Dictionary<string, dynamic> returnValues = new Dictionary<string, dynamic>();

            foreach (SqlParameter item in cmd.Parameters)
            {
                string paramName = item.ParameterName.StartsWith("@") ? item.ParameterName.Substring(1) : item.ParameterName;
                if (item.Direction == ParameterDirection.Output)
                    returnValues.Add(paramName, cmd.Parameters[item.ParameterName].Value);
            }
            return returnValues;
        }

        public int Insert(EntityOps ops)
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(ops.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ops.CommandText, conn))
            {
                cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
                cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
                conn.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return id;
        }

        public void Update(EntityOps ops)
        {
            using (SqlConnection conn = new SqlConnection(ops.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ops.CommandText, conn))
            {
                cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
                cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Dictionary<string, dynamic> GetScalarValues(EntityOps ops)
        {
            Dictionary<string, dynamic> scalars = new Dictionary<string, dynamic>();
            using (SqlConnection conn = new SqlConnection(ops.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ops.CommandText, conn))
            {
                cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
                cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
                conn.Open();
                cmd.ExecuteNonQuery();

                foreach (ParamItem item in ops.Params)
                {
                    if (item.IsOutput)
                        scalars.Add(item.Name, cmd.Parameters["@" + item.Name]);
                }
            }

            return scalars;
        }
    }
}