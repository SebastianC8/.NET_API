using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IPostCore
    {
        public Task<IEnumerable<PostDTO>> Get();
    }
}
