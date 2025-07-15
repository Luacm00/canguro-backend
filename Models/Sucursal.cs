using Canguro.API.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Canguro.API.Models
{
    // Clase Sucursal
    public class Sucursal
    {
        public int Id { get; set; }
        
        [Required]
        public int Codigo { get; set; }

        [Required, MaxLength(250)]
        public string Descripcion { get; set; } = null!;

        [Required, MaxLength(250)]
        public string Direccion { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Identificacion { get; set; } = null!;

        [Required]
        [CustomFechaCreacion(ErrorMessage = "La fecha no puede ser anterior a hoy.")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public int MonedaId { get; set; }
        public bool Activo { get; set; } = true;
    }
}
