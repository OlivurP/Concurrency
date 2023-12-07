using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Concurrency.Models;

namespace Concurrency.Data
{
    public class ConcurrencyContext : DbContext
    {
        public ConcurrencyContext (DbContextOptions<ConcurrencyContext> options)
            : base(options)
        {
        }

        public DbSet<Concurrency.Models.Movie> Movie { get; set; } = default!;

        public DbSet<Concurrency.Models.Order> Order { get; set; } = default!;
        public DbSet<Concurrency.Models.Seat> Seat { get; set; } = default!;
    }
}
