namespace VaporStore.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using DataProcessor.ImportDto;
    using VaporStore.Utilities;

    public static class Deserializer
    {
        public const string ErrorMessage = "Invalid Data";
        public const string SuccessfullyImportedGame = "Added {0} ({1}) with {2} tags";
        public const string SuccessfullyImportedUser = "Imported {0} with {1} cards";
        public const string SuccessfullyImportedPurchase = "Imported {0} for {1}";
        //DateTime
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            ImportGameDto[] gameDtos =
                JsonConvert.DeserializeObject<ImportGameDto[]>
                    (jsonString);

            StringBuilder sb = new StringBuilder();
            List<Game> games = new List<Game>();
            List<Developer> developers = new List<Developer>();
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();
            foreach (ImportGameDto gameDto in gameDtos)
            {
                if (!IsValid(gameDto) || gameDto.Tags.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime releaseDate;
                bool isReleaseDateValid = DateTime.TryParseExact
                 (gameDto.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);
                if (!isReleaseDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Game game = new Game()
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = releaseDate
                };
                

                //when we add something,we check if it exists,if it doesn't,we create it
                //doesn't exist, create it. Check the description for this requirement
                ////NameCheck
                Developer developerNameCheck = developers
                    .FirstOrDefault(d => d.Name == gameDto.Developer);//property check 
                if (developerNameCheck == null)//if not exist,create
                {
                    Developer developer = new Developer()
                    {
                        Name = gameDto.Developer
                    };
                    developers.Add(developer);
                    game.Developer = developer;
                }
                else//if exist,use it
                {
                    game.Developer = developerNameCheck;
                }
                

                //NameCheck
                //if doesn't exist, create it
                Genre genreNameCheck = genres
                    .FirstOrDefault(g => g.Name == gameDto.Genre);//property check 
                if (genreNameCheck == null)//if not exist,create
                {
                    Genre genre = new Genre()
                    {
                        Name = gameDto.Genre
                    };
                    genres.Add(genre);
                    game.Genre = genre;
                }
                else//if exist,use it
                {
                    game.Genre = genreNameCheck;
                }


                foreach (string tagName in gameDto.Tags)
                {
                    if (String.IsNullOrEmpty(tagName))
                    {
                        continue;
                    }

                    Tag tagNameCheck = tags
                        .FirstOrDefault(t => t.Name == tagName);
                    if (tagNameCheck == null)
                    {
                        Tag tag = new Tag()
                        {
                            Name = tagName
                        };

                        tags.Add(tag);
                        game.GameTags.Add(new GameTag()
                        {
                            Game = game,
                            Tag = tag
                        });
                    }
                    else
                    {
                        game.GameTags.Add(new GameTag()
                        {
                            Game = game,
                            Tag = tagNameCheck
                        });
                    }
                }
                if (game.GameTags.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                games.Add(game);
                sb.AppendLine(String.Format(SuccessfullyImportedGame, game.Name, game.Genre.Name, game.GameTags.Count));
            }
            context.Games.AddRange(games);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        //Enum.TryParse
        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<User> users = new List<User>();
            foreach (ImportUserDto userDto in userDtos)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                List<Card> userCards = new List<Card>();
                bool areAllCardsValid = true;
                foreach (ImportUserCardDto cardDto in userDto.Cards)
                {
                    if (!IsValid(cardDto))
                    {
                        areAllCardsValid = false;
                        break;
                    }

                    //simple array works a
                    Object cardTypeRes;
                    bool isCardTypeValid = Enum.TryParse(typeof(CardType), cardDto.Type, out cardTypeRes);

                    if (!isCardTypeValid)
                    {
                        areAllCardsValid = false;
                        break;
                    }

                    userCards.Add(new Card()
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.Cvc,
                        Type = (CardType)cardTypeRes
                    });
                }
                if (!areAllCardsValid || (userCards.Count == 0))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                User user = new User()
                {
                    Username = userDto.UserName,
                    FullName = userDto.FullName,
                    Email = userDto.Email,
                    Age = userDto.Age,
                    Cards = userCards
                };
                users.Add(user);
                sb.AppendLine(String.Format(SuccessfullyImportedUser, user.Username, user.Cards.Count));
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        //DateTime
        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportPurchaseDto[] purchaseDtos =
                xmlHelper.Deserialize<ImportPurchaseDto[]>
                    (xmlString, "Purchases");

            StringBuilder sb = new StringBuilder();
            List<Purchase> purchases = new List<Purchase>();
            foreach (ImportPurchaseDto purchaseDto in purchaseDtos)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                object purchaseTypeObj;
                bool isPurchaseTypeValid =
                    Enum.TryParse(typeof(PurchaseType), purchaseDto.Type, out purchaseTypeObj);

                if (!isPurchaseTypeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime date;
                bool isDateValid = DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Card cardNumberCheck = context
                    .Cards
                    .FirstOrDefault(c => c.Number == purchaseDto.Card);
                if (cardNumberCheck == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Game gameNameCheck = context
                    .Games
                    .FirstOrDefault(g => g.Name == purchaseDto.Title);
                if (gameNameCheck == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Purchase purchase = new Purchase()
                {
                    Type = (PurchaseType)purchaseTypeObj,
                    Date = date,
                    ProductKey = purchaseDto.ProductKey,
                    Game = gameNameCheck,
                    Card = cardNumberCheck
                };
                purchases.Add(purchase);
                sb.AppendLine(String.Format(SuccessfullyImportedPurchase, purchase.Game.Name, purchase.Card.User.Username));
            }
            context.Purchases.AddRange(purchases);
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