using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Models;

namespace LibrarySystem.Data
{
    public class LibraryDBContext : IdentityDbContext<IdentityDBContext>
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext> _options):base(_options)
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
