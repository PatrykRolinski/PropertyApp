using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using PropertyApp.Infrastructure;
using PropertyApp.Application.Functions.Users.Commands.RegisterUser;
using PropertyApp.Api.IntegrationTests.Helpers;
using System.Threading.Tasks;
using FluentAssertions;
using PropertyApp.Domain.Entities;
using System.Collections.Generic;

namespace PropertyApp.Api.IntegrationTests.ControllersTests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _htttpClient;
        private WebApplicationFactory<Program> _factory;
        private User _user;

        public AccountControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory= factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
            {
                var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<PropertyAppContext>));
                services.Remove(dbContextOptions);
                services.AddDbContext<PropertyAppContext>(options => options.UseInMemoryDatabase("PropertyDataBase"));
            });
            });
            _htttpClient= _factory.CreateClient();
            _user = new User() { Email = "Test@gmail.com", FirstName="SomeName", LastName="SomeLastName", VerificationToken="Token" };
            SeedUser(_user);
        }

        private void SeedUser(User user)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<PropertyAppContext>();
            dbContext.Add(user);
            dbContext.SaveChanges();
        }

        public static IEnumerable<object[]> GetInvalidRegisterCommand()
        {
            var list = new List<RegisterUserCommand>()
            {
                //eamil is already in use
                 new RegisterUserCommand() {Email="Test@gmail.com", Password="test", ConfirmPassword="test", FirstName="SomeName", LastName="SomeLastName"},
                //without LastName
                new RegisterUserCommand() { Email = "Test1@gmail.com", Password = "test", ConfirmPassword = "test", FirstName = "SomeName"},
                //without FirstName
                new RegisterUserCommand() { Email = "Test1@gmail.com", Password = "test", ConfirmPassword = "test", LastName = "SomLastName" },
                //ConfirmPassword not match
                new RegisterUserCommand() { Email = "Test1@gmail.com", Password = "test", ConfirmPassword = "test2", FirstName = "SomeName", LastName = "SomLastName" }
            };
            return list.Select(c => new object[] { c });
        }

        [Fact]
        public async Task RegisterUser_ForValidModel_ReturnsOk()
        {
            //arrange
            var registerUserCommand= new RegisterUserCommand() {Email="Test1@gmail.com", Password="test", ConfirmPassword="test", FirstName="SomeName", LastName="SomLastName"};

            //act
           var result=await _htttpClient.PostAsync("api/account/register", registerUserCommand.toJsonContent());

            //assert
            result.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [MemberData(nameof(GetInvalidRegisterCommand))]
        public async Task RegisterUser_ForInvalidModel_ReturnBadRequest(RegisterUserCommand registerUserCommand)
        {
            //act
            var result = await _htttpClient.PostAsync("api/account/register", registerUserCommand.toJsonContent());

            //assert
            result.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
