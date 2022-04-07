﻿using JMusik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Contratos
{

    public interface IProductosRepositorio
    {
        Task<List<Producto>> ObtenerProductosAsync();
        Task<Producto> ObtenerProductoAsync(int id);
        Task<Producto> Agregar(Producto producto);
        Task<bool> Actualizar(Producto producto);
        Task<bool> Eliminar(int id);
    }

}
