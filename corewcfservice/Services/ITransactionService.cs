using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using corewcfservice.models;
namespace corewcfservice.Services
{
    [CoreWCF.ServiceContract]
    public interface ITransactionService
    {
        [CoreWCF.OperationContract]
        Task<List<Transaction>> GetTransactionsAsync();

        [CoreWCF.OperationContract]
        Task<Transaction> GetTransactionAsync(int id);

        [CoreWCF.OperationContract]
        Task AddTransactionAsync(Transaction transaction);

        [CoreWCF.OperationContract]
        Task UpdateTransactionAsync(Transaction transaction);

        [CoreWCF.OperationContract]
        Task DeleteTransactionAsync(int id);
    }
}