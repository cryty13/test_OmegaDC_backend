using System;
using System.Data;
using System.Data.SqlClient;

namespace OmegaDC.Infra.StoreContext.DataContexts
{
    public class OmegaDcDataContext : IDisposable
    {
        public static string ConnectionString = @"Server=localhost;Database=Banco-wb;Trusted_Connection=True;";
        
        public SqlConnection Connection { get; set; }

        public OmegaDcDataContext()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
