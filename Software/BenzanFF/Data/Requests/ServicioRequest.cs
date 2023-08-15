using System.ComponentModel.DataAnnotations;

namespace BenzanFF.Data.Requests;

public class ServicioRequest
{
    [Required(ErrorMessage ="La descripción es obligatoria.")]
    public string Descripcion { get; set; } = null!;
    [
        Required(ErrorMessage ="Ingrese un precio para el servicio."), 
        Range(0,(double)decimal.MaxValue, ErrorMessage = $"El precio debe ser mayor o igual a 0 (Zero).")
    ]
    public decimal Precio { get; set; }
    [
        Range(1, int.MaxValue, ErrorMessage = $"Debe seleccionar una categoría.")
    ]
    public int IdCategoria { get; set; }
    public ImagenRequest imagen { get; set; } = new ImagenRequest();
}