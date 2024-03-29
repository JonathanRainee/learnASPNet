﻿using JRamedia.Models;
using Microsoft.EntityFrameworkCore;

namespace JRamedia.Data
{
    public class ApplicationDBContext: DbContext
    {

        //constructor               param           class                  nama       kmn
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        //bikin table
        public DbSet<Category> Categories { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
