using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassProject.Models
{
    public partial class StudentInfo
    {  
        [Key]
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool International { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int ExpectedHours { get; set; }
        public string Semester { get; set; }
        public string Year1 { get; set; }
        public string Phone { get; set; }
        public int ByuId { get; set; }
        public string PositionType { get; set; }
        public int ClassCode { get; set; }
        public int EmplRecord { get; set; }
        public string Supervisor { get; set; }
        public DateTime HireDate { get; set; }
        public float Payrate { get; set; }
        public DateTime LastPayIncrease { get; set; }
        public float PayIncreaseAmount { get; set; }
        public DateTime IncreaseInputDate { get; set; }
        public string YearInProgram { get; set; }
        public bool PayGradTuition { get; set; }
        public bool NameChangeCompleted { get; set; }
        public string Notes { get; set; }
        public bool Terminated1 { get; set; }
        public DateTime TerminationDate { get; set; }
        public string QualtricsSurvey { get; set; }
        public bool SubmittedForm { get; set; }
        public bool AuthorizationToWorkReceived { get; set; }
        public DateTime AuthorizationToWorkEmailSentDate { get; set; }
        public string ByuName { get; set; }
    }

    
}
