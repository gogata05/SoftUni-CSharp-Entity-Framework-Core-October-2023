using System.Text;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Boardgames.Utilities;
namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using Boardgames.Data;
    using Newtonsoft.Json;
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";
        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";
        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportCreatorDto[] creatorDtos =
                xmlHelper.Deserialize<ImportCreatorDto[]>
                    (xmlString, "Creators");//root
            StringBuilder sb = new StringBuilder();
            List<Creator> creators = new List<Creator>();
            foreach (ImportCreatorDto creatorDto in creatorDtos)
            {
                //var creatorUniqueNameCheck = creators.FirstOrDefault(x => x.CreatorName == creatorDto.CreatorName);|| creatorUniqueNameCheck != null
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Creator creator = new Creator()
                {
                    //<FirstName>Debra</FirstName>
                    //<LastName>Edwards</LastName>
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName,
                };
                foreach (ImportCreatorBoardgameDto boardgameDto in creatorDto.Boardgames)
                {
                    if (!IsValid(boardgameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Boardgame boardgame = new Boardgame()
                    {
                        //<Name>4 Gods</Name>
                        //<Rating>7.28</Rating>
                        //<YearPublished>2017</YearPublished>
                        //<CategoryType>4</CategoryType>
                        //<Mechanics>Area Majority / Influence, Hand Management, Set Collection, Simultaneous Action Selection, Worker Placement</Mechanics>
                        Name = boardgameDto.Name,
                        Rating = boardgameDto.Rating,
                        YearPublished = boardgameDto.YearPublished,
                        CategoryType = (CategoryType)boardgameDto.CategoryType,
                        Mechanics = boardgameDto.Mechanics,
                    };
                    creator.Boardgames.Add(boardgame);
                }
                creators.Add(creator);
                sb.AppendLine(String.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName,creator.Boardgames.Count));//?
            }
            context.Creators.AddRange(creators);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            ImportSellerDto[] sellerDtos = 
                JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            //string[] validEnumTypes = new string[] { "Howitzer", "Mortar"};// Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre), for the class// || !validEnumCategoryType.Contains()
            List<Seller> sellers = new List<Seller>();
            foreach (ImportSellerDto sellerDto in sellerDtos)
            {
                //var sellerUniqueNameCheck = sellers.FirstOrDefault(x => x.SellerName == sellerDto.SellerName);|| sellerUniqueNameCheck != null
                if (!IsValid(sellerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                //if (clientDto.Type == "usual")//if there are such a thing
                //{
                //    sb.AppendLine(ErrorMessage);
                //    continue;
                //}
                Seller seller = new Seller()
                {
                    //"Name": "6am",
                    //"Address": "The Netherlands",
                    //"Country": "Belgium",
                    //"Website": "www.6pm.com",
                    Name = sellerDto.Name,
                    Address = sellerDto.Address,
                    Country = sellerDto.Country,
                    Website = sellerDto.Website,
                };

                foreach (var boardgameId in sellerDto.Boardgames.Distinct())
                { 
                    Boardgame boardgame = context.Boardgames.Find(boardgameId);
                    if (boardgame == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    seller.BoardgamesSellers.Add(new BoardgameSeller()
                    {
                        //Seller = seller,
                        Seller = seller,
                        Boardgame = boardgame,
                    });
                }
                sellers.Add(seller);
                sb.AppendLine(String.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count));
            }
            context.Sellers.AddRange(sellers);
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
