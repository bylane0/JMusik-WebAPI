using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using JMusik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private IOrdenesRepositorio _ordenesRepositorio;
        private readonly IMapper _mapper;

        public OrdenesController(IOrdenesRepositorio ordenesRepositorio, ILogger<OrdenesController> logger,IMapper mapper)
        {
            this._ordenesRepositorio = ordenesRepositorio;
            this._mapper = mapper;
        }

        //// GET: api/ordenes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenDTO>>> Get()
        {
            try
            {
                var ordenes= await _ordenesRepositorio.ObtenerTodosAsync();
                return _mapper.Map<List<OrdenDTO>>(ordenes);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        //// GET: api/ordenes/detalles
        [HttpGet]
        [Route("detalles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenDTO>>> GetOrdenesConDetalle()
        {
            try
            {
                var ordenes = await _ordenesRepositorio.ObtenerTodosConDetallesAsync();
                return _mapper.Map<List<OrdenDTO>>(ordenes);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: api/ordenes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdenDTO>> Get(int id)
        {
            var orden = await _ordenesRepositorio.ObtenerAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return _mapper.Map<OrdenDTO>(orden);
        }


        // GET: api/ordenes/5/detalles
        [HttpGet("{id}/detalles")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdenDTO>> GetOrdenConDetalles(int id)
        {
            var orden = await _ordenesRepositorio.ObtenerConDetallesAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return _mapper.Map<OrdenDTO>(orden);
        }

        // POST: api/ordenes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrdenDTO>> Post(OrdenDTO OrdenDTO)
        {
            try
            {
                var orden = _mapper.Map<Orden>(OrdenDTO);

                var nuevaOrden = await _ordenesRepositorio.Agregar(orden);
                if (nuevaOrden == null)
                {
                    return BadRequest();
                }

                var nuevaOrdenDTO = _mapper.Map<OrdenDTO>(nuevaOrden);
                return CreatedAtAction(nameof(Post), new { id = nuevaOrdenDTO.Id }, nuevaOrdenDTO);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        // DELETE: api/ordenes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _ordenesRepositorio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception excepcion)
            {
                return BadRequest();
            }
        }

    }
}