using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Template.Application.Handlers.AAAs;
using Template.xUnitTest.Auth;
using Template.xUnitTest.Setup;
using Xunit;
using Xunit.Abstractions;

namespace Template.xUnitTest.Tests.AAAs
{

    [Collection("SetupCollection")]
    public class AAATest : BaseTest
    {
        public AAATest(ITestOutputHelper outputHelper, SetupFixture setupfixture) : base(outputHelper, setupfixture) { }

        
        string correctNombre = "testNombre";
        string correctApellido = "testApellido";
        string correctCedula = "1.234.567-8";


        [Fact]
        public async Task Given_CorrectoAAA_When_Adding_Then_No_Error()
        {
            // Arrange
            Context context = new Context(_setupfixture.Configuration, _setupfixture.UnitLogic);
            IHttpContextAccessor _contextAccessor = await context.GetCurrentUser();
            CancellationToken cancellationToken = new CancellationToken();

            var request = new AddAAA(
                correctNombre,
                correctApellido,
                correctCedula
            );

            var AAAHandler = new AddAAAHandler(
                _contextAccessor,
                _setupfixture.Configuration,
                _setupfixture.UnitLogic,
                null // al pasarle el Mediator como null, no se ejecutan los Publish que haya en el Handler
            );


            // Act
            var response = await AAAHandler.Handle(request, cancellationToken);
            _outputHelper.WriteLine(response.Mensaje);

            // Assert
            Assert.False(response.HayError);
        }

        [Fact]
        public async Task Given_Id_Then_Get_AAA()
        {
            // Arrange
            Context context = new Context(_setupfixture.Configuration, _setupfixture.UnitLogic);
            IHttpContextAccessor _contextAccessor = await context.GetCurrentUser();
            CancellationToken cancellationToken = new CancellationToken();

            var requestAdd = new AddAAA(
             correctNombre,
             correctApellido,
             correctCedula
            );

            var addAAAHandler = new AddAAAHandler(
                _contextAccessor,
                _setupfixture.Configuration,
                _setupfixture.UnitLogic,
                null // al pasarle el Mediator como null, no se ejecutan los Publish que haya en el Handler
            );

            var response = await addAAAHandler.Handle(requestAdd, cancellationToken);

            //

            var request = new GetAAAById(
                response.Data // Id
            );

            var AAAHandler = new GetAAAByIdHandler(
                _contextAccessor,
                _setupfixture.Configuration,
                _setupfixture.UnitLogic,
                null // al pasarle el Mediator como null, no se ejecutan los Publish que haya en el Handler
            );

            // Act
            var responseFinal = await AAAHandler.Handle(request, cancellationToken);
            _outputHelper.WriteLine(responseFinal.Mensaje);

            // Assert
            Assert.False(string.IsNullOrEmpty(responseFinal.Data?.Nombre));
        }

        [Fact]
        public async Task Given_AAAs_List_Then_No_Error()
        {
            // Arrange
            Context context = new Context(_setupfixture.Configuration, _setupfixture.UnitLogic);
            IHttpContextAccessor _contextAccessor = await context.GetCurrentUser();
            CancellationToken cancellationToken = new CancellationToken();

            AddAAA AAARequest = new AddAAA(nombre : "PruebaA", apellido : "PruebaA", cedula: "12345678");

            await _setupfixture.UnitLogic.AAALogic.Add(AAARequest);
            await _setupfixture.UnitLogic.AAALogic.Add(AAARequest);
            await _setupfixture.UnitLogic.AAALogic.Add(AAARequest);
            await _setupfixture.UnitLogic.AAALogic.Add(AAARequest);
            await _setupfixture.UnitLogic.AAALogic.Add(AAARequest);

            var request = new GetAAAsAll(

            );

            var AAAHandler = new GetAAAsAllHandler(
                _contextAccessor,
                _setupfixture.Configuration,
                _setupfixture.UnitLogic,
                null // al pasarle el Mediator como null, no se ejecutan los Publish que haya en el Handler
            );

            // Act
            var response = await AAAHandler.Handle(request, cancellationToken);
            _outputHelper.WriteLine(response.Mensaje);

            // Assert
            Assert.True(response.Data?.Count > 0);
        }

        #region AddAAAValidator

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Long_Nombre_More_Than_The_Max_Of_25_Characters")]
        public void Given_NotCorrectNombre_When_Validating_Then_Error(string notCorrectNombre)
        {
            // Arrange
            var AAAValidator = new AddAAAValidator();
            var model = new AddAAA(
                notCorrectNombre,
                correctApellido,
                correctCedula
            );

            // Act
            var result = AAAValidator.TestValidate(model);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_CorrectNombre_When_Validating_Then_NoError()
        {
            // Arrange
            var AAAValidator = new AddAAAValidator();
            var model = new AddAAA(
                correctNombre,
                correctApellido,
                correctCedula
            );

            // Act
            var result = AAAValidator.TestValidate(model);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Long_Test_More_Than_25_Characters")]
        public void Given_NotCorrectApellido_When_Validating_Then_Error(string notCorrectApellido)
        {
            // Arrange
            var AAAValidator = new AddAAAValidator();
            var model = new AddAAA(
                correctNombre,
                notCorrectApellido,
                correctCedula
            );

            // Act
            var result = AAAValidator.TestValidate(model);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_CorrectApellido_When_Validating_Then_NoError()
        {
            // Arrange
            var AAAValidator = new AddAAAValidator();
            var model = new AddAAA(
                correctNombre,
                correctApellido,
                correctCedula
            );

            // Act
            var result = AAAValidator.TestValidate(model);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("1234567")]
        [InlineData("1.234.567")]
        [InlineData("1234567.")]
        [InlineData("123456789")]
        public void Given_NotCorrectCedula_When_Validating_Then_Error(string notCorrectCedula)
        {
            // Arrange
            var AAAValidator = new AddAAAValidator();
            var model = new AddAAA(
                correctNombre,
                correctApellido,
                notCorrectCedula
            );

            // Act
            var result = AAAValidator.TestValidate(model);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_CorrectCedula_When_Validating_Then_NoError()
        {
            // Arrange
            var AAAValidator = new AddAAAValidator();
            var model = new AddAAA(
                correctNombre,
                correctApellido,
                correctCedula
            );

            // Act
            var result = AAAValidator.TestValidate(model);

            // Assert
            Assert.True(result.IsValid);
        }

        #endregion
    }
}
