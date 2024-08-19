
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using corewcfservice.models;
using corewcfservice.Services;



namespace corewcfservice.dataaccess
{
    public class LookupService : ILookupService
    {
        private readonly string _connectionString;

        public LookupService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Lookup>> GetAllLookups()
        {
            var lookups = new List<Lookup>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM lookup_table";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    lookups.Add(new Lookup
                    {
                        Lookup_id = (int)reader["Lookup_id"],
                        Lookup_type = reader["Lookup_type"] as string,
                        Lookup_desc = reader["Lookup_desc"] as string,
                        Is_active = reader["Is_active"] as string,
                        Createdby = reader["Createdby"] as string,
                        CreatedDATE = (DateTime)reader["CreatedDATE"],
                        Updatedby = reader["Updatedby"] as string,
                        UpdatedDATE = reader["UpdatedDATE"] as DateTime?,
                    });
                }
            }
            return lookups;
        }

        public async Task<Lookup> GetLookupById(int id)
        {
            Lookup lookup = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM lookup_table WHERE Lookup_id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    lookup = new Lookup
                    {
                        Lookup_id = (int)reader["Lookup_id"],
                        Lookup_type = reader["Lookup_type"] as string,
                        Lookup_desc = reader["Lookup_desc"] as string,
                        Is_active = reader["Is_active"] as string,
                        Createdby = reader["Createdby"] as string,
                        CreatedDATE = (DateTime)reader["CreatedDATE"],
                        Updatedby = reader["Updatedby"] as string,
                        UpdatedDATE = reader["UpdatedDATE"] as DateTime?,
                    };
                }
            }
            return lookup;
        }

        public async Task AddLookup(Lookup lookup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO lookup_table (Lookup_type, Lookup_desc, Is_active, Createdby, CreatedDATE, Updatedby, UpdatedDATE) " +
                            "VALUES (@Lookup_type, @Lookup_desc, @Is_active, @Createdby, @CreatedDATE, @Updatedby, @UpdatedDATE)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Lookup_type", lookup.Lookup_type);
                command.Parameters.AddWithValue("@Lookup_desc", lookup.Lookup_desc);
                command.Parameters.AddWithValue("@Is_active", lookup.Is_active);
                command.Parameters.AddWithValue("@Createdby", lookup.Createdby);
                command.Parameters.AddWithValue("@CreatedDATE", lookup.CreatedDATE);
                command.Parameters.AddWithValue("@Updatedby", lookup.Updatedby);
                command.Parameters.AddWithValue("@UpdatedDATE", lookup.UpdatedDATE.HasValue ? (object)lookup.UpdatedDATE.Value : System.DBNull.Value);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateLookup(Lookup lookup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE lookup_table SET Lookup_type = @Lookup_type, Lookup_desc = @Lookup_desc, " +
                            "Is_active = @Is_active, Createdby = @Createdby, CreatedDATE = @CreatedDATE, " +
                            "Updatedby = @Updatedby, UpdatedDATE = @UpdatedDATE WHERE Lookup_id = @Lookup_id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Lookup_id", lookup.Lookup_id);
                command.Parameters.AddWithValue("@Lookup_type", lookup.Lookup_type);
                command.Parameters.AddWithValue("@Lookup_desc", lookup.Lookup_desc);
                command.Parameters.AddWithValue("@Is_active", lookup.Is_active);
                command.Parameters.AddWithValue("@Createdby", lookup.Createdby);
                command.Parameters.AddWithValue("@CreatedDATE", lookup.CreatedDATE);
                command.Parameters.AddWithValue("@Updatedby", lookup.Updatedby);
                command.Parameters.AddWithValue("@UpdatedDATE", lookup.UpdatedDATE.HasValue ? (object)lookup.UpdatedDATE.Value : System.DBNull.Value);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteLookup(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM lookup_table WHERE Lookup_id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
