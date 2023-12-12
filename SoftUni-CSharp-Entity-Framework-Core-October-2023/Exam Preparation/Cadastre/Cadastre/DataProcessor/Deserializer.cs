using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Cadastre.Data;
using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using Cadastre.DataProcessor.ImportDtos;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor
{
    using Cadastre.Utilities;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedDistrict = "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen = "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportDistrictDto[] districtDtos = xmlHelper.Deserialize<ImportDistrictDto[]>(xmlDocument, "Districts");
            StringBuilder sb = new StringBuilder();
            string[] validEnumRegionTypes = new string[] { "SouthEast", "SouthWest", "NorthEast", "NorthWest" };
            List<District> districts = new List<District>();

            foreach (ImportDistrictDto districtDto in districtDtos)
            {
                if (!IsValid(districtDto) || !validEnumRegionTypes.Contains(districtDto.Region) ||
                    dbContext.Districts.Any(d => d.Name == districtDto.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                District district = new District()
                {
                    Region = (Region)Enum.Parse(typeof(Region), districtDto.Region),
                    Name = districtDto.Name,
                    PostalCode = districtDto.PostalCode
                };

                foreach (ImportDistrictPropertyDto propertyDto in districtDto.Properties)
                {
                    if (!IsValid(propertyDto) || dbContext.Properties.Any(p => p.PropertyIdentifier == propertyDto.PropertyIdentifier || dbContext.Properties.Any(p => p.Address == propertyDto.Address)))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // Extra validation for unique PropertyIdentifier within the district
                    if (district.Properties.Any(p => p.PropertyIdentifier == propertyDto.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    // Extra validation for unique Address within the district
                    if (district.Properties.Any(p => p.Address == propertyDto.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Property property = new Property()
                    {
                        PropertyIdentifier = propertyDto.PropertyIdentifier,
                        Area = propertyDto.Area,
                        Details = propertyDto.Details,
                        Address = propertyDto.Address,
                        DateOfAcquisition = DateTime.ParseExact(propertyDto.DateOfAcquisition, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    };
                    district.Properties.Add(property);
                }

                dbContext.Districts.Add(district);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, district.Name, district.Properties.Count));
            }
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            ImportCitizenDto[] citizenDtos = JsonConvert.DeserializeObject<ImportCitizenDto[]>(jsonDocument);
            StringBuilder sb = new StringBuilder();
            string[] validEnumMaritalStatusTypes = new string[] { "Unmarried", "Married", "Divorced", "Widowed" };

            foreach (ImportCitizenDto citizenDto in citizenDtos)
            {
                if (!IsValid(citizenDto) || !validEnumMaritalStatusTypes.Contains(citizenDto.MaritalStatus))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen citizen = new Citizen()
                {
                    FirstName = citizenDto.FirstName,
                    LastName = citizenDto.LastName,
                    BirthDate = DateTime.ParseExact(citizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), citizenDto.MaritalStatus)
                };

                foreach (var propertyId in citizenDto.Properties)
                {
                    
                    Property property = dbContext.Properties.Find(propertyId);
                    if (property == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (citizen.PropertiesCitizens.Any(pc => pc.PropertyId == propertyId))
                    {
                        //sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    citizen.PropertiesCitizens.Add(new PropertyCitizen()
                    {
                        Citizen = citizen,
                        PropertyId = propertyId
                    });
                }

                dbContext.Citizens.Add(citizen);
                sb.AppendLine(string.Format(SuccessfullyImportedCitizen, citizen.FirstName, citizen.LastName, citizen.PropertiesCitizens.Count));
            }

            dbContext.SaveChanges();
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
