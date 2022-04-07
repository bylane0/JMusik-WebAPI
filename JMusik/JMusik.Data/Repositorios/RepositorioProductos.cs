using JMusik.Data.Contratos;
using JMusik.Models;
using JMusik.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JMusik.Data.Repositorios
{

    public class ProductosRepositorio : IProductosRepositorio
    {
        private readonly TiendaDbContext _contexto;
        private readonly ILogger<ProductosRepositorio> _logger;

        public ProductosRepositorio(TiendaDbContext contexto,
            ILogger<ProductosRepositorio> logger)
        {
            _contexto = contexto;
            this._logger = logger;
        }
        public async Task<bool> Actualizar(Producto producto)
        {
            var productoBd = await ObtenerProductoAsync(producto.Id);
            productoBd.Nombre = producto.Nombre;
            producto.Precio = producto.Precio;

            //_contexto.Productos.Attach(producto);
            //_contexto.Entry(producto).State = EntityState.Modified;
            try
            {
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Actualizar)}: {ex.Message}");
            }
            return false;
        }

        public async Task<Producto> Agregar(Producto producto)
        {
            producto.Estatus = EstatusProducto.Activo;
            producto.FechaRegistro = DateTime.Now;
            _contexto.Productos.Add(producto);
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: {ex.Message}");
            }

            return producto;
        }

        public async Task<bool> Eliminar(int id)
        {
            //Se realiza una eliminación suave, solamente inactivamos el producto

            var producto = await _contexto.Productos
                                .SingleOrDefaultAsync(c => c.Id == id);
            if(producto != null)
            {
                producto.Estatus = EstatusProducto.Inactivo;
                _contexto.Productos.Attach(producto);
                _contexto.Entry(producto).State = EntityState.Modified;
            }
            try
            {
                return (await _contexto.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: {ex.Message}");
            }
            return false;

        }

        public async Task<Producto> ObtenerProductoAsync(int id)
        {
                return await _contexto.Productos
                    .SingleOrDefaultAsync(c => c.Id == id && c.Estatus == EstatusProducto.Activo);

        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _contexto.Productos.Where(u=>u.Estatus == EstatusProducto.Activo)
                .OrderBy(u => u.Nombre)
                                            .ToListAsync();
        }


    }

}
