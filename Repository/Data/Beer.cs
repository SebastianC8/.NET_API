using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class Beer
    {
        [Key] // PRIMARY KEY
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // AUTO_INCREMENT
        public int BeerID { get;  set; }

        public string Name { get; set; }

        public int BrandID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Alcohol {  get; set; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }
    }
}
