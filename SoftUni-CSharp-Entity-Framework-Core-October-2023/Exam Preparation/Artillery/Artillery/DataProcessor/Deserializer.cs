using System.ComponentModel.DataAnnotations;
using System.Text;
using Artillery.Data.Models;
using Artillery.DataProcessor.ImportDto;
using Artillery.Utilities;
using Newtonsoft.Json;
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";
        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportCountryDto[] countryDtos =
                xmlHelper.Deserialize<ImportCountryDto[]>
                    (xmlString, "Countries");//root
            StringBuilder sb = new StringBuilder();
            //string[] validEnumTypes = new string[] { "Howitzer", "Mortar"};// Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre), for the class// || !validEnumCategoryType.Contains()
            List<Country> countries = new List<Country>();
            foreach (ImportCountryDto entityDto in countryDtos)
            {
                //var entityUniqueNameCheck = entitys.FirstOrDefault(x => x.CountryName == entityDto.CountryName);|| entityUniqueNameCheck != null
                if (!IsValid(entityDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Country country = new Country()
                {
                    //<CountryName>Afghanistan</CountryName>
                    //<ArmySize>1697064</ArmySize>
                    CountryName = entityDto.CountryName,
                    ArmySize = entityDto.ArmySize

                };
                countries.Add(country);
                sb.AppendLine(String.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }
            context.Countries.AddRange(countries);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportManufacturerDto[] manufacturerDtos =
                xmlHelper.Deserialize<ImportManufacturerDto[]>
                    (xmlString, "Manufacturers");//root
            StringBuilder sb = new StringBuilder();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (ImportManufacturerDto manufacturerDto in manufacturerDtos)
            {
                var manufacturerUniqueNameCheck = manufacturers.FirstOrDefault(x => x.ManufacturerName == manufacturerDto.ManufacturerName);//Unique names only

                if (!IsValid(manufacturerDto) || manufacturerUniqueNameCheck != null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Manufacturer manufacturer = new Manufacturer()
                {
                    //<ManufacturerName>BAE Systems</ManufacturerName>
                    //<Founded>30 November 1999, London, England</Founded>//
                    ManufacturerName = manufacturerDto.ManufacturerName,
                    Founded = manufacturerDto.Founded
                };
                manufacturers.Add(manufacturer);
                //Split names:
                string[] manufacturerCountry = manufacturer.Founded.Split(", ").ToArray();//Split by ", "
                string[] last = manufacturerCountry.Skip(Math.Max(0, manufacturerCountry.Count() - 2)).ToArray();//take last 2 elements

                sb.AppendLine(String.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, string.Join(", ", last)));//print the Splited array called "last"
            }
            context.Manufacturers.AddRange(manufacturers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportShellDto[] shellDtos =
                xmlHelper.Deserialize<ImportShellDto[]>
                    (xmlString, "Shells");//root
            StringBuilder sb = new StringBuilder();
            //string[] validEnumTypes = new string[] { "Howitzer", "Mortar"};// Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre), for the class// || !validEnumCategoryType.Contains()
            List<Shell> shells = new List<Shell>();
            foreach (ImportShellDto shellDto in shellDtos)
            {
                //var shellUniqueNameCheck = shells.FirstOrDefault(x => x.ShellName == shellDto.ShellName);|| shellUniqueNameCheck != null
                if (!IsValid(shellDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Shell shell = new Shell()
                {
                    //<ShellWeight>50</ShellWeight>
                    //<Caliber>155 mm (6.1 in)</Caliber>
                    ShellWeight = shellDto.ShellWeight,
                    Caliber = shellDto.Caliber
                };
                shells.Add(shell);
                sb.AppendLine(String.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
            }
            context.Shells.AddRange(shells);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            ImportGunDto[] gunDtos = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Gun> guns = new List<Gun>();
            string[] validGunTypes = new[] { "Howitzer", "Mortar", "FieldGun", "AntiAircraftGun", "MountainGun", "AntiTankGun" };//enum string
            foreach (ImportGunDto gunDto in gunDtos)
            {
               
                if (!IsValid(gunDto) || !validGunTypes.Contains(gunDto.GunType))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Gun gun = new Gun()
                {
                    //"ManufacturerId": 14, 
                    //"GunWeight": 531616, 
                    //"BarrelLength": 6.86, 
                    //"NumberBuild": 287, 
                    //"Range": 120000, 
                    //"GunType": "Howitzer", 
                    //"ShellId": 41, 
                    ManufacturerId = gunDto.ManufacturerId,
                    GunWeight = gunDto.GunWeight,
                    BarrelLength = gunDto.BarrelLength,
                    NumberBuild = gunDto.NumberBuild,
                    Range = gunDto.Range,
                    GunType = (GunType)Enum.Parse(typeof(GunType), gunDto.GunType),
                    ShellId = gunDto.ShellId
                };

                foreach (var countryDto in gunDto.Countries)
                {
                    gun.CountriesGuns.Add(new CountryGun()
                    {
                        //Gun = gun,
                        //CountrieId = countryDto.CountrieId
                        Gun = gun,
                        CountryId = countryDto.Id
                    });
                }
                guns.Add(gun);
                sb.AppendLine(String.Format(SuccessfulImportGun, gun.GunType, gun.GunWeight, gun.BarrelLength));
            }
            context.Guns.AddRange(guns);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}