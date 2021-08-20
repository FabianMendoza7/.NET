using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Payments.Controllers;
using Payments.Test.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Payments.Test.PruebasUnitarias
{
    // Ejemplo de pruebas usando un mock.
    // En este caso se usa al probar RootController, el cual tiene una dependencia: IAuthorizationService,
    // ya que necesitamos que autorice siempre exitosamente para la prueba.
    // Tambien se usa el mock de dependencia URLHelperMock, ya que el controlador RootController tambien tiene 
    // la dependencia IUrlHelper.
    [TestClass]
    public class RootControllerTest
    {
        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos4Links()
        {
            // Preparación.
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Success();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            // Ejecución.
            var resultado = await rootController.Get();

            // Verificación.
            Assert.AreEqual(4, resultado.Value.Count());
        }

        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos2Links()
        {
            // Preparación.
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Failed();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            // Ejecución.
            var resultado = await rootController.Get();

            // Verificación.
            Assert.AreEqual(2, resultado.Value.Count());
        }

        // Este test utiliza la librería Moq con el fin de evitar la creación de clases Mock.
        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos2Links_UsandoMoq()
        {
            // Preparación.
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            mockAuthorizationService.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<IEnumerable<IAuthorizationRequirement>>()
                )).Returns(Task.FromResult(AuthorizationResult.Failed()));

            mockAuthorizationService.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<string>()
                )).Returns(Task.FromResult(AuthorizationResult.Failed()));

            var mockURLHelper = new Mock<IUrlHelper>();
            mockURLHelper.Setup(x=> x.Link(
                It.IsAny<string>(),
                It.IsAny<object>()
                )).Returns(string.Empty);

            var rootController = new RootController(mockAuthorizationService.Object);
            rootController.Url = mockURLHelper.Object;

            // Ejecución.
            var resultado = await rootController.Get();

            // Verificación.
            Assert.AreEqual(2, resultado.Value.Count());
        }
    }
}
