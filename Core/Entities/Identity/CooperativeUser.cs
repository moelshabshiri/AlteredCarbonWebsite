using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class CooperativeUser : IdentityUser
    {

        public string Name { get; set; }
        // public string Email { get; set; }
        // public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public string DisplayName { get; set; }


        // public List<KitOrder> kitOrders { get; set; }
        // public List<SellAgrWasteOrder> sellAgrWasteOrders { get; set; }


    }
}