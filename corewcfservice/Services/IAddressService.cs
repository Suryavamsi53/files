using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using corewcfservice.models;
namespace corewcfservice.Services
{
    [CoreWCF.ServiceContract]
    public interface IAddressService
    {
        [OperationContract]
        Task<List<Address>> GetAddressesAsync();

        [OperationContract]
        Task<Address> GetAddressAsync(int id);

        [OperationContract]
        Task AddAddressAsync(Address address);

        [OperationContract]
        Task UpdateAddressAsync(Address address);

        [OperationContract]
        Task DeleteAddressAsync(int id);
    }
}