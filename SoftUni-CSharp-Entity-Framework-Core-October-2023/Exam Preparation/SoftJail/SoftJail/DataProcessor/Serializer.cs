using CarDealer.Utilities;
namespace SoftJail.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using Newtonsoft.Json;
    using System.Xml.Serialization;
    using Data;
    using ExportDto;
    using System.Xml;
    using Formatting = Newtonsoft.Json.Formatting;
    public class Serializer
   {
       public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
       {
           var prisoners = context.Prisoners
               .Where(x => ids.Contains(x.Id))
               .ToArray()
               .Select(x => new
               {
                   //"Id": 3, 
                   //"Name": "Binni Cornhill", 
                   //"CellNumber": 503, 
                   //"Officers": [
                   Id = x.Id,
                   Name = x.FullName,
                   CellNumber = x.Cell.CellNumber,
                   Officers = x.PrisonerOfficers
                       .ToArray()
                       .OrderBy(y => y.Officer.FullName)
                       .Select(y => new
                       {
                           //OfficerName": "Hailee Kennon", 
                           //"Department": "ArtificialIntelligence"
                           OfficerName = y.Officer.FullName,
                           Department = y.Officer.Department.Name
                       })
                    .ToArray(),
                   TotalOfficerSalary = Math.Round(x.PrisonerOfficers.Sum(y => y.Officer.Salary), 2)
               })
               .OrderBy(x => x.Name)
               .ThenBy(x => x.Id)
               .ToArray();
           return JsonConvert.SerializeObject(prisoners, Formatting.Indented);
       }

       public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
       {
           string[] prisonerNamesArr = prisonersNames
               .Split(",")
               .ToArray();//!

           ExportPrisonerDto[] prisoners = context.Prisoners
               .Where(x => prisonerNamesArr.Contains(x.FullName))
               .ToArray()

               .Select(x => new ExportPrisonerDto
               {
                   //<Id>3</Id>
                   //<Name>Binni Cornhill</Name>
                   //<IncarcerationDate>1967-04-29</IncarcerationDate>
                   //<EncryptedMessages>
                   Id = x.Id,
                   Name = x.FullName,
                   IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                   EncryptedMessages = x.Mails
                   .ToArray()
                   .Select(y => new ExportPrisonerMailDto
                   {
                       //Description
                       Description = String.Join("", y.Description.Reverse())
               })
               .ToArray(),
               })
               .OrderBy(x => x.Name)
               .ThenBy(x => x.Id)
               .ToArray();

           XmlHelper xmlHelper = new XmlHelper();
           return xmlHelper.Serialize(prisoners, "Prisoners");
       }
   }
}