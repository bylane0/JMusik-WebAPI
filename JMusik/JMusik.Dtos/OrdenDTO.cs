using JMusik.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace JMusik.Dtos
{
    public class OrdenDTO
    {
        public OrdenDTO()
        {
            DetallesOrden = new List<DetalleOrdenDTO>();
        }

        public int Id { get; set; }
        public decimal CantidadArticulos { get; set; }
        public decimal Importe { get; set; }
        //[Required]
        public DateTime? FechaRegistro { get; set; }
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        //public EstatusOrden EstatusOrden { get; set; } * se establecen desde el sv
        public List<DetalleOrdenDTO> DetallesOrden { get; set; }
    }

}
