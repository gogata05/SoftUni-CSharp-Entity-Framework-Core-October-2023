using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class ImportEmployeeDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]//?
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}")]//?
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//⦁	Username – string with length [3, 40].Should contain only lower or upper case letters and/or digits. (required)
//⦁	Email – string (required). Validate it! There is attribute for this job.
//⦁	Phone – string. Consists only of three groups (separated by '-'), the first two consist of three digits and the last one – of 4 digits. (required)

//employees.json
//"Username": "jstanett0",
//"Email": "kknapper0@opera.com",
//"Phone": "819-699-1096",
//"Tasks": [
//34,
//32,
//65,
//30,
//30,
//45,