using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Data;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class BeerImplRepository : IBeerRepository
    {
        private StoreContext _storeContext;

        public BeerImplRepository(StoreContext context)
        {
            _storeContext = context;
        }

        public async Task<IEnumerable<Beer>> Get()
        {
            return await _storeContext.Beers.ToListAsync();
        }

        public async Task<Beer> GetById(int id)
        {
            return await _storeContext.Beers.FindAsync(id);
        }

        public async Task Create(Beer beer)
        {
            await _storeContext.Beers.AddAsync(beer);
        }

        public void Update(Beer beer)
        {
            _storeContext.Beers.Attach(beer);
            _storeContext.Beers.Entry(beer).State = EntityState.Modified;
        }

        public void Delete(Beer beer)
        {
            _storeContext.Beers.Remove(beer);
        }

        public async Task Save()
        {
            await _storeContext.SaveChangesAsync();
        }

    }
}
