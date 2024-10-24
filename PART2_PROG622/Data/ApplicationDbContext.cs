using Microsoft.EntityFrameworkCore;
using PART2_PROG622.Models;
using System.Collections.Generic;


    namespace PART2_PROG622.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }



            public DbSet<Claim> Claims { get; set; }
        public DbSet<SystemFile> Files { get; set; }
        
            
            public DbSet<FileCreation> FileCreations { get; set; }
        }
    }


