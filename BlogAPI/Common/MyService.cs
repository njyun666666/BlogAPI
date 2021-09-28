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
        public Int64 FileUploadSizeLimit_byte();
        public string FileUploadPath();
        public string FileSite();
        public string AppsettingName();
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
        public Int64 FileUploadSizeLimit_byte()
		{
            return Convert.ToInt64(_config["FileUploadSizeLimit_MB"]) * 1024 * 1024;
        }
        public string FileUploadPath()
		{
            return _config["FileUploadPath"];
		}
        public string FileSite()
		{
            return _config["FileSite"];
        }
        public string AppsettingName()
		{
            return _config["AppsettingName"];
		}
    }
}
