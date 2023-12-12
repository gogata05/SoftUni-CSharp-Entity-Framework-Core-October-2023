using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ExportDto;
using Trucks.Utilities;
namespace Trucks.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            ExportDespatcherDto[] despatchers = context.Despatchers
                .Where(x => x.Trucks.Any())
                .ToArray()

                .Select(x => new ExportDespatcherDto
                {
                    TrucksCount = x.Trucks.Count,
                    DespatcherName = x.Name,
                    Trucks = x.Trucks
                    .ToArray()

                    .OrderBy(x => x.RegistrationNumber)

                    .Select(y => new ExportDespatcherTruckDto()//dto2
                    {
                    //<RegistrationNumber>CT2462BX</RegistrationNumber>
                    //<Make>Scania</Make>
                    RegistrationNumber = y.RegistrationNumber,
                    Make = y.MakeType.ToString(),
                    }).ToArray()

                })
                .OrderByDescending(x => x.Trucks.Length)//Length or Count
                .ThenBy(x => x.DespatcherName)
                .ToArray();

            XmlHelper xmlHelper = new XmlHelper();
            return xmlHelper.Serialize(despatchers, "Despatchers");
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context.Clients
                .Where(x => x.ClientsTrucks.Any(y => y.Truck.TankCapacity >= capacity))
                .ToArray()

                .Select(x => new
                {
                    //Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    //Genre = x.Genre.ToString(), 

                    Name = x.Name,
                    Trucks = x.ClientsTrucks
                        .Where(y => y.Truck.TankCapacity >= capacity)
                    .ToArray()
                        
                    .OrderBy(x => x.Truck.MakeType.ToString())
                    .ThenByDescending(x => x.Truck.CargoCapacity)

                    .Select(y => new
                    {
                        //TruckRegistrationNumber = y.Truck.RegistrationNumber,
                        //MakeType = y.Truck.MakeType.ToString(),
                        //ContractStartDate = y.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        //Range = y.Range > 3000 ? "Long-range" : "Regular range",//for bool/DateTime HasValue ? ...
                        //Tags = String.Join(", ", y.GameTags.Select(y => y.Tag.Name).ToArray()),

                        //"TruckRegistrationNumber": "CT5206MM",
                        //"VinNumber": "WDB96341311261287",
                        //"TankCapacity": 1420,
                        //"CargoCapacity": 28058,
                        //"CategoryType": "Flatbed",
                        //"MakeType": "Daf"
                        TruckRegistrationNumber = y.Truck.RegistrationNumber,
                        VinNumber = y.Truck.VinNumber,
                        TankCapacity = y.Truck.TankCapacity,
                        CargoCapacity = y.Truck.CargoCapacity,
                        CategoryType = y.Truck.CategoryType.ToString(),
                        MakeType = y.Truck.MakeType.ToString(),
                    })
                .ToArray(),
            //TotalEntitySomething = Math.Round(x.EntityManyToMany.Sum(y => y.Entity.Something), 2)
            })

            .OrderByDescending(x => x.Trucks.Length)//Count
                .ThenBy(x => x.Name)

                .Take(10)//?
                .ToArray();
            return JsonConvert.SerializeObject(clients, Formatting.Indented);
        }
    }
}
