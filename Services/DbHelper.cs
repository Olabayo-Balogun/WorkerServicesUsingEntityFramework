using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerServicesUsingEntityFramework.Models;

namespace WorkerServicesUsingEntityFramework.Services
{
    public class DbHelper
    {
        private AppDbContext dbContext;

        private DbContextOptions<AppDbContext> GetAllOptions()
        {
            var optionBuilder= new DbContextOptionsBuilder<AppDbContext>();
            //This is where we pass the connectionstrings to the database
            optionBuilder.UseSqlServer("");
            return optionBuilder.Options;
        }

        //The method below is used to return all users in a list form
        public List<User> GetAllUsers()
        {
            using(dbContext=new AppDbContext(GetAllOptions()))
            {
                try
                {
                    //The variable below attempts to obtain the list of users in the database
                    var users = dbContext.Users.ToList();

                    //The contition below aims to pass in new data if the users list doesn't exist.
                    if (users != null)
                    {
                        return users;
                    }
                    else
                    {
                        return new List<User>();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

      
    }
}
