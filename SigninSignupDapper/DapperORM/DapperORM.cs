using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace SigninSignupDapper.DapperORM
{
    public class DapperORM
    {
        private static string ConnectionString = @"Data source = USER;Initial Catalog = Claims;Integrated Security = TRUE";
        public static void ReturnsNothing(string ProcName, DynamicParameters dynamicParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(ProcName, dynamicParameters, commandType: CommandType.StoredProcedure);
            }

        }
        public static IEnumerable<T> ReturnList<T>(string ProcName, DynamicParameters dynamicParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<T>(ProcName, dynamicParameters, commandType: CommandType.StoredProcedure);
            }

        }
    }
}
