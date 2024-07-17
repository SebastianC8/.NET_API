using AutoMapper;
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
        private IMapper _mapper;

        public BeerImplCore(IBeerRepository beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            var beers = await _beerRepository.Get();
            return beers.Select(b => _mapper.Map<BeerDTO>(b));
        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                BeerDTO beerDTO = _mapper.Map<BeerDTO>(beer);
                return beerDTO;
            }

            return null;
 
        }

        public async Task<BeerDTO> Create(BeerInsertDTO beerInsertDTO)
        {
            var beer = _mapper.Map<Beer>(beerInsertDTO); // Mapper 

            await _beerRepository.Create(beer);
            await _beerRepository.Save();

            var beerDTO = _mapper.Map<BeerDTO>(beer);

            return beerDTO;
        }

        public async Task<BeerDTO> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            var beer = await _beerRepository.GetById(id);
            
            if (beer != null)
            {

                beer = _mapper.Map<BeerUpdateDTO, Beer>(beerUpdateDTO, beer);

                /*
                beer.Name = beerUpdateDTO.Name;
                beer.Alcohol = beerUpdateDTO.Alcohol;
                beer.BrandID = beerUpdateDTO.BrandID;
                */

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDTO = _mapper.Map<BeerDTO>(beer);
                return beerDTO;
            }

            return null;

        }

        public async Task<BeerDTO> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDTO = _mapper.Map<BeerDTO>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDTO;
            }

            return null;
        }

    }
}
