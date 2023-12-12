using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeisterMask.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }
        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
        public string Phone { get; set; }

    }
}
//pk,fk,req,max
//ctor
//Employee
//⦁	Id – int, Primary Key
//⦁	Username – string with length [3, 40].Should contain only lower or upper case letters and/or digits. (required)
//⦁	Email – string (required). Validate it! There is attribute for this job.
//⦁	Phone – string. Consists only of three groups (separated by '-'), the first two consist of three digits and the last one – of 4 digits. (required)
//⦁	EmployeesTasks – collection of type EmployeeTask
