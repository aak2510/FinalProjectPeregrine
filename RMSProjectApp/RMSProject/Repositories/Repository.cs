using Microsoft.EntityFrameworkCore;
using RMSProject.Data;
using RMSProject.Repositories.IRepository;
using System.Linq.Expressions;

namespace RMSProject.Repositories
{
    /*
     * This class is a generic concrete implementation of the generic repository class.
     * We use this class in all other concrete implementations to allow for common interactions to take place.
     */
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ModelsDbContext _context;
        // The main aim is to be able to use this reposity class with any DbSet,
        // so we need to specify which one when the methods are being called upon.
        internal DbSet<T> DbSet;

        public Repository(ModelsDbContext dbContext)
        {
            _context = dbContext;
            // set the property to the specific class calling these repository methods
            this.DbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(expression);
            return query.FirstOrDefault();
        }
    }
}
