using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PropertyApp.Api.IntegrationTests.Helpers;
using PropertyApp.Application.Functions.Properties.Commands.AddProperty;
using PropertyApp.Domain.Entities;
using PropertyApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PropertyApp.Api.IntegrationTests
{
    public class PropertyControllerTests: IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private HttpClient _client;
        private CustomWebApplicationFactory<Program> _factory;
        

        public PropertyControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private void SeedRestaurant(Property property)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<PropertyAppContext>();
            dbContext.Add(property);
            dbContext.SaveChanges();
        }

        //Tests for GetAllProperties
        #region
        public static IEnumerable<object[]> GetValidQueryParameters()
        {
            yield return new object[] { "PageNumber=1&PageSize=5" };
            yield return new object[] { "PageNumber=1&PageSize=10" };
            yield return new object[] { "PageNumber=2&PageSize=15" };
            yield return new object[] { "PageNumber=1&PageSize=5&City=Warsaw" };
            yield return new object[] { "PageNumber=1&PageSize=5&Country=Poland" };
            yield return new object[] { "PageNumber=1&PageSize=5&MinimumPrice=100" };
            yield return new object[] { "PageNumber=1&PageSize=5&MaximumPrice=30719847"};
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyStatus=Sale" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyStatus=Rent" };
            yield return new object[] { "PageNumber=1&PageSize=5&MarketType=Primary" };
            yield return new object[] { "PageNumber=1&PageSize=5&MarketType=Secondary" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyType=Flat" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyType=House" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyType=Penthouse" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyType=Garage" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyType=Castle" };
            yield return new object[] { "PageNumber=1&PageSize=5&SortBy=Date" };
            yield return new object[] { "PageNumber=1&PageSize=5&SortBy=Price" };
            yield return new object[] { "PageNumber=1&PageSize=5&SortOrder=Ascending" };
            yield return new object[] { "PageNumber=1&PageSize=5&SortOrder=Descending" };
        }
        public static IEnumerable<object[]> GetInvalidQueryParameters()
        {
            yield return new object[] { "" };
            yield return new object[] { "PageNumber=1&PageSize=7" };
            yield return new object[] { "PageNumber=0&PageSize=10" };
            yield return new object[] { "PageNumber=1&PageSize=-15" };
            yield return new object[] { "PageNumber=1&PageSize=5&MinimumPrice=-100" };
            yield return new object[] { "PageNumber=1&PageSize=5&MaximumPrice=-1" };
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyStatus=Saleee" };
            yield return new object[] { "PageNumber=1&PageSize=5&MarketType=InvalidMarketType"};
            yield return new object[] { "PageNumber=1&PageSize=5&PropertyType=InvalidPropertyType" };
            yield return new object[] { "PageNumber=1&PageSize=5&SortBy=InvalidSortByInput" };
            yield return new object[] { "PageNumber=1&PageSize=5&SortOrder=InvalidSortOrderInput" };
        }
        [Theory]
        [MemberData(nameof(GetValidQueryParameters))]
        public async Task GetAllProperties_WithValidQueryParameters_ReturnsOkResult(string validQuery)
        {
           

            //act
            var result= await _client.GetAsync($"api/Property?{validQuery}");

            //assert
            result.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
        }


        [Theory]
        [MemberData(nameof(GetInvalidQueryParameters))]
        public async Task GetAllProperties_WithInvalidQueryParameters_ReturnsBadRequestResult(string invalidQuery)
        {
           
            //act
            var result = await _client.GetAsync($"api/Property?{invalidQuery}");

            //assert
            result.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
        }
        #endregion// //Test

        //Tests for AddProperty
        #region
        [Fact]
        public async Task AddProperty_WithValidModel_ReturnsCreatedResult()
        {
            //arrange
            var model = new CreatePropertyCommand()
            {
                Description = "TestDescription",
                Price = 100,
                Country = "Poland",
                City = "Warsaw",
                Street = "Generalna",
                ClosedKitchen = false,
            };

            var formContent = model.ToMultipartFormDataContent();

            //act

            var response = await _client.PostAsync("api/property", formContent);

            //assert

            response.Should().HaveStatusCode(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();

        }
        [Fact]
        public async Task AddProperty_WithInvalidModel_ReturnsBadRequestResult()
        {
            var model = new CreatePropertyCommand()
            {
                PropertySize = 30
            };

            var formContent = model.ToMultipartFormDataContent();

            //act

            var response = await _client.PostAsync("api/property", formContent);

            //assert

            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Headers.Location.Should().BeNull();

        }
        #endregion

        //Tests for DeleteProperty
        #region
        [Fact]
        public async Task DeleteProperty_ValidCommand_ReturnsNoContent()
        {
            //arrange
            var property = new Property()
            {
                CreatedById = Guid.Parse("53bfba37-1b94-41e3-abfa-bbc7c8cc5ae9"),
                Description = "Test"
            };

            SeedRestaurant(property);
            
            //act
            var response = await _client.DeleteAsync("api/property/" + property.Id);

            //assert

            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteProperty_ForNonPropertyCreator_ReturnsForbidden()
        {
            //arrange
            var property = new Property()
            {
                CreatedById = Guid.Parse("33bfba37-1b94-41e3-abfa-bbc7c8cc5ae9"),
                Description = "Test"
            };

            SeedRestaurant(property);

            //act
            var response = await _client.DeleteAsync("api/property/" + property.Id);

            //assert

            response.Should().HaveStatusCode(System.Net.HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task DeleteProperty_ForNonExsistProperty_ReturnsNotFound()
        {
            //act
            var response = await _client.DeleteAsync("api/property/" + "150000");

            //assert

            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
        }

        #endregion

    }
}