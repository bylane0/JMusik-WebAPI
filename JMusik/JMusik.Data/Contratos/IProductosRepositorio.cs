using JMusik.Models;

namespace JMusik.Data.Contratos
{

    public interface IProductosRepositorio
    {
        Task<List<Producto>> ObtenerProductosAsync();
        Task<Producto> ObtenerProductoAsync(int id);
        Task<Producto> Agregar(Producto producto);
        Task<bool> Actualizar(Producto producto);
        Task<bool> Eliminar(int id);
        Task<(int totalRegistros, IEnumerable<Producto> registros)> ObtenerPaginasProductosAsync(int paginaActual, int registrosPorPagina);
    }

}
