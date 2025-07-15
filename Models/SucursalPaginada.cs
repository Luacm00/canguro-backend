namespace Canguro.API.Models
{
    public class SucursalPaginada
    {
        public List<Sucursal> Sucursales { get; set; } = new();
        public int TotalRegistros { get; set; }
    }
}
