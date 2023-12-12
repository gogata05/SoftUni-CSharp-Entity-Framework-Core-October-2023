using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TeisterMask.Data.Models;
namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectDto
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }//?

        [XmlArray("Tasks")]
        public ExportProjectTaskDto[] Tasks { get; set; }
    }
}
//xml

//Id – integer, Primary Key 
//Name – text 
//OpenDate – date and time 
//DueDate – date and time 
//Tasks – collection of type Task 

//<Project TasksCount="10">
//< ProjectName > Hyster - Yale </ ProjectName >
//< HasEndDate > No </ HasEndDate >
//< Tasks >
