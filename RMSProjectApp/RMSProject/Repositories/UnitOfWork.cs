using Microsoft.EntityFrameworkCore;
using RMSProject.Data;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;

namespace RMSProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMenuItemsRepository MenuItemsRepository { get; private set; }

        public INutritionalInformationRepository NutritionalInformationRepository { get; private set; }

        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public IOrderInformationRepository OrderInformationRepository { get; private set; }


        private ModelsDbContext _context;

        public UnitOfWork(ModelsDbContext dbContext)
        {
            _context = dbContext;
            MenuItemsRepository = new MenuItemsRepository(_context);
            NutritionalInformationRepository = new NutritionalInformationRepository(_context);
            ShoppingCartRepository = new ShoppingCartRepository(_context);
            OrderInformationRepository = new OrderInformationRepository(_context, ShoppingCartRepository);
        }




        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
