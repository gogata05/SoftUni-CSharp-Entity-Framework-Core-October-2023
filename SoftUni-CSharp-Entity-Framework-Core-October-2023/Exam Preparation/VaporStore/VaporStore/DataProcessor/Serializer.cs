using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;
using VaporStore.Utilities;
namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;

    using Newtonsoft.Json;

    using Data;
    using VaporStore.DataProcessor.ExportDto;

   public static class Serializer
   {
       public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
       {
           var genres = context.Genres
               .Where(x =>  genreNames.Contains(x.Name))
               .ToArray()
               .Select(x => new
               {
                   Id = x.Id,
                   Genre = x.Name,
                   Games = x.Games.Where(y => y.Purchases.Any())
                   .ToArray()

                   .OrderByDescending(x => x.Purchases.Count)
                   .ThenBy(x => x.Id)

                   .Select(y => new
                   {
                       Id = y.Id,
                       Title = y.Name,
                       Developer = y.Developer.Name,
                       Tags = String.Join(", ", y.GameTags.Select(x => x.Tag.Name).ToArray()),
                       Players = y.Purchases.Count
                   })
               .ToArray(),
                   TotalPlayers = x.Games.Sum(y => y.Purchases.Count)
               })

           .OrderByDescending(x => x.TotalPlayers)//Count
               .ThenBy(x => x.Id)
               .ToArray();
           return JsonConvert.SerializeObject(genres, Formatting.Indented);
       }

       public static string ExportUserPurchasesByType(VaporStoreDbContext context, string purchaseType)
       {
           PurchaseType purchaseTypeEnum = Enum.Parse<PurchaseType>(purchaseType);//enum parse

           ExportUserDto[] users = context.Users
               .Where(u => u.Cards.Any(c => c.Purchases.Any()))
               .ToArray()
               .Select(u => new ExportUserDto()
               {
                   Username = u.Username,
                   Purchases = context.Purchases
                       .Where(p => p.Card.User.Username == u.Username && p.Type == purchaseTypeEnum)
                       .ToArray()
                       .OrderBy(p => p.Date)
                       .Select(p => new ExportUserPurchaseDto()
                       {
                           CardNumber = p.Card.Number,
                           CardCvc = p.Card.Cvc,
                           Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                           Game = new ExportUserPurchaseGameDto()
                           {
                               Name = p.Game.Name,
                               Genre = p.Game.Genre.Name,
                               Price = p.Game.Price
                           }
                       })
                       .ToArray(),
                   TotalSpent = context.Purchases
                       .ToArray()
                       .Where(p => p.Card.User.Username == u.Username && p.Type == purchaseTypeEnum)
                       .Sum(p => p.Game.Price)
               })
               .Where(u => u.Purchases.Length > 0)
               .OrderByDescending(u => u.TotalSpent)
               .ThenBy(u => u.Username)
               .ToArray();

           XmlHelper xmlHelper = new XmlHelper();
           return xmlHelper.Serialize(users, "Users");
       }
   }
}