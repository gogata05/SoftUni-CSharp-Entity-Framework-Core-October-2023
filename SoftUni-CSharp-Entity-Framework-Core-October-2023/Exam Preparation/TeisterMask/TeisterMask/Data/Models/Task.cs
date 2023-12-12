using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeisterMask.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeisterMask.Data.Models
{
    public class Task
    {
        public Task()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }
        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public DateTime OpenDate { get; set; } 
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int ExecutionTypeId { get; set; }
        [Required]
        public virtual ExecutionType ExecutionType { get; set; } 
        [Required]
        public int LabelTypeId { get; set; }
        [Required]
        public virtual LabelType LabelType { get; set; } 
        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        [Required]
        public virtual Project Project { get; set; } 
    }
}
//pk,fk,req,max
//ctor
//Task
//⦁	Id – integer, Primary Key
//⦁	Name – text with length [2, 40] (required)
//⦁	OpenDate – date and time (required)
//⦁	DueDate – date and time (required)
//⦁	ExecutionType – enumeration of type ExecutionType, with possible values (ProductBacklog, SprintBacklog, InProgress, Finished) (required)
//⦁	LabelType – enumeration of type LabelType, with possible values (Priority, CSharpAdvanced, JavaAdvanced, EntityFramework, Hibernate) (required)
// ⦁	ProjectId – integer, foreign key (required)
// ⦁	Project – Project 
// ⦁	EmployeesTasks – collection of type EmployeeTask
