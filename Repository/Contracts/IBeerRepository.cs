using Repository.Data;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IBeerRepository
    {
        Task<IEnumerable<Beer>> Get();
        Task<Beer> GetById(int id);
        Task Create(Beer beer);
        void Update(Beer beer);
        void Delete(Beer beer);
        Task Save();
    }
}
