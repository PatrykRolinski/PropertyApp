using AutoMapper;
using FluentAssertions;
using Moq;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Mapper;
using PropertyApp.Application.Models;
using PropertyApp.Application.UnitTests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PropertyApp.Application.UnitTests.Properties
{
    public class GetPropertiesListHandlerTests
    {
        private readonly Mock<IPropertyRepository> _mockRepo;
        private readonly IMapper _mapper;

        public GetPropertiesListHandlerTests()
        {
            _mockRepo= MockPropertyRepository.GetPropertyRepository();
            var mapperConfig = new MapperConfiguration(c =>
             {
                 c.AddProfile<MappingProfile>();
             });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async void Handle_ValidQuery_ReturnPropertiesList()
        {
            
            
            //arrange
            var handler = new GetPropertiesListHandler(_mockRepo.Object, _mapper);

            //act
            var totalCount = MockPropertyRepository.DummyPropertyList.Count;
            var result =handler.Handle(new GetPropertiesListQuery() { PageSize=5, PageNumber=1}, CancellationToken.None).Result;
            var totalItems =result.TotalCount;
            
            //assert
            result.Should().BeOfType<PageResult<GetPropertiesListDto>>();
            totalItems.Should().Be(totalCount);

            
        }

        [Theory]
        [InlineData(3,1)]
        [InlineData(-3, 1)]
        [InlineData(5, -1)]
        [InlineData(0, 0)]
        public async void Handle_InvalidQuery_ThrowValidationException(int pageSize, int pageNumber)
        {
            //arrange
            var handler = new GetPropertiesListHandler(_mockRepo.Object, _mapper);

            //act
            Func<Task> result = async () => await handler.Handle(new GetPropertiesListQuery() { PageSize = pageSize, PageNumber = pageNumber }, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
