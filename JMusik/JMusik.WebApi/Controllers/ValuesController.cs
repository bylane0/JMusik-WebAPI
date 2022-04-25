using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace JMusik.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ValuesController : ControllerBase
    {
        private IProductosRepositorio _productosRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;
        private readonly IConfiguration _configuration;

        public ValuesController(IProductosRepositorio productosRepositorio,
            IMapper mapper,
            ILogger<ProductosController> logger,
            IConfiguration configuration)
        {
            _productosRepositorio = productosRepositorio;
            this._mapper = mapper;
            this._logger = logger;
            this._configuration = configuration;
        }



        public async Task<ActionResult<List<ProductoDTO>>> Post(ListaPostDTO listaCampos)
        {
            string nombres = string.Empty;
            IConfigurationSection section = _configuration.GetSection("ExportCSV");
            string filepath = section.GetValue<string>("PathName");
            string filename = section.GetValue<string>("FileName");
            string filefullpath = filepath + "/" + filename;
            string delimiter = ";";

            if (listaCampos == null)
                return NotFound();
            else
            {
                var productos = await _productosRepositorio.ObtenerProductosAsync();

                bool firstTime = true;

                foreach (var item in typeof(ProductoDTO).GetProperties())
                {
                    //Console.WriteLine(item.Name);
                    foreach (var name in listaCampos.Lista)
                    {
                        if (item.Name == name)
                        {
                            if (firstTime)
                                nombres += name;
                            else
                                nombres += ";" + name;
                            firstTime = false;
                            Console.WriteLine("Esta va: " + name);
                        }
                    }
                }

                var nuevosDTOS = new List<ProductoDTO>();
                var miDTO = _mapper.Map<List<ProductoDTO>>(productos);
                foreach (var item in miDTO)
                {
                    var camposDTO = item.GetType().GetProperties();
                    var newDTO = new ProductoDTO();
                    foreach (var nameCampo in camposDTO)
                    {
                        if (nombres.Contains(nameCampo.Name))
                        {
                            var nombreCampo = nameCampo.Name;
                            PropertyInfo property = typeof(ProductoDTO).GetProperty(nombreCampo);
                            object fieldValue1 = property.GetValue(item, null);
                            property.SetValue(newDTO, fieldValue1);
                        }

                    }
                    nuevosDTOS.Add(newDTO);

                }
                try
                {
                    FileStream fs = new FileStream(@filefullpath, FileMode.Truncate, FileAccess.Write);   // 1er parametro: Path *** 2do parametro: Controlar si el fichero existe o no *** 3er parametro: Acceso 
                    StreamWriter sw = new StreamWriter(fs);
                    //primera linea
                    sw.WriteLine(nombres);
                    foreach (var item in nuevosDTOS)
                    {
                        if (item.Id != 0)
                            sw.Write(item.Id + delimiter);
                        if (!string.IsNullOrEmpty(item.Nombre))
                            sw.Write(item.Nombre + delimiter);
                        if (item.Precio != 0)
                            sw.Write(item.Precio + delimiter);
                        sw.WriteLine();
                    }
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                return nuevosDTOS;

            }
        }
    }
}



