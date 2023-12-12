using System.Text;
using Newtonsoft.Json;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;
using Trucks.Utilities;
namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Data;
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";
        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";
        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            //var creatorUniqueNameCheck = creators.FirstOrDefault(x => x.CreatorName == creatorDto.CreatorName);|| creatorUniqueNameCheck != null
            XmlHelper xmlHelper = new XmlHelper();
            ImportDespatcherDto[] despatcherDtos =
                xmlHelper.Deserialize<ImportDespatcherDto[]>
                    (xmlString, "Despatchers");//root
            StringBuilder sb = new StringBuilder();
            List<Despatcher> despatchers = new List<Despatcher>();
            foreach (ImportDespatcherDto despatcherDto in despatcherDtos)
            {
                if (!IsValid(despatcherDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Despatcher despatcher = new Despatcher()
                {
                    //<Name>Genadi Petrov</Name>
                    //<Position>Specialist</Position>
                    Name = despatcherDto.Name,
                    Position = despatcherDto.Position,
                };
                foreach (ImportDespatcherTruckDto  truckDto in despatcherDto.Trucks)
                {
                    if (!IsValid(truckDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Truck truck = new Truck()
                    {
                        //<RegistrationNumber>CB0796TP</RegistrationNumber>
                        //<VinNumber>YS2R4X211D5318181</VinNumber>
                        //<TankCapacity>1000</TankCapacity>
                        //<CargoCapacity>23999</CargoCapacity>
                        //<CategoryType>0</CategoryType>
                        //<MakeType>3</MakeType>
                        RegistrationNumber = truckDto.RegistrationNumber,
                        VinNumber = truckDto.VinNumber,
                        TankCapacity = truckDto.TankCapacity,
                        CargoCapacity = truckDto.CargoCapacity,
                        CategoryType = (CategoryType)truckDto.CategoryType,
                        MakeType = (MakeType)truckDto.MakeType,
                    };
                    despatcher.Trucks.Add(truck);
                }
                despatchers.Add(despatcher);
                sb.AppendLine(String.Format(SuccessfullyImportedDespatcher, despatcher.Name, despatcher.Trucks.Count));
            }
            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            ImportClientDto[] clientDtos = 
                JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Client> clients = new List<Client>();
            //string[] validEnumTypes = new string[] { "Howitzer", "Mortar"};// Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre), for the class// || !validEnumCategoryType.Contains()
            foreach (ImportClientDto clientDto in clientDtos)
            {
                //var sellerUniqueNameCheck = sellers.FirstOrDefault(x => x.SellerName == sellerDto.SellerName);|| sellerUniqueNameCheck != null
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (clientDto.Type=="usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Client client = new Client()
                {
                    //"Name": "Kuenehne",
                    //"Nationality": "The Netherlands",
                    //"Type": "golden",
                    Name = clientDto.Name,
                    Nationality = clientDto.Nationality,
                    Type = clientDto.Type,
                };
                foreach (var truckId in clientDto.Trucks.Distinct())
                { 
                    Truck truck = context.Trucks.Find(truckId);
                    if (truck == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    client.ClientsTrucks.Add(new ClientTruck()
                    {
                        Client = client,
                        Truck = truck,
                    });
                }
                clients.Add(client);
                sb.AppendLine(String.Format(SuccessfullyImportedClient, client.Name, client.ClientsTrucks.Count));
            }
            context.Clients.AddRange(clients);
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