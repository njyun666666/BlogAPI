using BlogAPI.Models.Org;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface ILoginService
	{
		public Task<OrgAccountInfoModel> AccountInfoGet(string googleID);
		public Task<OrgAccountInfoModel> AccountInfoAdd(GoogleJsonWebSignature.Payload googleUser);
		public Task<int> LoginLogAdd(string uid, string key, string ip);
	}
}
