using FluentValidation.TestHelper;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PropertyApp.Application.UnitTests.Validators
{
    public class GetPropertiesListValidatorTests
    {
        public static IEnumerable<object[]> GetSampleValidData()
        {
            var list = new List<GetPropertiesListQuery>()
            {
                new GetPropertiesListQuery()
                {
                PageNumber = 1,
                PageSize = 10,
                MinimumPrice = 100,
                MaximumPrice = 200,
                MinimumSize = 10,
                MaximumSize = 20,
                },
                 new GetPropertiesListQuery()
                {
                PageNumber = 2,
                PageSize = 5,
                MinimumPrice = 0,
                MaximumPrice = 200,
                MinimumSize = 0,
                MaximumSize = 60,
                }

            };
            return list.Select(q => new object[] { q });
        }

        public static IEnumerable<object[]> GetSampleInvalidData()
        {
            var list = new List<GetPropertiesListQuery>()
            {
                new GetPropertiesListQuery()
                {
                PageNumber = 1,
                PageSize = 3,
                MinimumPrice = 100,
                MaximumPrice = 200,
                MinimumSize = 10,
                MaximumSize = 20,
                },
                 new GetPropertiesListQuery()
                {
                PageNumber = 1,
                PageSize = 5,
                MinimumPrice = -2,
                MaximumPrice = 1,
                MinimumSize = 0,
                MaximumSize = 60,
                },
                new GetPropertiesListQuery()
                {
                PageNumber = 1,
                PageSize = 5,
                MinimumPrice = 0,
                MaximumPrice = -6,
                MinimumSize =10,
                MaximumSize = 4,
                },
                new GetPropertiesListQuery()
                {
                PageNumber = 1,
                PageSize = 5,
                MinimumPrice = 1,
                MaximumPrice = 2,
                MinimumSize = -1,
                MaximumSize = 5,
                },
                 new GetPropertiesListQuery()
                {
                PageNumber = 1,
                PageSize = 5,
                MinimumPrice = -1,
                MaximumPrice = -2,
                MinimumSize = -1,
                MaximumSize = -5,
                }

            };
            return list.Select(q => new object[] { q });
        }
        [Theory]
        [MemberData(nameof(GetSampleValidData))]
        public void Validate_ForCorrectQuery_ReturnSuccess(GetPropertiesListQuery query)
        {
            //arrange

            var validator = new GetPropertiesListValidator();
            
             //act
            var result=validator.TestValidate(query);

            //assert
            result.ShouldNotHaveAnyValidationErrors();

        }
        [Theory]
        [MemberData(nameof(GetSampleInvalidData))]
        public void Validate_ForIncorrectQuery_ReturnFailure(GetPropertiesListQuery query)
        {
            //arrange

            var validator = new GetPropertiesListValidator();

            //act
            var result = validator.TestValidate(query);

            //assert
            result.ShouldHaveAnyValidationError();

        }

    }
}
