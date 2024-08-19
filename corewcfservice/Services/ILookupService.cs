using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using corewcfservice.models;

namespace corewcfservice.Services
{
    [ServiceContract]
    public interface ILookupService
    {
        [OperationContract]
        Task<IEnumerable<Lookup>> GetAllLookups();

        [OperationContract]
        Task<Lookup> GetLookupById(int id);

        [OperationContract]
        Task AddLookup(Lookup lookup);

        [OperationContract]
        Task UpdateLookup(Lookup lookup);

        [OperationContract]
        Task DeleteLookup(int id);
    }
}
