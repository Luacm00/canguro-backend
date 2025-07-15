using Canguro.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Canguro.API.Data
{
    public class UserRespository
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        public UserRespository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ValidarUsuario", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Role = reader.GetString(2),
                    Activo = reader.GetBoolean(3)
                };
            }

            return null;
        }
    }
}
