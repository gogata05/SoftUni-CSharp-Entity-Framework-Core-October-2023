﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class ExportPrisonerDto
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public ExportPrisonerMailDto[] EncryptedMessages { get; set; }
    }
}
//xml

//Id – integer, Primary Key 
//IncarcerationDate – Date
//Mails – collection of type Mail 

//    < Prisoner >
//     < Id > 3 </ Id >
//     < Name > Binni Cornhill </ Name >
//     < IncarcerationDate > 1967 - 04 - 29 </ IncarcerationDate >
//     < EncryptedMessages >
//      < Message >
//      < Description > !? sdnasuoht evif - ytnewt rof deksa uoy ro orez artxe na ereht sI</Description> 
//      </Message> 
