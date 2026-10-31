using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using Utilities.Repo.Helpers;
using Utilities.Repo.Model;

namespace Utilities.Repo.Implementations
{
    public class Repos : IRepo
    {
        private readonly CollectionHelper collectionHelper = new();

        public async Task<List<Dictionary<string, dynamic>>> GetCollection(EntityOps ops)
        {
            List<Dictionary<string, dynamic>> returnCollection = null;

            await using SqlConnection conn = new SqlConnection(ops.ConnectionString);
            await using SqlCommand cmd = new SqlCommand(ops.CommandText, conn);
            cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;

            if (ops.Params?.Count > 0)
            {
                cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
            }

            await conn.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            returnCollection = collectionHelper.GetDictionaryCollection(reader);

            return returnCollection;
        }

        public async Task<string> GetCollectionJson(EntityOps ops)
        {
            var jsonResult = new StringBuilder();

            await using SqlConnection conn = new SqlConnection(ops.ConnectionString);
            await using SqlCommand cmd = new SqlCommand(ops.CommandText, conn);
            cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;

            if (ops.Params?.Count > 0)
            {
                cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
            }

            await conn.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                jsonResult.Append("[]");
            }

            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }

            return jsonResult.ToString();
        }

        public async Task<Dictionary<string, dynamic>> Delete(EntityOps ops)
        {
            Dictionary<string, dynamic> returnValues = null;

            await using SqlConnection conn = new SqlConnection(ops.ConnectionString);
            await using SqlCommand cmd = new SqlCommand(ops.CommandText, conn);
            cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
            cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            returnValues = CheckForOutputParams(cmd);

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

        public async Task<int> Insert(EntityOps ops)
        {
            await using SqlConnection conn = new SqlConnection(ops.ConnectionString);
            await using SqlCommand cmd = new SqlCommand(ops.CommandText, conn);
            cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
            cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));
            
            await conn.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task Update(EntityOps ops)
        {
            await using SqlConnection conn = new SqlConnection(ops.ConnectionString);
            await using SqlCommand cmd = new SqlCommand(ops.CommandText, conn);
            cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
            cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<Dictionary<string, dynamic>> GetScalarValues(EntityOps ops)
        {
            Dictionary<string, dynamic> scalars = new Dictionary<string, dynamic>();
            await using SqlConnection conn = new SqlConnection(ops.ConnectionString);
            await using SqlCommand cmd = new SqlCommand(ops.CommandText, conn);
            cmd.CommandType = ops.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
            cmd.Parameters.AddRange(collectionHelper.GetSqlParamList(ops));

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            foreach (ParamItem item in ops.Params)
            {
                if (item.IsOutput)
                    scalars.Add(item.Name, cmd.Parameters["@" + item.Name]);
            }

            return scalars;
        }
    }
}