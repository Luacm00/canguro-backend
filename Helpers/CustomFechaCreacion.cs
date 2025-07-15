using System.ComponentModel.DataAnnotations;

namespace Canguro.API.Helpers
{
    public class CustomFechaCreacionAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime fecha)
            {
                return fecha >= DateTime.Today;
            }
            return false;
        }
    }
}
