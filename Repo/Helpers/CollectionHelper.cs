using Microsoft.Data.SqlClient;
using System.Data;
using Utilities.Repo.Model;

namespace Utilities.Repo.Helpers
{
    public class CollectionHelper
    {
        public List<Dictionary<string, dynamic>> GetDictionaryCollection(SqlDataReader sdr)
        {
            List<Dictionary<string, dynamic>> returnCollection = new List<Dictionary<string, dynamic>>();

            while (sdr.Read())
            {
                var row = new Dictionary<string, dynamic>();

                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    row[sdr.GetName(i)] =
                        sdr.IsDBNull(i) ? null : sdr.GetValue(i);
                }

                returnCollection.Add(row);
            }

            return returnCollection;
        }

        public SqlParameter[] GetSqlParamList(EntityOps ops)
        {
            return ops.Params.Select(CreateSqlParam).ToArray();
        }

        private SqlParameter CreateSqlParam(ParamItem item)
        {
            SqlParameter parm = new SqlParameter
            {
                ParameterName = "@" + item.Name,
                Value = item.Value
            };

            if (item.IsOutput)
            {
                parm.Direction = ParameterDirection.Output;
                parm.SqlDbType = GetSqlDataType(item.Type);
                parm.Size = item.Size;
            }

            return parm;
        }

        private SqlDbType GetSqlDataType(string type)
        {
            SqlDbType dbType = 0;

            if (type == "varchar")
                dbType = SqlDbType.VarChar;
            else if (type == "int")
                dbType = SqlDbType.Int;
            else if (type == "long")
                dbType = SqlDbType.BigInt;
            else if (type == "datetime")
                dbType = SqlDbType.DateTime;

            return dbType;
        }
    }
}