using Demo.API.DTOs;
using Demo.API.Helper;
using Demo.DAL;
using Demo.MVC.Services.IServices;

namespace Demo.MVC.Services
{
	public class VillaService : BaseService, IVillaService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string villaUrl;

		public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory=clientFactory;
			villaUrl = configuration.GetValue<string>("ServiceUrls:VillaApis");
		}

		public Task<T> CreateAsync<T>(VillaCreateDTO villaCreateDTO)
		{
			return SendAsync<T>(new APIRequest() {
				ApiType = SD.ApiType.POST,
				Data = villaCreateDTO,
				Url = villaUrl + "/api/Villa",
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = villaUrl + "/api/Villa/"+id,
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/Villa",
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/Villa/"+ id,
			});
		}

		public Task<T> UpdateAsync<T>(VillaUpdateDTO villaUpdateDTO)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = villaUpdateDTO,
				Url = villaUrl + "/api/Villa",
			});
		}
	}
}
