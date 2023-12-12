using System.Globalization;
using System.Text;
using Invoices.Data.Models;
using Invoices.Data.Models.Enums;
using Invoices.DataProcessor.ImportDto;
using Invoices.Utilities;
using Newtonsoft.Json;
namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Invoices.Data;
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";
        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";
        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";

        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportClientDto[] clientDtos =
                xmlHelper.Deserialize<ImportClientDto[]>
                    (xmlString, "Clients");//root
            StringBuilder sb = new StringBuilder();
            List<Client> clients = new List<Client>();
            foreach (ImportClientDto clientDto in clientDtos)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Client client = new Client()
                {
                    //<Name>LiCB</Name>
                    //<NumberVat>BG5464156654654654</NumberVat>
                    Name = clientDto.Name,
                    NumberVat = clientDto.NumberVat,
                };
                
                foreach (ImportClientAddressDto adressDto in clientDto.Addresses)
                {
                    if (!IsValid(adressDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Address address = new Address()
                    {
                        //<StreetName>Gnigler strasse</StreetName>
                        //<StreetNumber>57</StreetNumber>
                        //<PostCode>5020</PostCode>
                        //<City>Salzburg</City>
                        //<Country>Austria</Country>
                        StreetName = adressDto.StreetName,
                        StreetNumber = adressDto.StreetNumber,
                        PostCode = adressDto.PostCode,
                        City = adressDto.City,
                        Country = adressDto.Country,
                    };
                    client.Addresses.Add(address);
                }
                clients.Add(client);
                sb.AppendLine(String.Format(SuccessfullyImportedClients, client.Name));
            }
            context.Clients.AddRange(clients);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            ImportInvoiceDto[] invoiceDtos = 
                JsonConvert.DeserializeObject<ImportInvoiceDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Invoice> invoices = new List<Invoice>();
            foreach (ImportInvoiceDto invoiceDto in invoiceDtos)
            {
                if (!IsValid(invoiceDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (invoiceDto.IssueDate == DateTime.ParseExact("01/01/0001", "dd/MM/yyyy", CultureInfo.InvariantCulture) || invoiceDto.DueDate == DateTime.ParseExact("01/01/0001", "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (invoiceDto.IssueDate > invoiceDto.DueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Invoice invoice = new Invoice()
                {
                    //"Number": 1427940691,
                    //"IssueDate": "2022-08-29T00:00:00",
                    //"DueDate": "2022-10-28T00:00:00",
                    //"Amount": 913.13,
                    //"CurrencyType": 1,
                    //"ClientId": 1
                    Number = invoiceDto.Number,
                    IssueDate = invoiceDto.IssueDate,
                    DueDate = invoiceDto.DueDate,
                    Amount = invoiceDto.Amount,
                    CurrencyType = (CurrencyType)invoiceDto.CurrencyType,
                    ClientId = invoiceDto.ClientId,
                };
                invoices.Add(invoice);
                sb.AppendLine(String.Format(SuccessfullyImportedInvoices, invoice.Number));
            }
            context.Invoices.AddRange(invoices);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            ImportProductDto[] productDtos = 
                JsonConvert.DeserializeObject<ImportProductDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Product> products = new List<Product>();
            foreach (ImportProductDto productDto in productDtos)
            {
                if (!IsValid(productDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Product product = new Product()
                {
                    //"Name": "ADR plate",
                    //"Price": 14.97,
                    //"CategoryType": 1,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CategoryType = (CategoryType)productDto.CategoryType,
                };

                foreach (var clientsId in productDto.Clients.Distinct())
                {
                    Client client = context.Clients.Find(clientsId);
                    if (client == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
	 
                    product.ProductsClients.Add(new ProductClient()
                    {
                        Product = product,
                        Client = client,
                    });
                }
                products.Add(product);
                sb.AppendLine(String.Format(SuccessfullyImportedProducts, product.Name, product.ProductsClients.Count));
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
