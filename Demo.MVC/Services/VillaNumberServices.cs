using Demo.API.DTOs;
using Demo.API.Helper;
using Demo.DAL;
using Demo.MVC.Services.IServices;

namespace Demo.MVC.Services
{
    public class VillaNumberServices : BaseService, IVillaNumberSevices
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;


        public VillaNumberServices(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory=clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaApis");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO villaNumberCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = villaNumberCreateDTO,
                Url = villaUrl + "/api/VillaNumber",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaNumber/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumber",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumber/"+ id,
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaNumberUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = villaNumberUpdateDTO,
                Url = villaUrl + "/api/VillaNumber",
            });
        }
    }
}
