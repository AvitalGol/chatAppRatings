using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using chatAppRatings.Models;

namespace chatAppRatings.Data
{
    public class chatAppRatingsContext : DbContext
    {
        public chatAppRatingsContext (DbContextOptions<chatAppRatingsContext> options)
            : base(options)
        {
        }

        public DbSet<chatAppRatings.Models.Rating>? Rating { get; set; }
    }
}
