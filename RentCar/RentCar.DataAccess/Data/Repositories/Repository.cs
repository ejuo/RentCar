using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Interfaces;
using System.Threading.Tasks;

namespace RentCar.DataAccess.Data.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
    {
        protected readonly TContext Context;

        protected Repository(TContext context)
        {
            this.Context = context;
        }
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
