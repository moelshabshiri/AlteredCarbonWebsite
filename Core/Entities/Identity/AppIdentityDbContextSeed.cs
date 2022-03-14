
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<FarmerUser> fUserManager, UserManager<CooperativeUser> cUserManager)
        {
            if (!fUserManager.Users.Any())
            {
                var user = new FarmerUser
                {
                    Name = "Mohamed",
                    Email = "elshabshiri@gmail.com",
                    UserName = "elshabshiri@gmail.com", 
                    PhoneNumber = "01075402387", 
                    Points=500,
                    AccountType="farmer",
                    Address = new Address
                    {
                        FirstName = "Mohamed",
                        LastName = "Elshabshiri",
                        Street = "36",
                        City = "Cairo",
                        ZipCode = "31311"
                    }
                };

                await fUserManager.CreateAsync(user, "Pa$$w0rd");
            }


            if (!cUserManager.Users.Any())
            {
                var user = new CooperativeUser
                {
                    Name = "Coop",
                    Email = "coop@gmail.com",
                    UserName = "coop@gmail.com", 
                    PhoneNumber = "01075402888", 
                    AccountType="cooperative"
                };

                await cUserManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}