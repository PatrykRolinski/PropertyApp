using FluentAssertions;
using FluentValidation.TestHelper;
using PropertyApp.Application.Functions.Properties.Commands.AddProperty;
using PropertyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PropertyApp.Application.UnitTests.Validators
{
    public class CreatePropertyValidatorTests
    {
        private const string moreThan2000chars = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibu";
        private const string moreThan100chars = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean ma";


        public static IEnumerable<object[]> GetValidSampleData()
        {
            var list = new List<object>()
            {
                new CreatePropertyCommand() { City = "Warsaw", ClosedKitchen = true, Country = "Poland", Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertySize = 30, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                new CreatePropertyCommand() { City = "Some", ClosedKitchen = false, Country = "Germany", Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage}
            };
            return list.Select(c => new object[] { c });
        }

        public static IEnumerable<object[]> GetInvalidSampleData()
        {
            var list = new List<object>()
            { 
                //without City
                new CreatePropertyCommand() { ClosedKitchen = true, Country = "Poland", Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertySize = 30, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage}, 

                //without Country
                new CreatePropertyCommand() { City = "Some", ClosedKitchen = false, Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                //without Description
                 new CreatePropertyCommand() { City = "Some", ClosedKitchen = false, Country = "Germany", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                 //without Price
                  new CreatePropertyCommand() { City = "Some", ClosedKitchen = false, Country = "Germany", Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                    //descritpion too many char
                     new CreatePropertyCommand() { City = "Some", ClosedKitchen = false, Country = "Germany", Description = moreThan2000chars, Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                //City too many char
                      new CreatePropertyCommand() { City = moreThan100chars, ClosedKitchen = true, Country = "Poland", Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertySize = 30, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                //Country too many char
                       new CreatePropertyCommand() { City = "Warsaw", ClosedKitchen = true, Country = moreThan100chars, Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertySize = 30, PropertyStatus = PropertyStatus.Sale, Street = "Street", PropertyType=PropertyType.Garage},

                //Street too many char
                        new CreatePropertyCommand() { City = "Warsaw", ClosedKitchen = true, Country = "Poland", Description = "Some Description", Floor = 1, MarketType = MarketType.Primary,
                NumberOfRooms = 3, Price = 2, PropertySize = 30, PropertyStatus = PropertyStatus.Sale, Street = moreThan100chars, PropertyType=PropertyType.Garage},

            };
            return list.Select(c => new object[] { c });
        }



        [Theory]
        [MemberData(nameof(GetValidSampleData))]
        public void Validate_ForCorrectCommand_ReturnSuccess(CreatePropertyCommand command)
        {
            //arrange
            var validator = new CreatePropertyValidator();
           
            //act
            var result= validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidSampleData))]
        public void Validate_ForIncorrectCommand_ReturnsFailure(CreatePropertyCommand command)
        {
            //arrange
            var validator= new CreatePropertyValidator();

            //
            var result=validator.TestValidate(command);

            //assert
            result.ShouldHaveAnyValidationError();

        }
    }
}
