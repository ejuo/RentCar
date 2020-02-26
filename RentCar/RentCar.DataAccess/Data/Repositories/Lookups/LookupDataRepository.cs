using Microsoft.EntityFrameworkCore;
using RentCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.DataAccess.Data.Repositories.Lookups
{
    public class LookupDataRepository : ICarTypeLookupDataRepository
    {
        private Func<RentCarDbContext> _contextCreator;

        public LookupDataRepository(Func<RentCarDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<List<LookupItem>> GetCarTypeLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.CarTypes.AsNoTracking()
                    .Select(m =>
                        new LookupItem
                        {
                            Id = m.Id,
                            DisplayMember = m.Name
                        })
                    .ToListAsync();
                return items;
            }
        }

    }
}
