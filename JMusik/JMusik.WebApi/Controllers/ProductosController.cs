#nullable disable
using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using JMusik.Models;
using Microsoft.AspNetCore.Mvc;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IProductosRepositorio _productosRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductosRepositorio productosRepositorio,
            IMapper mapper,
            ILogger<ProductosController> logger)
        {
            _productosRepositorio = productosRepositorio;
            this._mapper = mapper;
            this._logger = logger;
        }
        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
        {
            try
            {
                var productos = await _productosRepositorio.ObtenerProductosAsync();
                return _mapper.Map<List<ProductoDTO>>(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: ${ex.Message}");
                return BadRequest();
            }
        }
        // GET: api/Productos/5
        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDTO>> Get(int ID)
        {
            var producto = await _productosRepositorio.ObtenerProductoAsync(ID);
            if (producto == null)     
                return NotFound();
            else
                return _mapper.Map<ProductoDTO>(producto);
        }
        // POST: api/Productos/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDTO>> Post(ProductoDTO productoDTO)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDTO);
                var nuevoProducto = await _productosRepositorio.Agregar(producto);
                if (producto == null)
                    return BadRequest();
                else
                {
                    var nuevoProductoDTO = _mapper.Map<ProductoDTO>(nuevoProducto);
                    return CreatedAtAction(nameof(Post), new {id=nuevoProductoDTO.Id}, nuevoProductoDTO);
                    //Si se crea el producto devuelve el ID y el objeto.
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: ${ex.Message}");

                return BadRequest();
            }
        }
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/Productos/5
        [HttpPut("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDTO>> Put(int ID, [FromBody] ProductoDTO productoDTO)
        {
            if (productoDTO == null)
                return NotFound();
            else
            {
                var producto = _mapper.Map<Producto>(productoDTO);
                var resultado = await _productosRepositorio.Actualizar(producto);
                if (!resultado)
                    return BadRequest();
                else
                    return productoDTO;
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Delete(int ID)
        {
            try
            {
                var resultado = await _productosRepositorio.Eliminar(ID); // Elimina logicamente el registro
                if (!resultado)
                    return BadRequest();
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: ${ex.Message}");
                return BadRequest();  
            }
          
        }

    }
}