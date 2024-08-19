using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading.Tasks;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class AccountService : IAccountService
{
    private readonly string connectionString = "your_connection_string_here";

    public async Task<IEnumerable<Account>> GetAccounts()
    {
        var accounts = new List<Account>();

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM Account_table", connection);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    accounts.Add(new Account
                    {
                        AccId = reader.GetInt32(reader.GetOrdinal("AccId")),
                        AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber")),
                        AccountStatus_id = reader.GetInt32(reader.GetOrdinal("AccountStatus_id")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                        UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    });
                }
            }
        }

        return accounts;
    }

    public async Task<Account> GetAccountById(int id)
    {
        Account account = null;

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM Account_table WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", id);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    account = new Account
                    {
                        AccId = reader.GetInt32(reader.GetOrdinal("AccId")),
                        AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber")),
                        AccountStatus_id = reader.GetInt32(reader.GetOrdinal("AccountStatus_id")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                        UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    };
                }
            }
        }

        return account;
    }

    public async Task AddAccount(Account account)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(
                "INSERT INTO Account_table (AccId, AccountNumber, AccountStatus_id, CreatedDate, CreatedBy) " +
                "VALUES (@AccId, @AccountNumber, @AccountStatus_id, @CreatedDate, @CreatedBy)", connection);
            command.Parameters.AddWithValue("@AccId", account.AccId);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
            command.Parameters.AddWithValue("@CreatedDate", account.CreatedDate);
            command.Parameters.AddWithValue("@CreatedBy", account.CreatedBy);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task UpdateAccount(int id, Account account)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(
                "UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id, " +
                "UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", id);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
            command.Parameters.AddWithValue("@UpdatedDate", account.UpdatedDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedBy", account.UpdatedBy ?? (object)DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteAccount(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM Account_table WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}
