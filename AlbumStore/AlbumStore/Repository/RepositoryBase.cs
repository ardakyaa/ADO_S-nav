using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumStore.Repository
{
    public abstract class RepositoryBase
    {
        SqlConnection connection = null;

        public RepositoryBase()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        }

        public SqlConnection Connection
        {
            get
            {
                return connection;
            }

        }
    }
}
