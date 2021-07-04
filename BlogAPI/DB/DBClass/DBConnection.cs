using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.DBClass
{
    public interface IDBConnection
    {
        public string Connection(string dbName);
        public string ConnectionBlogDB();
    }
    public class DBConnection : IDBConnection
    {
        private IConfiguration _config;
        public DBConnection(IConfiguration config)
        {
            _config = config;

        }

        public string Connection(string dbName)
        {
            return _config["DBSetting:" + dbName];
        }

        public string ConnectionBlogDB()
        {
            return _config["DBSetting:BlogDB"];
        }

    }
}
