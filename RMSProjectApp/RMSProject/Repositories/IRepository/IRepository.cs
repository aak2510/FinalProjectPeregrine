using System.Linq.Expressions;

namespace RMSProject.Repositories.IRepository
{
    /*
     * A generic repository class consisting of all the common methods that would be implemented by every repository (DbSet).
     * 
     * Methods such as update/save may not be common across all DbSets, as we may just, for example, read from them but not write.
     * So we need to make sure we implemented those types of methods in the interface class specific to the database set and extend from this one.
     */
    public interface IRepository<T> where T : class
    {
        // Expression parameter will be used and passed through to the GetFirstOrDefault function
        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        IQueryable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);
    }
}
