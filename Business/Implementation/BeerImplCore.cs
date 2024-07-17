using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Implementation
{
    public class BeerImplCore : IBeerCore
    {
        private StoreContext _storeContext;

        public BeerImplCore(StoreContext context)
        {
            _storeContext = context;
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _storeContext.Beers.Select(b => new BeerDTO
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID,
            }).ToListAsync();
        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer != null)
            {
                BeerDTO beerDTO = new BeerDTO
                {
                    Id = beer.BrandID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                return beerDTO;
            }

            return null;
 
        }

        public async Task<BeerDTO> Create(BeerInsertDTO beerInsertDTO)
        {
            var beer = new Beer
            {
                Name = beerInsertDTO.Name,
                Alcohol = beerInsertDTO.Alcohol,
                BrandID = beerInsertDTO.BrandID
            };

            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            var beerDTO = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDTO;
        }

        public async Task<BeerDTO> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            var beer = await _storeContext.FindAsync<Beer>(id);
            
            if (beer != null)
            {
                beer.Name = beerUpdateDTO.Name;
                beer.Alcohol = beerUpdateDTO.Alcohol;
                beer.BrandID = beerUpdateDTO.BrandID;

                await _storeContext.SaveChangesAsync();

                var beerDTO = new BeerDTO
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                return beerDTO;
            }

            return null;

        }

        public async Task<BeerDTO> Delete(int id)
        {
            var beer = await _storeContext.FindAsync<Beer>(id);

            if (beer != null)
            {
                var beerDTO = new BeerDTO
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                _storeContext.Remove(beer);
                await _storeContext.SaveChangesAsync();

                return beerDTO;
            }

            return null;
        }

    }
}
