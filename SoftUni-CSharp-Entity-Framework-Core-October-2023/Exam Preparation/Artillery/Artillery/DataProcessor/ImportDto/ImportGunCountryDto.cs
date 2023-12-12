using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Artillery.DataProcessor.ImportDto
{
    public class ImportGunCountryDto
    {
        [Required]//?
        public int Id { get; set; }
    }
}
//req,"?",min,max,range,regular,email,enum range

//"Countries": [
//{ "Id": 86 },
//{ "Id": 57 },
//{ "Id": 64 },
//{ "Id": 74 },
//{ "Id": 58 }