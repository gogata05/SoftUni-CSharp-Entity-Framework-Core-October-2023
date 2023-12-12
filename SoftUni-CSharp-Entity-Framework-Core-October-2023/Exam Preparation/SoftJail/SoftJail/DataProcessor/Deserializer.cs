using CarDealer.Utilities;
namespace SoftJail.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    // ReSharper disable InconsistentNaming
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";
        private const string SuccessfullyImportedDepartment = "Imported {0} with {1} cells";
        private const string SuccessfullyImportedPrisoner = "Imported {0} {1} years old";
        private const string SuccessfullyImportedOfficer = "Imported {0} ({1} prisoners)";
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            ImportDepartmentDto[] departmentDtos = 
                JsonConvert.DeserializeObject<ImportDepartmentDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Department> departments = new List<Department>();

            foreach (ImportDepartmentDto departmentDto in departmentDtos)
            {
                if (!IsValid(departmentDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Department department = new Department()
                {
                    Name = departmentDto.Name,
                };
                bool isCellValid = true;
                foreach (var cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        isCellValid = false;
                        break;
                    }
                    Cell cell = new Cell()
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    };
                    department.Cells.Add(cell);
                }

                if (!isCellValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (department.Cells.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                departments.Add(department);
                sb.AppendLine(String.Format(SuccessfullyImportedDepartment, department.Name, department.Cells.Count));
            }
            context.Departments.AddRange(departments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
       
        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            ImportPrisonerDto[] prisonerDtos = 
                JsonConvert.DeserializeObject<ImportPrisonerDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Prisoner> prisoners = new List<Prisoner>();
            foreach (ImportPrisonerDto prisonerDto in prisonerDtos)
            {
                //var uniquePrisoner = prisoners.FirstOrDefault(x => x.PrisonerName == dto.PrisonerName);|| uniquePrisoner != null
                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime incarcerationDate;
                bool isIncarcerationDateValid = DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out incarcerationDate);
                if (!isIncarcerationDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                DateTime? releaseDate = null;
                if (!String.IsNullOrWhiteSpace(prisonerDto.ReleaseDate))
                {
                    DateTime releaseDateValue;
                    bool isReleaseDateValid = DateTime.TryParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDateValue);

                    if (!isReleaseDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    releaseDate = releaseDateValue;
                }

                Prisoner prisoner = new Prisoner()
                {
                    //"FullName": "", 
                    //"Nickname": "The Wallaby", 
                    //"Age": 32, 
                    //"IncarcerationDate": "29/03/1957", 
                    //"ReleaseDate": "27/03/2006", 
                    //"Bail": null, 
                    //"CellId": 5, 
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId
                };

                foreach (ImportPrisonerMailDto  mailDto in prisonerDto.Mails)
                {
                    if (!IsValid(mailDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Mail mail = new Mail()
                    {
                        //Description": "Invalid FullName", 
                        //"Sender": "Invalid Sender", 
                        //"Address": "No Address"
                        Description = mailDto.Description,
                        Sender = mailDto.Sender,
                        Address = mailDto.Address
                    };
                    prisoner.Mails.Add(mail);
                }
                prisoners.Add(prisoner);
                sb.AppendLine(String.Format(SuccessfullyImportedPrisoner, prisoner.FullName, prisoner.Age));
            }
            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

       
        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportOfficerDto[] officerDtos =
                xmlHelper.Deserialize<ImportOfficerDto[]>
                    (xmlString, "Officers");
            StringBuilder sb = new StringBuilder();
            List<Officer> officers = new List<Officer>();

            //string[] validEnumPosition = new string[] { "Overseer", "Guard", "Watcher", "Labour" };
            //string[] validEnumWeapon = new string[] { "Knife", "FlashPulse", "ChainRifle", "Pistol", "Sniper" };
            foreach (ImportOfficerDto officerDto in officerDtos)
            {
                //var uniqueOfficer = officers.FirstOrDefault(x => x.OfficerName == dto.OfficerName);|| uniqueOfficer != null
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                object positionTypeObj;
                bool isPositionTypeValid = Enum.TryParse(typeof(Position), officerDto.Position, out positionTypeObj);
                if (!isPositionTypeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                object weaponTypeObj;
                bool isWeaponTypeValid = Enum.TryParse(typeof(Weapon), officerDto.Weapon, out weaponTypeObj);
                if (!isWeaponTypeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Officer officer = new Officer()
                {
                    //<Name>Minerva Kitchingman</Name>
                    //<Money>2582</Money>
                    //<Position>Invalid</Position>
                    //<Weapon>ChainRifle</Weapon>
                    //<DepartmentId>2</DepartmentId>
                    FullName = officerDto.Name,
                    Salary = officerDto.Money,
                    Position = Enum.Parse<Position>(officerDto.Position),
                    Weapon = Enum.Parse<Weapon>(officerDto.Weapon),
                    DepartmentId = officerDto.DepartmentId
                };

                foreach (ImportOfficerPrisonerDto prisonerDto in officerDto.Prisoners)
                {
                    if (!IsValid(prisonerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    OfficerPrisoner officerPrisoner = new OfficerPrisoner()
                    {
                        Officer = officer,
                        PrisonerId = prisonerDto.PrisonerId
	   
                    };
                    officer.OfficerPrisoners.Add(officerPrisoner);
                }
                officers.Add(officer);
                sb.AppendLine(String.Format(SuccessfullyImportedOfficer, officer.FullName, officer.OfficerPrisoners.Count));
            }
            context.Officers.AddRange(officers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}