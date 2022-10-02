using Moq;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.UnitTests.Mock
{
    public static class MockPropertyRepository
    {
        public static List<Property> DummyPropertyList = new List<Property>()
        {
            new Property()
                {
                Id = 1,
                Description = "TestDescription",
                Price = 100,
                ClosedKitchen = false,
                Address= new Address(){ Country = "Poland", City = "Warsaw", Street = "Generalna" }
                },
                 new Property()
                {
                Id=2,
                Description = "TestSecondDescription",
                Price = 100000,
                ClosedKitchen = true,
                Address= new Address(){ Country = "Germany", City = "Berlin", Street = "Strasse" }
                }
        };
       
        public static Mock<IPropertyRepository> GetPropertyRepository()
        {
            var properties = DummyPropertyList;

            var paginationHelper=new PaginationHelper<Property>() { Items=properties, totalCount=properties.Count};
           

            var mockRepo=new Mock<IPropertyRepository>();
            mockRepo.Setup(r=> r.GetAllAsync(It.IsAny<GetPropertiesListQuery>())).ReturnsAsync(paginationHelper);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Property>())).ReturnsAsync((Property property) =>
             {
                 property.Id = 3;
                 properties.Add(property);
                 return property;
             });
            return mockRepo;
        }
    }
}
