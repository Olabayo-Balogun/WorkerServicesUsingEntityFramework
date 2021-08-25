using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerServicesUsingEntityFramework.Models;

namespace WorkerServicesUsingEntityFramework.Services
{
    //Inheriting the DbContextb class below helps you interact with the database
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
    }
}
