using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DataGrid.Models
{
    public class Employee
    {
        [Column("employee_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Column("forename")]
        public string Forename { get; set; }
        
        [Column("surname")]
        public string Surname { get; set; }
        
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }
        
        [Column("telephone")]
        public string? Phone { get; set; }
        
        [Column("mobile")]
        [MaxLength(50)]//(50, "mobile length could not be more than 50 characters")]
        public string Mobile { get; set; }
        
        [Column("address")]
        [MaxLength(50)]
        public string? Address { get; set; }
        
        [Column("address2")]
        public string? Address2 { get; set; }
        
        [Column("postcode")]
        public string? Postcode { get; set; }
        
        [Column("email_home")]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
        public string? EmailHome { get; set; }
        

        [Column("start_date")]
        public DateTime? StartDate { get; set; }
        
        
    }
}
