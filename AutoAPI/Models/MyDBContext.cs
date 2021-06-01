using Microsoft.EntityFrameworkCore;
using System;
using AutoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAPI
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<Car> Car { get; set; }
        public DbSet<Merk> m1 { get; set; }
        public DbSet<Model> m2 { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}
