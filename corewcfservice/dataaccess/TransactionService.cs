using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using corewcfservice.models;

namespace corewcfservice.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string connectionString = "your_connection_string_here";

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var transactions = new List<Transaction>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Transaction_Table", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        transactions.Add(new Transaction
                        {
                            TransID = reader.GetInt32(reader.GetOrdinal("TransID")),
                            AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                            TransTypeID = reader.GetInt32(reader.GetOrdinal("TransTypeID")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Transaction_type = reader.GetString(reader.GetOrdinal("Transaction_type")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                            UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                            UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                        });
                    }
                }
            }

            return transactions;
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            Transaction transaction = null;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Transaction_Table WHERE TransID = @TransID", connection);
                command.Parameters.AddWithValue("@TransID", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        transaction = new Transaction
                        {
                            TransID = reader.GetInt32(reader.GetOrdinal("TransID")),
                            AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                            TransTypeID = reader.GetInt32(reader.GetOrdinal("TransTypeID")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Transaction_type = reader.GetString(reader.GetOrdinal("Transaction_type")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                            UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                            UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                        };
                    }
                }
            }

            return transaction;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Transaction_Table (TransID, AccountID, TransTypeID, Amount, Date, Transaction_type, CreatedDate, CreatedBy) " +
                    "VALUES (@TransID, @AccountID, @TransTypeID, @Amount, @Date, @Transaction_type, @CreatedDate, @CreatedBy)", connection);
                command.Parameters.AddWithValue("@TransID", transaction.TransID);
                command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
                command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Date", transaction.Date);
                command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
                command.Parameters.AddWithValue("@CreatedDate", transaction.CreatedDate);
                command.Parameters.AddWithValue("@CreatedBy", transaction.CreatedBy);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Transaction_Table SET AccountID = @AccountID, TransTypeID = @TransTypeID, Amount = @Amount, " +
                    "Date = @Date, Transaction_type = @Transaction_type, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE TransID = @TransID", connection);
                command.Parameters.AddWithValue("@TransID", transaction.TransID);
                command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
                command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Date", transaction.Date);
                command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
                command.Parameters.AddWithValue("@UpdatedDate", transaction.UpdatedDate ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedBy", transaction.UpdatedBy ?? (object)System.DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteTransactionAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Transaction_Table WHERE TransID = @TransID", connection);
                command.Parameters.AddWithValue("@TransID", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
