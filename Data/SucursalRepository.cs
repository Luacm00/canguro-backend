using Canguro.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

//Llamar los procedimientos almacenados
namespace Canguro.API.Data
{
    public class SucursalRepository
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        // Constructor de la configuración para obtener la conexión
        public SucursalRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");

            Console.WriteLine(">> Connection string cargada:");
            Console.WriteLine(_connectionString ?? "No se cargo ninguna conexión");
        }

        // Métodos para manejar las sucursales
        public async Task<SucursalPaginada> GetSucursalesAsync(int page, int pageSize)
        {
            var result = new SucursalPaginada();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetSucursales", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Page", page);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            // Primer resultset: sucursales
            while (await reader.ReadAsync())
            {
                result.Sucursales.Add(new Sucursal
                {
                    Id = reader.GetInt32(0),
                    Codigo = reader.GetInt32(1),
                    Descripcion = reader.GetString(2),
                    Direccion = reader.GetString(3),
                    Identificacion = reader.GetString(4),
                    FechaCreacion = reader.GetDateTime(5),
                    MonedaId = reader.GetInt32(6),
                    Activo = reader.GetBoolean(7)
                });
            }

            // Segundo resultset: total
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.TotalRegistros = reader.GetInt32(0);
            }

            return result;
        }


        // Método para agregar una sucursal
        public async Task InsertSucursalAsync(Sucursal s)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertSucursal", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Codigo", s.Codigo);
            cmd.Parameters.AddWithValue("@Descripcion", s.Descripcion);
            cmd.Parameters.AddWithValue("@Direccion", s.Direccion);
            cmd.Parameters.AddWithValue("@Identificacion", s.Identificacion);
            cmd.Parameters.AddWithValue("@FechaCreacion", s.FechaCreacion);
            cmd.Parameters.AddWithValue("@MonedaId", s.MonedaId);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        // Método para actualizar una sucursal
        public async Task UpdateSucursalAsync(Sucursal s)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_UpdateSucursal", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", s.Id);
            cmd.Parameters.AddWithValue("@Descripcion", s.Descripcion);
            cmd.Parameters.AddWithValue("@Direccion", s.Direccion);
            cmd.Parameters.AddWithValue("@Identificacion", s.Identificacion);
            cmd.Parameters.AddWithValue("@MonedaId", s.MonedaId);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        // Método para eliminar una sucursal
        public async Task DeleteSucursalAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_DeleteSucursal", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
