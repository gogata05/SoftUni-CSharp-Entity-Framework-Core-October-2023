using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;
using TeisterMask.Data.Models;
namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class ImportProjectTaskDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name {get; set; }

        [XmlElement("OpenDate")]
        [Required]
        public string OpenDate { get; set; }//DateTime or string?

        [XmlElement("DueDate")]
        [Required]
        public string DueDate { get; set; }//DateTime or string?

        [XmlElement("ExecutionType")]
        [Required]
        public int ExecutionType { get; set; }//int or string?

        [XmlElement("LabelType")]
        [Required]
        public int LabelType { get; set; }//int or string?
    }
}
//req,"?",min,max,range,regular,email,enum range

//Name – text with length [2, 40] (required)
//OpenDate – date and time (required) 
//DueDate – date and time (required) 
//ExecutionType – enumeration of type ExecutionType, with possible values (ProductBacklog, SprintBacklog, InProgress, Finished) (required)
//LabelType – enumeration of type LabelType, with possible values (Priority, CSharpAdvanced, JavaAdvanced, EntityFramework, Hibernate) (required)

//< Task >
//< Name > Australian </ Name >
//< OpenDate > 19 / 08 / 2018 </ OpenDate >
//< DueDate > 13 / 07 / 2019 </ DueDate >
//< ExecutionType > 2 </ ExecutionType >
//< LabelType > 0 </ LabelType >
