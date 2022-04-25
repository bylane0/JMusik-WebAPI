using JMusik.Models.Enum;
namespace JMusik.Models
{
    public class Producto
    {
        public Producto()
        {
            DetallesOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public EstatusProducto Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<DetalleOrden> DetallesOrden { get; set; }
    }
}
