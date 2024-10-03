using AutoMapper.Internal;
using Demo.DAL;

namespace Demo.MVC.Services.IServices
{
	public interface IBaseServices
	{
		ApiResponse responseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest apiRequest);
	}
}
