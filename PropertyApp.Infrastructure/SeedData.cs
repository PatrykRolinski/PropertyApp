using Bogus;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Infrastructure
{
    public class SeedData
    {
        private readonly PropertyAppContext _context;

        public SeedData(PropertyAppContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Properties.Any())
                {
                var addressGenerator = new Faker<Address>()
                       .RuleFor(a => a.City, f => f.Address.City())
                       .RuleFor(a => a.Country, f => f.Address.Country())
                       .RuleFor(a => a.Street, f => f.Address.StreetName())
                       .RuleFor(a => a.Floor, f => f.Random.Byte(1,10));

                
                var roles = new[] { "Member", "Manager" };
                var roleGenerator = new Faker<Role>()
                    .RuleFor(r => r.Name, f => f.PickRandom(roles));

                var userGenerator = new Faker<User>()
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Email, f => f.Person.Email)
                    .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
                    .RuleFor(u => u.Role, f => roleGenerator.Generate())
                    .RuleFor(u=> u.CreatedDate,f => f.Date.Between(new DateTime(2000, 01, 10), new DateTime(2020, 01, 10)));
                    

                var photoGenerator = new Faker<Photo>()
                    .RuleFor(p => p.Url, f => f.Image.PicsumUrl())
                    .RuleFor(p => p.IsMain, f => f.Random.Bool())
                    .RuleFor(p => p.PublicId, f => f.Internet.Url());

                var propertyGenerator = new Faker<Property>()
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence(10))
                    .RuleFor(p => p.OriginalPrice, f => f.Random.Int(500, 1000000))
                    .RuleFor(p => p.Price, f => f.Random.Int(500, 1000000))
                    .RuleFor(p=> p.PropertyType, f=> f.PickRandom<PropertyType>())
                    .RuleFor(p=> p.MarketType, f=>f.PickRandom<MarketType>())
                    .RuleFor(p=> p.ClosedKitchen, f=> f.Random.Bool())
                    .RuleFor(p=> p.PropertySize, f=> f.Random.UShort(1, 200))
                    .RuleFor(p=> p.NumberOfRooms, f => f.Random.Byte(1, 100))
                    .RuleFor(p=> p.Address, f=>addressGenerator.Generate())
                    .RuleFor(p=> p.CreatedBy, f=> userGenerator.Generate())
                    .RuleFor(p=> p.CreatedDate, f=> f.Date.Between(new DateTime(2000,01,10), new DateTime(2020,01,10)))
                    .RuleFor(p=> p.Photos, f=> photoGenerator.Generate(3).ToList());

                var property = propertyGenerator.Generate(190);
                _context.AddRange(property);
                _context.SaveChanges();
                
                }

            }
            else
            {
                throw new Exception();
            }



        }
    }
}
