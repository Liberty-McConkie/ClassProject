
using System;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Models
{
    //constructor
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base (options)
        {
            //leave blank
        }

        //        //create the dataset!!

        public DbSet<StudentInfo> StudentInfos { get; set; }

    }
       
}




