using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Models
{
    public class EFStudentRepository : IStudentRepository
    {
        private StudentInfoContext context { get; set; }
        public EFStudentRepository(DbContext temp)
        {
            context = temp;
        }

        public IQueryable<StudentInfo> StudentInfos => context.StudentInfos;
    }
}
