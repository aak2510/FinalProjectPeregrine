using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RMSProject.Data;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using System.Linq;
using System.Linq.Expressions;

namespace RMSProject.Repositories
{
    public class MenuItemsRepository : Repository<MenuItem>, IMenuItemsRepository
    {
        private ModelsDbContext _context;
        public MenuItemsRepository(ModelsDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public void Update(MenuItem item)
        {
            _context.MenuItem.Update(item);
        }

        public bool FindAny(Expression<Func<MenuItem, bool>> expression)
        {
            return _context.MenuItem.Any(expression);
        }


    }
}
