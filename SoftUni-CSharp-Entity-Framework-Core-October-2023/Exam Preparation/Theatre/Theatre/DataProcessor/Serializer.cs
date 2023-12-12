using Newtonsoft.Json;
using Theatre.Data.Models;
namespace Theatre.DataProcessor
{
    using System;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;
    using Theatre.Utilities;
    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theaters = context.Theatres
                .Where(x => x.NumberOfHalls >= numbersOfHalls && x.Tickets.Count >= 20)
                .ToArray()

                .Select(x => new
                {
                    //"Name": "Capitol Theatre Building", 
                    //"Halls": 10, 
                    //"TotalIncome": 860.02, 
                    //"Tickets": [
                    x.Name,
                    Halls = x.NumberOfHalls,
                    TotalIncome = x.Tickets.Where(x => x.RowNumber <= 5).Sum(x => x.Price),
                    Tickets = x.Tickets.Where(x => x.RowNumber <= 5)
                     .ToArray()
                     .OrderByDescending(y => y.Price)
                     .Select(y => new
                     {
                         //"Price": 93.48, 
                         //"RowNumber": 3
                         Price = y.Price,
                         RowNumber = y.RowNumber
                     })
                     .ToArray()
                })
                .OrderByDescending(x => x.Halls)
                .ThenBy(x => x.Name)
                .ToArray();
            return JsonConvert.SerializeObject(theaters, Formatting.Indented);
        }

        public static string ExportPlays(TheatreContext context, double raiting)
        {
            ExportPlayDto[] plays = context.Plays
            .Where(x => x.Rating <= raiting)
                .ToArray()

                .Select(x => new ExportPlayDto
                {
                    //<Play Title="A Raisin in the Sun" Duration="01:40:00" Rating="5.4" Genre="Drama">
                    //<Actors>
                    Title = x.Title,
                    Duration = x.Duration.ToString("c"),//!
                    Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),//!
                    Genre = x.Genre.ToString(),
                    Actors = x.Casts
                        .Where(x => x.IsMainCharacter)
                        .ToArray()
                        .OrderByDescending(y => y.FullName)
                        .Select(y => new ExportPlayCastDto()
                        {
                            // <Actor FullName="Sylvia Felipe" MainCharacter="Plays main character in 'A Raisin in the Sun'." /> 
                            FullName = y.FullName,
                            MainCharacter = $"Plays main character in '{x.Title}'."
                        })
                        .ToArray(),
                })
                .OrderBy(x => x.Title)
                .ThenByDescending(x => x.Genre)
                .ToArray();

            XmlHelper xmlHelper = new XmlHelper();
            return xmlHelper.Serialize(plays, "Plays");
        }
    }
}
