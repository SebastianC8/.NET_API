using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }
    }
}
