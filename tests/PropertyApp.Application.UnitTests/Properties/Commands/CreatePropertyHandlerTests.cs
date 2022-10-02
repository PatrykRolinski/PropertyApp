using AutoMapper;
using CloudinaryDotNet.Actions;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Moq;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Functions.Properties.Commands.AddProperty;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Mapper;
using PropertyApp.Application.UnitTests.Mock;
using PropertyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PropertyApp.Application.UnitTests.Properties
{
    public class CreatePropertyHandlerTests
    {
        private readonly Mock<IPropertyRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IAuthorizationService> _authorizationService;
        private readonly Mock<IPhotoService> _photoService;

        public CreatePropertyHandlerTests()
        {
            _mockRepo = MockPropertyRepository.GetPropertyRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _currentUserService = new Mock<ICurrentUserService>();
            _authorizationService = new Mock<IAuthorizationService>();
            _photoService = new Mock<IPhotoService>();
        }
        private const string correctId = "53bfba37-1b94-41e3-abfa-bbc7c8cc5ae9";
        private readonly ClaimsPrincipal correctClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, correctId),
                    new Claim(ClaimTypes.Role, "Manager")
                }));

        [Fact]
        public async Task Handle_ValidateCommand_AddPropertyToRepository()
        {
            //arrange
            _currentUserService.Setup(x => x.User).Returns(correctClaimsPrincipal);
            _currentUserService.Setup(x => x.UserId).Returns(correctId);
            
            _authorizationService.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<Property>(), It.IsAny<IEnumerable<IAuthorizationRequirement>>()))
                                    .ReturnsAsync(AuthorizationResult.Success());

            _photoService.Setup(p=> p.AddPhotoAsync(It.IsAny<FormFile>()))
                .ReturnsAsync(new ImageUploadResult());
            //act
            var beforeNumberOfProperties = MockPropertyRepository.DummyPropertyList.Count;
            
            var handler = new CreatePropertyHandler(_mockRepo.Object, _mapper, _photoService.Object, _currentUserService.Object, _authorizationService.Object);
            var result =await handler.Handle(new CreatePropertyCommand() { Description = "Test", Price = 199, ClosedKitchen = true, Country = "Poland", City = "Warsaw", Street = "Generalna" },CancellationToken.None);

            var afterNumberOfProperties = MockPropertyRepository.DummyPropertyList.Count;

            //assert
            result.Should().NotBe(0);
            beforeNumberOfProperties.Should().Be(afterNumberOfProperties - 1);
        }
    }
}
