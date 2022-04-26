using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.WebApi.Controllers;
using JMusik.WebApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace TestProject1
{
    public class ProductosControllerTests
    {

        private static IMapper? _mapper;

        public ProductosControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new JMusikProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public void primerTestSinDB() // LABORATORIO_CONTROLLER
        {
            //Arrange
            var controller = new LaboratorioController();
            //Act
            var result = controller.Get();
            //Assert
            Assert.Equal("test", result);
        }

        [Fact]
        public void TestProductosDB()// PRODUCTOS_CONTROLLER
        {
            //Preparacion
            Mock<IProductosRepositorio> productoRepo = new Mock<IProductosRepositorio>();

            ProductosController controller = new ProductosController(productoRepo.Object, _mapper);

            //Act
            var actionResult = controller.GetThrowOkResult();
            var status = actionResult.Result.Result;
            //Validaciones
            
            //si no esta vacio
            Assert.NotNull(actionResult);

            //Si da ok
            Assert.IsType<OkObjectResult>(status);


        }


    }
}



//int count = 5;
//var dataStore = A.Fake<IProductosRepositorio>();
//var fakeProducts = A.CollectionOfDummy<ProductoDTO>(count);



//A.CallTo(() => dataStore.ObtenerProductosAsync()).Returns(Task.FromResult(fakeProducts));

//var controller = new ProductosController(dataStore);
////Act
//var result = controller.Get();
////Assert
//Assert.Equal("test", result);