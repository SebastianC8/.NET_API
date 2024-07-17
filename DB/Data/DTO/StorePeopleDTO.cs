using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.DTO
{
    public class StorePeopleDTO
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; } = string.Empty;
        public string PASSWORD { get; set; } = string.Empty;
    }
}
