using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Naringskollen.Data
{
    public class NaringskollenDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public NaringskollenDbContext(DbContextOptions<NaringskollenDbContext> options) : base(options)
        {
           
        }
    }
}
