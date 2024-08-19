using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using corewcfservice.models;

namespace corewcfservice.Services
{
    [CoreWCF.ServiceContract]
    public interface IAccountHolderService
    {
        [CoreWCF.OperationContract]
        Task<List<AccountHolder>> GetAccountHoldersAsync();

        [CoreWCF.OperationContract]
        Task<AccountHolder> GetAccountHolderAsync(int id);

        [CoreWCF.OperationContract]
        Task AddAccountHolderAsync(AccountHolder accountHolder);

        [CoreWCF.OperationContract]
        Task UpdateAccountHolderAsync(AccountHolder accountHolder);

        [CoreWCF.OperationContract]
        Task DeleteAccountHolderAsync(int id);
    }
}