using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Models
{
    public class EFStudentRepository : IStudentRepository
    {
        private StudentInfoContext _context { get; set; }
        public EFStudentRepository(StudentInfoContext temp)
        {
            _context = temp;
        }

        public IQueryable<StudentInfo> StudentInfos => _context.StudentInfos;
    }
}
