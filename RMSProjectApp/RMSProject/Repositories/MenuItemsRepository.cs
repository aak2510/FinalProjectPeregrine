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
            
            /*
             * We update each field separately. This is because when we edit or update a MenuItem we don't need update every field.
             * For example, when it comes to the image field, if we just updated the entire DbSet, then if the user doesn't re upload the image, 
             * then EF core would assume that the new update is a blank upload and thus delete the image. 
             * So we need to make sure we check to see if the field is not null (in other words the user has entered in a new file) then we upload an image.
             * If the user has NOT entered anything, then we don't change/update the image to a blank.
             */


            var obj = _context.MenuItem.FirstOrDefault(u => u.Id == item.Id);
            if (obj != null)
            {
                obj.ItemName = item.ItemName;
                obj.ItemDescription = item.ItemDescription;
                obj.ItemPrice = item.ItemPrice;
                obj.ItemDescription = item.ItemDescription;
                obj.TypeOfMeal = item.TypeOfMeal;
                if (obj.ImageUrl != null)
                {
                    obj.ImageUrl = item.ImageUrl;
                }
            }

        }

        public bool FindAny(Expression<Func<MenuItem, bool>> expression)
        {
            return _context.MenuItem.Any(expression);
        }


    }
}
