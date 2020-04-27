using loginIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace loginIdentity.Data
{
    public class ApplicationDataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
             : base (options)
        {
            
        }        
        
    }
}