namespace Footballers.DataProcessor
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
    using DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;
    using AutoMapper.QueryableExtensions;
    using Footballers.Utilities;
    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<FootballersProfile>();
            //});
            //var mapper = new Mapper(config);
            //ExportCoachDto[] coachDtos = context
            //    .Coaches
            //    .Where(c => c.Footballers.Any())
            //    .ProjectTo<ExportCoachDto>(config)
            //    .OrderByDescending(c => c.FootballersCount)
            //    .ThenBy(c => c.Name)
            //    .ToArray();
            //XmlHelper xmlHelper = new XmlHelper();
            //return xmlHelper.Serialize(coachDtos, "Coaches");


            ExportCoachDto[] coaches = context.Coaches
                .Where(x => x.Footballers.Any())
                .ToArray()
                .Select(x => new ExportCoachDto()
                {
                    //FootballersCount = "5" >
                    //< CoachName > Pep Guardiola </ CoachName >
                    //< Footballers >
                    FootballersCount = x.Footballers.Count(),
                    CoachName = x.Name,
                    Footballers = x.Footballers
                    .ToArray()
                    .OrderBy(y => y.Name)
                    .Select(y => new ExportCoachFootballerDto()
                    {
                        //< Name > Bernardo Silva </ Name >
                        //< Position > Midfielder </ Position >
                        Name = y.Name,
                        Position = y.PositionType.ToString()
                    })
                    .ToArray()
                })
                .OrderByDescending(x => x.Footballers.Length)
                .ThenBy(x => x.CoachName)
                .ToArray();
            XmlHelper xmlHelper = new XmlHelper();
            return xmlHelper.Serialize(coaches, "Coaches");
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Where(x => x.TeamsFootballers.Any(y => y
                    .Footballer.ContractStartDate >= date))
                .ToArray()

                .Select(x => new
                {
                    x.Name,
                    Footballers = x.TeamsFootballers.Where(y => y.Footballer.ContractStartDate >= date)
                    .ToArray()

                    .OrderByDescending(y => y.Footballer.ContractEndDate)
                    .ThenBy(y => y.Footballer.Name)
                    .Select(y => new
                    {
                        //"FootballerName": "Phil Foden", 
                        //"ContractStartDate": "12/30/2021", 
                        //"ContractEndDate": "04/13/2025", 
                        //"BestSkillType": "Dribble", 
                        //"PositionType": "Midfielder" 
                        FootballerName = y.Footballer.Name,
                        ContractStartDate = y.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = y.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = y.Footballer.BestSkillType.ToString(),
                        PositionType = y.Footballer.PositionType.ToString()
                    })
                .ToArray()
                })

            .OrderByDescending(x => x.Footballers.Length)
                .ThenBy(x => x.Name)

                .Take(5)
                .ToArray();
            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }

}