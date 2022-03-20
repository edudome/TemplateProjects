using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Template.Application.Handlers.AAAs.Empty;
using Template.xUnitTest.Auth;
using Template.xUnitTest.Setup;
using Xunit;
using Xunit.Abstractions;

namespace Template.xUnitTest.Tests.AAAs.Empty
{
    [Collection("SetupCollection")]
    public class EmptyTest : BaseTest
    {
        public EmptyTest(ITestOutputHelper outputHelper, SetupFixture setupfixture) : base(outputHelper, setupfixture) { }


        string correctValue = "example";


        [Fact]
        public async Task Given_CorrectX_When_Do_Then_No_Error()
        {
            // Arrange
            Context context = new Context(_setupfixture.Configuration, _setupfixture.UnitLogic);
            IHttpContextAccessor _contextAccessor = await context.GetCurrentUser();
            CancellationToken cancellationToken = new CancellationToken();

            var request = new XCommand(
                correctValue
            );

            var emptyHandler = new XCommandHandler(
                _contextAccessor,
                _setupfixture.Configuration,
                _setupfixture.UnitLogic,
                null // al pasarle el Mediator como null, no se ejecutan los Publish que haya en el Handler
            );


            // Act
            var response = await emptyHandler.Handle(request, cancellationToken);
            _outputHelper.WriteLine(response.Mensaje);


            // Assert
            Assert.False(response.HayError);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Long_Value_More_Than_The_Max_Of_25_Characters")]
        public void Given_NotCorrectValue_When_Validating_Then_Error(string notCorrectValue)
        {
            // Arrange
            var AAAValidator = new XCommandValidator();
            var model = new XCommand(
                notCorrectValue
            );


            // Act
            var result = AAAValidator.TestValidate(model);


            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_CorrectValue_When_Validating_Then_NoError()
        {
            // Arrange
            var XValidator = new XCommandValidator();
            var model = new XCommand(
                correctValue
            );


            // Act
            var result = XValidator.TestValidate(model);


            // Assert
            Assert.True(result.IsValid);
        }
    }
}
