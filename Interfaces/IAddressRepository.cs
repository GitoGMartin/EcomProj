using ECommerce.API.Models;

namespace EcomProj.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetAddressByIdAsync(Guid id);
        Task<Guid> CreateAsync(Address Addy);
        Task<bool> UpdateAsync(Guid id, Address Addy);
        Task<bool> DeleteAsync(Guid id);
    }
}

