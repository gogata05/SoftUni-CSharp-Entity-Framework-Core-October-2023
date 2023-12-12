namespace Theatre.DataProcessor
{
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Theatre.Data.Models;
using Theatre.Data.Models;
using Theatre.Data.Models.Enums;
using Theatre.DataProcessor.ImportDto;
using Theatre.Utilities;
using System.ComponentModel.DataAnnotations;
using Theatre.Data;
public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";
        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";
        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";
        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportPlayDto[] playDtos =
                xmlHelper.Deserialize<ImportPlayDto[]>
                    (xmlString, "Plays");//root
            StringBuilder sb = new StringBuilder();
            List<Play> plays = new List<Play>();

            string[] validEnumTypes = new string[] { "Drama", "Comedy", "Romance", "Musical" };

            TimeSpan minimumTime = new TimeSpan(1, 0, 0);//duration/P1DT2H30M/initialization//outside the loop

            foreach (ImportPlayDto playDto in playDtos)
            {
                //var playUniqueNameCheck = plays.FirstOrDefault(x => x.PlayName == playDto.PlayName);|| playUniqueNameCheck != null

                TimeSpan currentTime =
                    TimeSpan.ParseExact(playDto.Duration, "c", CultureInfo.InvariantCulture);//move in the loop

                // || currentTime < minimumTime //validation in the loop

                //Duration = TimeSpan.ParseExact(dto.Duration, "c", CultureInfo.InvariantCulture),//move in the class
                if (!IsValid(playDto) || !validEnumTypes.Contains(playDto.Genre) || currentTime < minimumTime)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Play play = new Play()
                {
                    //<Title>The Hsdfoming</Title>
                    //<Duration>03:40:00 </ Duration >
                    //< Raiting > 8.2 </ Raiting >
                    //< Genre > Action </ Genre >
                    //< Description > A guyat Pinter turns into a debatable conundrum as oth ordinary.</ Description >
                    //< Screenwriter > Roger Nciotti </ Screenwriter >
                    Title = playDto.Title,
                    Duration = TimeSpan.ParseExact(playDto.Duration, "c", CultureInfo.InvariantCulture),//?
                    Rating = playDto.Raiting,
                    Genre = (Genre)Enum.Parse(typeof(Genre), playDto.Genre),
                    Description = playDto.Description,
                    Screenwriter = playDto.Screenwriter
                };
                plays.Add(play);
                sb.AppendLine(String.Format(SuccessfulImportPlay, play.Title, play.Genre,play.Rating));
            }
            context.Plays.AddRange(plays);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportCastDto[] castDtos =
                xmlHelper.Deserialize<ImportCastDto[]>
                    (xmlString, "Casts");//root
            StringBuilder sb = new StringBuilder();
            List<Cast> casts = new List<Cast>();

            foreach (ImportCastDto castDto in castDtos)
            {
                //var castUniqueNameCheck = casts.FirstOrDefault(x => x.CastName == castDto.CastName);|| castUniqueNameCheck != null
                if (!IsValid(castDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Cast cast = new Cast()
                {
                    //<FullName>Van Tyson</FullName>
                    //<IsMainCharacter>false</IsMainCharacter>
                    //<PhoneNumber>+44-35-745-2774</PhoneNumber>
                    //<PlayId>26</PlayId>
                    FullName = castDto.FullName,
                    IsMainCharacter = castDto.IsMainCharacter == "true" ? true : false,//!
                    PhoneNumber = castDto.PhoneNumber,
                    PlayId = castDto.PlayId
                };
                casts.Add(cast);
                sb.AppendLine(String.Format(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter ? "main" : "lesser"));
            }
            context.Casts.AddRange(casts);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            ImportTheatreDto[] theatreTicketDtos = 
                JsonConvert.DeserializeObject<ImportTheatreDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Theatre> theatreTickets = new List<Theatre>();
            foreach (ImportTheatreDto theatreDto in theatreTicketDtos)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Theatre theatre = new Theatre()
                {
                    //"Name": "",
                    //"NumberOfHalls": 7,
                    //"Director": "Ulwin Mabosty",
                    //"Tickets": 
                    Name = theatreDto.Name,
                    NumberOfHalls = theatreDto.NumberOfHalls,
                    Director = theatreDto.Director,
                };

                foreach (ImportTheatreTicketDto entity2Dto in theatreDto.Tickets)
                {
                    if (!IsValid(entity2Dto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Ticket entity2 = new Ticket()
                    {
                        //"Price": 7.63,
                        //"RowNumber": 5,
                        //"PlayId": 4
                        Price = entity2Dto.Price,
                        RowNumber = entity2Dto.RowNumber,
                        PlayId = entity2Dto.PlayId,
                    };
                    theatre.Tickets.Add(entity2);
                }
                theatreTickets.Add(theatre);
                sb.AppendLine(String.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));//?
            }
            context.Theatres.AddRange(theatreTickets);
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
