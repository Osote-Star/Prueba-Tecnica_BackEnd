using System.ComponentModel.DataAnnotations;

namespace Evaluacion.Models
{
    public class Producto
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public string categoria { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo.")]
        public decimal precio { get; set; }
    }
}