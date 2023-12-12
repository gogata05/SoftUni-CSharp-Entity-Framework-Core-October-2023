﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Castle.DynamicProxy.Generators.Emitters;

namespace CarDealer.DTOs.Export
{
    [XmlType("car")]
    public class CarsWithDistanceDto
    {
        [XmlElement("make")] public string Make { get; set; }
        [XmlElement("model")] public string Model { get; set; }
        [XmlElement("traveled-distance")] public long TravelledDistance { get; set; }
    }
}
