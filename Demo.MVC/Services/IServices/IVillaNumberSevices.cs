using Demo.API.DTOs;

namespace Demo.MVC.Services.IServices
{
    public interface IVillaNumberSevices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNumberCreateDTO villaNumberCreateDTO);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaNumberUpdateDTO);
        Task<T> DeleteAsync<T>(int id);
    }
}
