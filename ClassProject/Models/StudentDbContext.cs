
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Models
{
    //constructor
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            //leave blank
        }

        public DbSet<StudentInfo> StudentInfo { get; set; }
    }
}
