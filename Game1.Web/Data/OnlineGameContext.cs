using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Game1.Web.Models;

namespace Game1.Web.Data
{
    public class OnlineGameContext : IdentityDbContext<User>
    {
        public OnlineGameContext(DbContextOptions<OnlineGameContext> options) 
        : base(options)
        {
                
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
