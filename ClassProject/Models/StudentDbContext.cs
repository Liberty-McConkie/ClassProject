
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
}
}
}
}

        public DbSet<StudentInfo> StudentInfos { get; set; }














// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

    public partial class BookstoreContext : DbContext
    {
        public StudentInfoContext()
        {
        }

        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StudentInfo> StudentInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source = Bookstore.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.HasIndex(e => e.BookId)
                    .IsUnique();




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
