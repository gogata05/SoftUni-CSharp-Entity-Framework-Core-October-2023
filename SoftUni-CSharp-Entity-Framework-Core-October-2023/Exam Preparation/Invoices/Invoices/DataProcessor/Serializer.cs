using System;
using System.Linq;
using System.Globalization;
using Invoices.DataProcessor.ExportDto;
using Invoices.Utilities;
using Newtonsoft.Json;
namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Invoices.Data.Models;
    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            ExportClientDto[] clients = context.Clients
                .Where(x => x.Invoices.Any(x => x.IssueDate > date))
                //Any sometimes may not be needed
                //.Where(x => arrayParameter.Contains(x.Prop))//If [] parameter
                //.Where(x => x.Prop >= parameter && x.Invoices.Count() >= 20))//If normal
                .ToArray()

                .Select(x => new ExportClientDto
                {
                    //Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    //Genre = x.Genre.ToString(),
                   
                    //<Client InvoicesCount="9">
                    //<ClientName>SPEDOX,SRO</ClientName>
                    //<VatNumber>SK2023911087</VatNumber>
                    //<Invoices>
                    InvoicesCount = x.Invoices.Count,
                    ClientName = x.Name,
                    VatNumber = x.NumberVat,
                    Invoices = x.Invoices
                        .ToArray()

                        .OrderBy(x => x.IssueDate)
                        .ThenByDescending(x => x.DueDate)

                        .Select(y => new ExportClientInvoiceDto()//dto2
                    {
                            //TruckRegistrationNumber = y.Truck.RegistrationNumber,
                            //MakeType = y.Truck.MakeType.ToString(),
                            //ContractStartDate = y.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                            //Range = y.Range > 3000 ? "Long-range" : "Regular range",//for bool/DateTime HasValue ? ...
                            //Tags = String.Join(", ", y.GameTags.Select(y => y.Tag.Name).ToArray()),

                            //<InvoiceNumber>1063259096</InvoiceNumber>
                            //<InvoiceAmount>167.22</InvoiceAmount>
                            //<DueDate>02/19/2023</DueDate>
                            //<Currency>EUR</Currency>
                            InvoiceNumber = y.Number,
                            InvoiceAmount = y.Amount,
                            DueDate = y.DueDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                            Currency = y.CurrencyType.ToString(),
                        })
                .ToArray(),
            //TotalEntitySomething = Math.Round(x.EntityManyToMany.Sum(y => y.Entity.Something), 2)
            })

                .OrderByDescending(x => x.Invoices.Length)
                .ThenBy(x => x.ClientName)
                //.Take(10)//?
                .ToArray();

            XmlHelper xmlHelper = new XmlHelper();
            return xmlHelper.Serialize(clients, "Clients");
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc =>
                    pc.Client.Name.Length >= nameLength))
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .OrderBy(pc => pc.Client.Name)
                        .Select(pc => new
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat
                        })
                        .ToArray()
                })
                .OrderByDescending(p => p.Clients.Length)
                .ThenBy(p => p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }
    }
}