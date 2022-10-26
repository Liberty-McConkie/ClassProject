using System;
using System.Linq;

namespace ClassProject.Models
{
    public interface IStudentRepository
    {
        IQueryable<StudentInfo> StudentInfos { get; }
    }
}
