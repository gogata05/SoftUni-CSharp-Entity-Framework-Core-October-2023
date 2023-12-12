using System.Globalization;
using System.Text;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using Footballers.DataProcessor.ImportDto;
using Footballers.Utilities;
using Newtonsoft.Json;
namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";
        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";
        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportCoachDto[] coachDtos =
                xmlHelper.Deserialize<ImportCoachDto[]>
                    (xmlString, "Coaches");//root
            StringBuilder sb = new StringBuilder();
            List<Coach> coaches = new List<Coach>();
            foreach (ImportCoachDto coachDto in coachDtos)
            {
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Coach coach = new Coach()
                {
                    //<Name>S</Name>
                    //<Nationality>25/01/2018</Nationality>
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality,
                };
                foreach (ImportCoachFootballerDto  footballerDto in coachDto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime startDate;//default value 01/01/0001
                    bool isContractStartDateValid = DateTime.TryParseExact(footballerDto.ContractStartDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);//HH:mm
                    if (!isContractStartDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    DateTime? endDate = null;
                    if (!String.IsNullOrWhiteSpace(footballerDto.ContractEndDate))
                    {
                        DateTime endDateValue;
                        bool isContractEndDateValid = DateTime.TryParseExact(footballerDto.ContractEndDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateValue);
                        if (!isContractEndDateValid)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        endDate = endDateValue;
                    }
                    if (startDate > endDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    
                    Footballer footballer = new Footballer()
                    {
                        //<Name>Benjamin Bourigeaud</Name>
                        //<ContractStartDate>22/03/2020</ContractStartDate>
                        //<ContractEndDate>24/02/2026</ContractEndDate>
                        //<BestSkillType>2</BestSkillType>
                        //<PositionType>2</PositionType>
                        Name = footballerDto.Name,
                        ContractStartDate = startDate,
                        ContractEndDate = endDate ?? DateTime.MinValue,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType,
                    };
                    coach.Footballers.Add(footballer);
                }
                coaches.Add(coach);
                sb.AppendLine(String.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }
            context.Coaches.AddRange(coaches);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            ImportTeamDto[] teamDtos = 
                JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Team> teams = new List<Team>();
            foreach (ImportTeamDto teamDto in teamDtos)
            {
                if (!IsValid(teamDto) || teamDto.Trophies==0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Team team = new Team()
                {
                    //TO DO IMPORT TO DB
                    //Name = teamDto.Name,
                    //Name": "Brentford F.C.",
                    //"Nationality": "The United Kingdom",
                    //"Trophies": "5",
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies,
                };

                foreach (var footballerId in teamDto.Footballers.Distinct())
                { 
                    Footballer footballer = context.Footballers.Find(footballerId);
                    if (footballer == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    team.TeamsFootballers.Add(new TeamFootballer()
                    {
                        Team = team,
                        Footballer = footballer,
                    });
                }
                teams.Add(team);
                sb.AppendLine(String.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }
            context.Teams.AddRange(teams);
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
