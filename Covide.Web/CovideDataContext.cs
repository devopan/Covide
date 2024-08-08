using Covide.Web.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Covide.Web
{
    public class CovideDataContext : DbContext
    {
        public CovideDataContext(
            DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ColorCode> ColorCodes { get; set; }
    }
}
