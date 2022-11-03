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

        public IQueryable<StudentInfo> StudentInfo => _context.StudentInfo;
        public void AddStudent(StudentInfo si)
        {
            _context.Add(si);
            _context.SaveChanges();
        }
        public void UpdateStudent(StudentInfo si)
        {
            _context.Update(si);
            _context.SaveChanges();
        }
        public void DeleteStudent(StudentInfo si)
        {
            _context.Remove(si);
            _context.SaveChanges();
        }
    }
}
