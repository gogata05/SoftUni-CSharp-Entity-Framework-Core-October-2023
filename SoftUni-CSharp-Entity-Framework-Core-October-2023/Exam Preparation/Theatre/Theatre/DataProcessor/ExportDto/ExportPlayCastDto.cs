using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Actor")]
    public class ExportPlayCastDto
    {
        [XmlAttribute("FullName")]
        public string FullName { get; set; }

        [XmlAttribute("MainCharacter")]
        public string MainCharacter { get; set; }//?
    }
}
//< Actor FullName = "Sylvia Felipe" MainCharacter = "Plays main character in 'A Raisin in the Sun'." />