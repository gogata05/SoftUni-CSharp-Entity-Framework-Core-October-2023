using TeisterMask.Utilities;

namespace TeisterMask.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Xml.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using Data;
    using Data.Models;
    using ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;
    using TeisterMask.DataProcessor.ImportDto;
   public class Serializer
   {
       public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
       {
           /*ExportProjectDto[] projectDtos = Mapper.Map<ExportProjectDto[]>(projects)
               .OrderByDescending(p => p.TasksCount)
               .ThenBy(p => p.Name)
               .ToArray();*/

           ExportProjectDto[] projects = context.Projects
               .Where(x => x.Tasks.Any())
               .ToArray()
               .Select(x => new ExportProjectDto
               {
                   //TasksCount="10"> 
                   //< ProjectName > Hyster - Yale </ ProjectName >
                   //< HasEndDate > No </ HasEndDate >
                   //< Tasks >
                   TasksCount = x.Tasks.Count,
                   ProjectName = x.Name,
                   HasEndDate = x.DueDate.HasValue ? "Yes" : "No",
                   Tasks = x.Tasks
                   .ToArray()
                   .OrderBy(x => x.Name)
                   .Select(y => new ExportProjectTaskDto
                   {
                       //<Name>Broadleaf</Name>
                       //<Label>JavaAdvanced</Label>
                       Name = y.Name,
                       Label = y.LabelType.ToString()
                   })
               .ToArray(),
               })
               .OrderByDescending(x => x.Tasks.Length)
               .ThenBy(x => x.ProjectName)//?
               .ToArray();
           XmlHelper xmlHelper = new XmlHelper();
           return xmlHelper.Serialize(projects, "Projects");
       }

       public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
       {
           var employees = context.Employees
               .Where(x => x.EmployeesTasks.Any(y => y
                   .Task.OpenDate >= date))
               .ToArray()

               .Select(x => new
               {
                   x.Username,
                   Tasks = x.EmployeesTasks
                       .Where(y => y.Task.OpenDate >= date)
                   .ToArray()

                   .OrderByDescending(y => y.Task.DueDate)
                   .ThenBy(y => y.Task.Name)

                   .Select(y => new
                   {
                       //"TaskName": "Pointed Gourd", 
                       //"OpenDate": "10/08/2018", 
                       //"DueDate": "10/24/2019", 
                       //"LabelType": "Priority", 
                       //"ExecutionType": "ProductBacklog"
                       TaskName = y.Task.Name,
                       OpenDate = y.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                       DueDate = y.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                       LabelType = y.Task.LabelType.ToString(),
                       ExecutionType = y.Task.ExecutionType.ToString()
                   }).ToArray()
               })

               .OrderByDescending(x => x.Tasks.Length)
               .ThenBy(x => x.Username)
   
               .Take(10)
               .ToArray();
           return JsonConvert.SerializeObject(employees, Formatting.Indented);
       }
   }
}