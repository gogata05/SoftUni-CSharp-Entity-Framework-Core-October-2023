
using Artillery.DataProcessor.ExportDto;
using Artillery.Utilities;
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Newtonsoft.Json;
    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells
                .Where(x => x.ShellWeight > shellWeight)
                .Select(x => new
                {
                    ShellWeight = x.ShellWeight,
                    Caliber = x.Caliber,
                    Guns = x.Guns
                        .Where(g => g.GunType == GunType.AntiAircraftGun)//where enum is AntiAircraftGun
                        .OrderByDescending(g => g.GunWeight) 
                        .Select(g => new
                        {
                            GunType = g.GunType.ToString(),
                            GunWeight = g.GunWeight,
                            BarrelLength = g.BarrelLength,
                            Range = g.Range > 3000 ? "Long-range" : "Regular range",
                        })
                        .ToArray(),
                })
                .OrderBy(x => x.ShellWeight)
                .ToArray();
            return JsonConvert.SerializeObject(shells, Formatting.Indented);
        }


        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            ExportGunDto[] guns = context.Guns
                .Where(x => x.Manufacturer.ManufacturerName == manufacturer)//any?
                .ToArray()

                .Select(x => new ExportGunDto
                {
                    //Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    //Genre = x.Genre.ToString(), 

                    //<Gun Manufacturer="Krupp" GunType="Mortar" GunWeight="1291272" BarrelLength="8.31" Range="14258">
                    //<Countries>
                    Manufacturer = x.Manufacturer.ManufacturerName,
                    GunType = x.GunType.ToString(),
                    GunWeight = x.GunWeight,
                    BarrelLength = x.BarrelLength,
                    Range = x.Range,
                    Countries = x.CountriesGuns
                        .Where(y => y.Country.ArmySize > 4500000)
                    
                    .ToArray()
                        .OrderBy(x => x.Country.ArmySize)

                        .Select(y => new ExportGunCountryDto()
                    {
                    //TruckRegistrationNumber = y.Truck.RegistrationNumber,
                    //MakeType = y.Truck.MakeType.ToString(),
                    //ContractStartDate = y.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                    //Range = y.Range > 3000 ? "Long-range" : "Regular range",//for bool/DateTime HasValue ? ...
                    //Tags = String.Join(", ", y.GameTags.Select(y => y.Tag.Name).ToArray()),

                    //<Country Country="Sweden" ArmySize="5437337" />
                    //<Country Country="Portugal" ArmySize="9523599" />
                    Country = y.Country.CountryName,
                    ArmySize = y.Country.ArmySize
                    })
                .ToArray(),
                })

            .OrderBy(x => x.BarrelLength)//Count or Length
                .ToArray();

            XmlHelper xmlHelper = new XmlHelper();
            return xmlHelper.Serialize(guns, "Guns");
        }
    }
}
