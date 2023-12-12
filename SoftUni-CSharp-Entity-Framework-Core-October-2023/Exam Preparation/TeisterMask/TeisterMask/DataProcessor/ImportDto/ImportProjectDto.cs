using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportProjectDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name {get; set; }

        [XmlElement("OpenDate")]
        [Required]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string? DueDate { get; set; }//?

        [XmlArray("Tasks")]
        public ImportProjectTaskDto[] Tasks { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range
//xml
//⦁	Name – text with length [2, 40] (required)
//⦁	OpenDate – date and time (required)
//⦁	DueDate – date and time (can be null)

//projects.xml
//< Project >
//    < Name > S </ Name >
//    < OpenDate > 25 / 01 / 2018 </ OpenDate >
//    < DueDate > 16 / 08 / 2019 </ DueDate >
//    < Tasks >
