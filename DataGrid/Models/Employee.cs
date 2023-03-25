using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataGrid.Models
{
    public class Employee
    {
        [Column("employee_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Index(0)]
        [Column("payroll_number")]
        public string PayrollNumber { get; set; }

        [Index(1)]
        [Column("forename")]
        public string Forename { get; set; }

        [Index(2)]
        [Column("surname")]
        public string Surname { get; set; }

        [Index(3)]
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [Index(4)]
        [Column("telephone")]
        public string? Phone { get; set; }

        [Index(5)]
        [Column("mobile")]
        [MaxLength(50)]//(50, "mobile length could not be more than 50 characters")]
        public string Mobile { get; set; }

        [Index(6)]
        [Column("address")]
        [MaxLength(50)]
        public string? Address { get; set; }

        [Index(7)]
        [Column("address2")]
        public string? Address2 { get; set; }

        [Index(8)]
        [Column("postcode")]
        public string? Postcode { get; set; }

        [Index(9)]
        [Column("email_home")]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
        public string? EmailHome { get; set; }


        [Index(10)]
        [Column("start_date")]
        public DateTime? StartDate { get; set; }
    }
//   public class EmployeeCSV
//    {
//        [Index(0)]
//        [Column("payroll_number")]
//        public string PayrollNumber { get; set; }

//        [Index(1)]
//        [Column("forename")]
//        public string Forename { get; set; }

//        [Index(2)]
//        [Column("surname")]
//        public string Surname { get; set; }

//        [Index(3)]
//        [Column("date_of_birth")]
//        public DateTime? DateOfBirth { get; set; }

//        [Index(4)]
//        [Column("telephone")]
//        public string? Phone { get; set; }

//        [Index(5)]
//        [Column("mobile")]
//        [MaxLength(50)]//(50, "mobile length could not be more than 50 characters")]
//        public string Mobile { get; set; }

//        [Index(6)]
//        [Column("address")]
//        [MaxLength(50)]
//        public string? Address { get; set; }

//        [Index(7)]
//        [Column("address2")]
//        public string? Address2 { get; set; }

//        [Index(8)]
//        [Column("postcode")]
//        public string? Postcode { get; set; }

//        [Index(9)]
//        [Column("email_home")]
//        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
//        public string? EmailHome { get; set; }


//        [Index(10)]
//        [Column("start_date")]
//        public DateTime? StartDate { get; set; }
//    }
}
