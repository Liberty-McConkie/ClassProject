using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Models
{
    public class EFStudentRepository : IStudentRepository
    {
        private StudentDbContext _context { get; set; }
        public EFStudentRepository(StudentDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<StudentInfo> StudentInfos => _context.StudentInfos;
    }
}
