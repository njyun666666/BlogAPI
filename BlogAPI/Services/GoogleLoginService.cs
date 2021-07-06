using BlogAPI.Common;
using BlogAPI.Services.IServices;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class GoogleLoginService : IGoogleLoginService
	{
		private readonly IMyService _myService;

		public GoogleJsonWebSignature.Payload googleUser;


		public GoogleLoginService(IMyService myService)
		{
			_myService = myService;
		}

		public async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string googleTokenId)
		{
			var settings = new GoogleJsonWebSignature.ValidationSettings
			{
				Audience = new List<string>() { _myService.Google_client_id() }
			};
			var payload = await GoogleJsonWebSignature.ValidateAsync(googleTokenId, settings);
			googleUser = payload;

			return payload;
		}

		public GoogleJsonWebSignature.Payload GetUser()
		{
			return googleUser;
		}


	}
}
