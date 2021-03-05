using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DbContext
    {
        private readonly IDbConnection _connection;
        //public IDbTransaction Transaction { get; private set; }

        public DbContext()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            //Transaction = _connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            var cmd = _connection.CreateCommand();
            //cmd.Transaction = Transaction;
            return cmd;
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }
    }
}
