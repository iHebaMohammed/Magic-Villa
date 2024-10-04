using Demo.API.DTOs;
using Demo.API.Helper;
using Demo.DAL;
using Demo.MVC.Services.IServices;

namespace Demo.MVC.Services
{
    public class AuthServices : BaseService, IAuthServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private string authUrl;


        public AuthServices(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory=clientFactory;
            authUrl = configuration.GetValue<string>("ServiceUrls:VillaApis");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDTO,
                Url = authUrl + "/api/Users/login",
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO)
        {
            return SendAsync<T>(new APIRequest() 
            {
                ApiType= SD.ApiType.POST,
                Data = registerationRequestDTO,
                Url = authUrl + "/api/Users/register",
            });
        }
    }
}
