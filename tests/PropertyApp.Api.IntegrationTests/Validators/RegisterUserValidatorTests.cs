using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Functions.Users.Commands.RegisterUser;
using PropertyApp.Domain.Entities;
using PropertyApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PropertyApp.Api.IntegrationTests.Validators
{
    public class RegisterUserValidatorTests
    {
        private PropertyAppContext _propertyAppContext;
        private Mock<IUserRepository> _userRepositoryMock= new Mock<IUserRepository>();

        public RegisterUserValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<PropertyAppContext>();
            builder.UseInMemoryDatabase("TestDb");
            _propertyAppContext = new PropertyAppContext(builder.Options);
            SeedUsers();


        }
        public void SeedUsers()
        {
            var users = new List<User>()
            {
                new User
                {
                    Email="test@gmail.com",
                    FirstName = "Janek",
                    LastName = "Smith",
                    VerificationToken="token"
                },
                new User
                {
                    Email="test1@gmail.com",
                    FirstName = "John",
                    LastName = "Nemo",
                    VerificationToken="token"
                }
            };
            _propertyAppContext.Users.AddRange(users);
            _propertyAppContext.SaveChanges();
        }

        [Fact]
        public async Task Validate_ForEmailInUse_ReturnsFailure()
        {
            var userAlreadyRegistered = _propertyAppContext.Users.First();
          _userRepositoryMock.Setup(u => u.FindyByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(_propertyAppContext.Users.FirstOrDefault(u => u.Email == userAlreadyRegistered.Email)));

            var registerUserCommand = new RegisterUserCommand()
            {
                Email = userAlreadyRegistered.Email,
                FirstName = "Janek",
                LastName = "Piotr",
                Password = "1234",
                ConfirmPassword = "1234"
            };

            var registerUserValidator = new RegisterUserValidator(_userRepositoryMock.Object);
            var result =await registerUserValidator.TestValidateAsync(registerUserCommand);

            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task Validate_ForCorrectInput_ReturnsSuccess()
        {
            var email = "test123@gmail.com";
            _userRepositoryMock.Setup(u => u.FindyByEmail(It.IsAny<string>()))
                  .Returns(Task.FromResult(_propertyAppContext.Users.FirstOrDefault(u => u.Email == email)));

            var registerUserCommand = new RegisterUserCommand()
            {
                Email =email,
                FirstName = "Janek",
                LastName = "Piotr",
                Password = "1234",
                ConfirmPassword = "1234"
            };

            var registerUserValidator = new RegisterUserValidator(_userRepositoryMock.Object);
            var result = await registerUserValidator.TestValidateAsync(registerUserCommand);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
