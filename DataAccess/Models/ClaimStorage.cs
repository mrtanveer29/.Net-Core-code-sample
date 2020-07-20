using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Models
{
    public static class ClaimStorage

    {
        public static List<Claim> claims = new List<Claim> {
            new Claim("Admin","Admin"),
            new Claim("Accounts","Accounts")
        };
    }
}
