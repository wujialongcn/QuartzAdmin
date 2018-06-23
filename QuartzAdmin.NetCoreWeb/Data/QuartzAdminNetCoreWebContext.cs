using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuartzAdmin.NetCoreWeb.Models
{
    public class QuartzAdminNetCoreWebContext : DbContext
    {
        public QuartzAdminNetCoreWebContext (DbContextOptions<QuartzAdminNetCoreWebContext> options)
            : base(options)
        {
        }

        public DbSet<QuartzAdmin.NetCoreWeb.Models.Movie> Movie { get; set; }
    }
}
