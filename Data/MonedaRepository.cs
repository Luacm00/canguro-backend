using Canguro.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Canguro.API.Data
{
    public class MonedaRepository
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        public MonedaRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Moneda>> GetAllAsync()
        {
            var list = new List<Moneda>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT Id, Nombre FROM Moneda", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Moneda
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1)
                });
            }

            return list;
        }
    }
}
