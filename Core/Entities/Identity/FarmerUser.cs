using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class FarmerUser : IdentityUser
    {

        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Points { get; set; }
        public string AccountType { get; set; }
        public string DisplayName { get; set; }

        public Address Address { get; set; }


    }
}