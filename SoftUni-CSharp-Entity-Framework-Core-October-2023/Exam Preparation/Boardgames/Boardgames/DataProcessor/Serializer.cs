using Boardgames.DataProcessor.ExportDto;
using Boardgames.Utilities;
namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Data.Models.Enums;
    using Newtonsoft.Json;
    using System.Xml.Linq;
    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            ExportCreatorDto[] creators = context.Creators
                .Where(x => x.Boardgames.Any())
                .ToArray()

                .Select(x => new ExportCreatorDto
                {
                    //Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    //Genre = x.Genre.ToString(), 

                    //<Creator BoardgamesCount="6">
                    //<CreatorName>Cade O'Neill</CreatorName>
                    //<Boardgames>
                    BoardgamesCount = x.Boardgames.Count,
                    CreatorName = x.FirstName + " " + x.LastName,//?
                    Boardgames = x.Boardgames
                    .ToArray()

                    .OrderBy(x => x.Name)
                    .Select(y => new ExportCreatorBoardgameDto()
                    {
                    //TruckRegistrationNumber = y.Truck.RegistrationNumber,
                    //MakeType = y.Truck.MakeType.ToString(),
                    //ContractStartDate = y.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                    //Range = y.Range > 3000 ? "Long-range" : "Regular range",//for bool/DateTime HasValue ? ...
                    //Tags = String.Join(", ", y.GameTags.Select(y => y.Tag.Name).ToArray()),


                    //<BoardgameName>Bohnanza: The Duel</ BoardgameName >
                    //< BoardgameYearPublished > 2019 </ BoardgameYearPublished >
                    BoardgameName = y.Name,
                    BoardgameYearPublished = y.YearPublished,
                    })
                .ToArray(),
                })

            .OrderByDescending(x => x.Boardgames.Length)//Count or Length
                .ThenBy(x => x.CreatorName)
                .ToArray();
            XmlHelper xmlHelper = new XmlHelper();
            return xmlHelper.Serialize(creators, "Creators");
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(x => x.BoardgamesSellers.Any(y => y.Boardgame.YearPublished >= year && y.Boardgame.Rating <= rating))//Any sometimes may not be needed
                //.Where(x => arrayParameter.Contains(x.Prop))//If [] parameter
                //.Where(x => x.Prop >= parameter && x.Boardgame.Count() >= 20))//If normal
                .ToArray()

                .Select(x => new
                {
                    //Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    //Genre = x.Genre.ToString(), 

                    Name = x.Name,
                    Website = x.Website,
                    Boardgames = x.BoardgamesSellers
                    .Where(y => y.Boardgame.YearPublished >= year && y.Boardgame.Rating <= rating)
                    .ToArray()

                    .OrderByDescending(x => x.Boardgame.Rating)
                    .ThenBy(x => x.Boardgame.Name)

                    .Select(y => new
                    {
                        //TruckRegistrationNumber = y.Truck.RegistrationNumber,
                        //MakeType = y.Truck.MakeType.ToString(),
                        //ContractStartDate = y.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        //Range = y.Range > 3000 ? "Long-range" : "Regular range",//for bool/DateTime HasValue ? ...
                        //Tags = String.Join(", ", y.GameTags.Select(y => y.Tag.Name).ToArray()),

                        //"Name": "The Fog of War",
                        //"Rating": 9.65,
                        //"Mechanics": "Grid Movement, Hand Management, Rock-Paper-Scissors, Time Track, Variable Player Powers",
                        //"Category": "Strategy"
                        Name = y.Boardgame.Name,
                        Rating = y.Boardgame.Rating,
                        Mechanics = y.Boardgame.Mechanics,
                        Category = y.Boardgame.CategoryType.ToString(),
                    })
                .ToArray(),
            //TotalEntitySomething = Math.Round(x.EntityManyToMany.Sum(y => y.Entity.Something), 2)
            })

            .OrderByDescending(x => x.Boardgames.Length)//Count or Length
                .ThenBy(x => x.Name)
                
                .Take(5)//?
                .ToArray();
            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}