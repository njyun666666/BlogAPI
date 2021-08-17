using BlogAPI.Common;
using BlogAPI.Filters;
using BlogAPI.Models.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class FileController : ControllerBase
	{
        IWebHostEnvironment _env;
        IMyService _myService;
        public FileController(IWebHostEnvironment env, IMyService myService)
		{
            _env = env;
            _myService = myService;
        }
        [HttpPost]
        [TypeFilter(typeof(LoginFilter))]
        public async Task<IActionResult> Upload([FromHeader] string UID, [FromForm] FileUploadModel file)
        {
            FileUploadViewModel result = new FileUploadViewModel();

            long sizeLimit = _myService.FileUploadSizeLimit_byte();
            string uploadPath = _myService.FileUploadPath();
            string fileSite = _myService.FileSite();


            string serverDirPath = $@"{uploadPath}/{UID}";


            if (!Directory.Exists(serverDirPath))
			{
                Directory.CreateDirectory(serverDirPath);
			}

            foreach (var formFile in file.Files)
            {
                if (formFile.Length > 0)
                {

                    if (formFile.Length <= sizeLimit)
                    {
                        string nFileName = Path.GetRandomFileName().Replace(".", "");
                        string ext = Path.GetExtension(formFile.FileName);
                        nFileName = nFileName + ext;


                        string relativePath = $@"/{UID}/{nFileName}";
                        string serverFilePath = $@"{uploadPath}/{relativePath}";


                        using (var stream = System.IO.File.Create(serverFilePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        result.data.succMap.Add(formFile.FileName, $"{fileSite}{relativePath}");

                    }
					else
					{
                        result.data.errFiles.Add(formFile.FileName);
                    }

                }
            }

            return Ok(result);
        }

	}
}
