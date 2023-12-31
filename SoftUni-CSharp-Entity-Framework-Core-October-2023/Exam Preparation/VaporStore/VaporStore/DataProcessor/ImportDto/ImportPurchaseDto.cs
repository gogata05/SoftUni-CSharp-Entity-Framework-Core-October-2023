﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using VaporStore.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.ImportDto
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [Required]
        [XmlAttribute("title")]
        public string Title { get; set; }

        [Required]
        [XmlElement("Type")]
        public string Type { get; set; }

        [Required]
        [XmlElement("Key")]
        //[RegularExpression(@"^[A-Z\d]{4}-[A-Z\d]{4}-[A-Z\d]{4}$")]
        //[RegularExpression(@"^([A-Z0-9]{4})\-([A-Z0-9]{4})\-([A-Z0-9]{4})$")]
        [RegularExpression(@"^[A-Z\d]{4}-[A-Z\d]{4}-[A-Z\d]{4}$")]
        public string ProductKey { get; set; }

        [Required]
        [XmlElement("Card")]
        public string Card { get; set; }

        [Required]
        [XmlElement("Date")]
        public string Date { get; set; }
    }
}
//xml

//Type – enumeration of type PurchaseType, with possible values ("Retail", "Digital") (required)
//ProductKey – text, which consists of 3 pairs of 4 uppercase Latin letters and digits, separated by dashes (ex. "ABCD-EFGH-1J3L") (required)
//Date – Date(required)
//Card – the purchase's card (required) 

//< Purchase title = "Dungeon Warfare 2" >
//< Type > Digital </ Type >
//< Key > ZTZ3 - 0D2S - G4TJ </ Key >
//< Card > 1833 5024 0553 6211 </ Card >
//< Date > 07 / 12 / 2016 05:49 </ Date >
//</ Purchase >