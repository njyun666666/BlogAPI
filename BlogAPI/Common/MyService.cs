using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Common
{
    public interface IMyService
    {
        public string Google_client_id();
        public string BlogAPI_Key();
    }
    public class MyService : IMyService
    {
        private IConfiguration _config;
        public MyService(IConfiguration config)
        {
            _config = config;
        }

        public string Google_client_id()
        {
            return _config["Google_client_id"];
        }
        public string BlogAPI_Key()
        {
            return _config["BlogAPI_Key"];
        }

    }
}
