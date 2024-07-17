using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Data
{
    public class Brand
    {
        [Key] // PRIMARY KEY
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // AUTO_INCREMENT
        public int BrandID { get; set; }

        public string Name { get; set; }
    }
}
