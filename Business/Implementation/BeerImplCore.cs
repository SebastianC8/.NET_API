using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
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
        private IBeerRepository _beerRepository;

        public BeerImplCore(StoreContext context, IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => new BeerDTO
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID,
            });
        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

            await _beerRepository.Create(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);
            
            if (beer != null)
            {
                beer.Name = beerUpdateDTO.Name;
                beer.Alcohol = beerUpdateDTO.Alcohol;
                beer.BrandID = beerUpdateDTO.BrandID;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDTO = new BeerDTO
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDTO;
            }

            return null;
        }

    }
}
