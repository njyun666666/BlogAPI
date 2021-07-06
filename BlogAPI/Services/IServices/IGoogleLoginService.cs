using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface IGoogleLoginService
	{
		public Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string googleTokenId);
		public GoogleJsonWebSignature.Payload GetUser();
	}
}
