using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading.Tasks;
using corewcfservice.Services;
namespace corewcfservice.dataaccess{
[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class AccountHolderService : IAccountHolderService
{
    private readonly string connectionString = "your_connection_string_here";

    public async Task<IEnumerable<AccountHolder>> GetAccountHolders()
    {
        var accountHolders = new List<AccountHolder>();

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM Account_holder_table", connection);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    accountHolders.Add(new AccountHolder
                    {
                        AccHID = reader.GetInt32(reader.GetOrdinal("AccHID")),
                        AccNUM = reader.GetString(reader.GetOrdinal("AccNUM")),
                        AccTypeID = reader.GetInt32(reader.GetOrdinal("AccTypeID")),
                        Acc_holders_N = reader.GetString(reader.GetOrdinal("Acc_holders_N")),
                        AC_Balance = reader.GetDecimal(reader.GetOrdinal("AC_Balance")),
                        CD = reader.GetDateTime(reader.GetOrdinal("CD")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                        UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    });
                }
            }
        }

        return accountHolders;
    }

    public async Task<AccountHolder> GetAccountHolderById(int id)
    {
        AccountHolder accountHolder = null;

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM Account_holder_table WHERE AccHID = @AccHID", connection);
            command.Parameters.AddWithValue("@AccHID", id);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    accountHolder = new AccountHolder
                    {
                        AccHID = reader.GetInt32(reader.GetOrdinal("AccHID")),
                        AccNUM = reader.GetString(reader.GetOrdinal("AccNUM")),
                        AccTypeID = reader.GetInt32(reader.GetOrdinal("AccTypeID")),
                        Acc_holders_N = reader.GetString(reader.GetOrdinal("Acc_holders_N")),
                        AC_Balance = reader.GetDecimal(reader.GetOrdinal("AC_Balance")),
                        CD = reader.GetDateTime(reader.GetOrdinal("CD")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                        UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    };
                }
            }
        }

        return accountHolder;
    }

    public async Task AddAccountHolder(AccountHolder accountHolder)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(
                "INSERT INTO Account_holder_table (AccHID, AccNUM, AccTypeID, Acc_holders_N, AC_Balance, CD, CreatedDate, CreatedBy) " +
                "VALUES (@AccHID, @AccNUM, @AccTypeID, @Acc_holders_N, @AC_Balance, @CD, @CreatedDate, @CreatedBy)", connection);
            command.Parameters.AddWithValue("@AccHID", accountHolder.AccHID);
            command.Parameters.AddWithValue("@AccNUM", accountHolder.AccNUM);
            command.Parameters.AddWithValue("@AccTypeID", accountHolder.AccTypeID);
            command.Parameters.AddWithValue("@Acc_holders_N", accountHolder.Acc_holders_N);
            command.Parameters.AddWithValue("@AC_Balance", accountHolder.AC_Balance);
            command.Parameters.AddWithValue("@CD", accountHolder.CD);
            command.Parameters.AddWithValue("@CreatedDate", accountHolder.CreatedDate);
            command.Parameters.AddWithValue("@CreatedBy", accountHolder.CreatedBy);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task UpdateAccountHolder(int id, AccountHolder accountHolder)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(
                "UPDATE Account_holder_table SET AccNUM = @AccNUM, AccTypeID = @AccTypeID, Acc_holders_N = @Acc_holders_N, " +
                "AC_Balance = @AC_Balance, CD = @CD, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE AccHID = @AccHID", connection);
            command.Parameters.AddWithValue("@AccHID", id);
            command.Parameters.AddWithValue("@AccNUM", accountHolder.AccNUM);
            command.Parameters.AddWithValue("@AccTypeID", accountHolder.AccTypeID);
            command.Parameters.AddWithValue("@Acc_holders_N", accountHolder.Acc_holders_N);
            command.Parameters.AddWithValue("@AC_Balance", accountHolder.AC_Balance);
            command.Parameters.AddWithValue("@CD", accountHolder.CD);
            command.Parameters.AddWithValue("@UpdatedDate", accountHolder.UpdatedDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedBy", accountHolder.UpdatedBy ?? (object)DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteAccountHolder(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM Account_holder_table WHERE AccHID = @AccHID", connection);
            command.Parameters.AddWithValue("@AccHID", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}
}