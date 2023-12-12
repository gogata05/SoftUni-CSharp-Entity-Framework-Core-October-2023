using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientDto
    {
        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }

        [XmlElement("ClientName")]
        public string ClientName { get; set; }

        [XmlElement("VatNumber")]
        public string VatNumber { get; set; }

        [XmlArray("Invoices")]
        public ExportClientInvoiceDto[] Invoices { get; set; }
    }
}

//⦁	Number – int in range  [1,000,000,000…1,500,000,000] (required)
//⦁	Amount – decimal (required)
//⦁	DueDate – DateTime (required)
//⦁	CurrencyType – enumeration of type CurrencyType, with possible values (BGN, EUR, USD) (required)


//< Client InvoicesCount = "9" >
//    < ClientName > SPEDOX,SRO </ ClientName >
//    < VatNumber > SK2023911087 </ VatNumber >
//    < Invoices >
