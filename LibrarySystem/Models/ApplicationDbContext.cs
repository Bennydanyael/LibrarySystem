using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityDBContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> _options):base(_options)
        {
        }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Persons> Persons { get; set; }
        public DbSet<LibrarySystemRun> LibrarySystemRuns { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
