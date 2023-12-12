// ReSharper disable InconsistentNaming
using TeisterMask.Utilities;
namespace TeisterMask.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using Data;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ImportDto;
    using Microsoft.VisualBasic;
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";
        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";
        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportProjectDto[] projectDtos =
                xmlHelper.Deserialize<ImportProjectDto[]>
                    (xmlString, "Projects");
            StringBuilder sb = new StringBuilder();
            List<Project> projects = new List<Project>();
            foreach (ImportProjectDto projectDto in projectDtos)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                DateTime ProjectStartDate;//default value 01/01/0001
                bool isProjectStartDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out ProjectStartDate);
                if (!isProjectStartDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                DateTime? ProjectEndDate = null;
                if (!String.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    DateTime ProjectEndDateValue;
                    bool isEndDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out ProjectEndDateValue);
                    if (!isEndDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    ProjectEndDate = ProjectEndDateValue;
                }

                Project project = new Project()
                {
                    //Name>S</Name>
                    //<OpenDate>25/01/2018</OpenDate>
                    //<DueDate>16/08/2019</DueDate>
                    Name = projectDto.Name,
                    OpenDate = ProjectStartDate,
                    DueDate = ProjectEndDate,
                };
                foreach (ImportProjectTaskDto taskDto in projectDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime taskStartDate;//default value 01/01/0001
                    bool isOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out taskStartDate);
                    if (!isOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    DateTime? taskEndDate = null;
                    DateTime taskEndDateValue;
                        bool isDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out taskEndDateValue);
                    //if task start date, is after ,project start date
                    if (taskStartDate < ProjectStartDate)//<>?
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    //if task end date, is after ,project end date
                    if (ProjectEndDate.HasValue && taskEndDateValue > ProjectEndDate.Value)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task task = new Task()
                    {
                        //<Name>Australian</Name>
                        //<OpenDate>19/08/2018</OpenDate>
                        //<DueDate>13/07/2019</DueDate>
                        //<ExecutionType>2</ExecutionType>
                        //<LabelType>0</LabelType>
                        Name = taskDto.Name,
                        OpenDate = taskStartDate,
                        DueDate = taskEndDateValue,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType,
                    };
                    project.Tasks.Add(task);
                }
                projects.Add(project);
                sb.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }
            context.Projects.AddRange(projects);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            ImportEmployeeDto[] employeeDtos =
                JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Employee> employees = new List<Employee>();
            foreach (ImportEmployeeDto employeeDto in employeeDtos)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Employee employee = new Employee()
                {
                    //"Username": "jstanett0", 
                    //"Email": "kknapper0@opera.com", 
                    //"Phone": "819-699-1096", 
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                };

                foreach (var taskId in employeeDto.Tasks.Distinct())
                {
                    Task task = context.Tasks.Find(taskId);
                    if (task == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    employee.EmployeesTasks.Add(new EmployeeTask()
                    {
                        Employee = employee,
                        Task = task,
                    });
                }
                employees.Add(employee);
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }
            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}