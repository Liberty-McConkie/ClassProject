using System;
namespace ClassProject.Models
{
    public class IndexViewModel
    {
        public decimal empCountMath;

        public StudentInfo studentInfo { get; set; }

        public decimal avgPay { get; set; }
        public decimal taAvgPay { get; set; }
        public decimal raAvgPay { get; set; }
        public decimal officeAvgPay { get; set; }
        public decimal stInstAvgPay { get; set; }
        public decimal otherAvgPay { get; set; }
    }
}

