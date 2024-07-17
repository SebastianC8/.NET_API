using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.DTO
{
    public class BeerDTO
    {
        public int Id { get;  set; }

        public string Name { get; set; }

        public int BrandID { get; set; }

        public decimal Alcohol { get; set; }

    }
}
