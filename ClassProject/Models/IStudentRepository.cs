using System;
using System.Linq;

namespace ClassProject.Models
{
    public interface IStudentRepository
    {
        IQueryable<StudentInfo> StudentInfo { get; }
        public void AddStudent(StudentInfo si);
        public void UpdateStudent(StudentInfo si);
        public void DeleteStudent(StudentInfo si);
    }
}
