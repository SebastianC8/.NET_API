using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IBeerCore
    {
        Task<IEnumerable<BeerDTO>> Get();
        Task<BeerDTO> GetById(int id);
        Task<BeerDTO> Create(BeerInsertDTO beerInsertDTO);
        Task<BeerDTO> Update(int id, BeerUpdateDTO beerInsertDTO);
        Task<BeerDTO> Delete(int id);
    }
}
