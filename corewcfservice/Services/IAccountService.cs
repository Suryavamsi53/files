using System.ServiceModel;
using corewcfservice.model;
using corewcfservice;
using corewcfservice.dataaccess;
using corewcfservice.models;

namespace corewcfservice.Services{

[CoreWCF.ServiceContract]
public interface IAccountService
{
    [CoreWCF.OperationContract]
    Task<List<Account>> GetAccountsAsync();

    [CoreWCF.OperationContract]
    Task<Account> GetAccountAsync(int id);

    [CoreWCF.OperationContract]
    Task AddAccountAsync(Account account);

    [CoreWCF.OperationContract]
    Task UpdateAccountAsync(Account account);

    [CoreWCF.OperationContract]
    Task DeleteAccountAsync(int id);
}
}