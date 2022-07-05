using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelListing.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _db;


        // Dependency Injection:
        // What ever we loaded in the start up is now available
        // no need to instaniate, just refrence to existing object,
        // so now we can get a copy of dbcontext

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _db = _dbContext.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            // check for includes
            if (includes != null)
            {
                foreach(var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            // check for includes
            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task InserRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async Task Insert(T entity)
        {
           await _db.AddAsync(entity);
        }

        public void Update(T Entity)
        {
            // checking 
            _db.Attach(Entity);
            //
            _dbContext.Entry(Entity).State = EntityState.Modified;
        }
    }
}
