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

        private ModelsDbContext _context;
        public UnitOfWork(ModelsDbContext dbContext)
        {
            _context = dbContext;
            MenuItemsRepository = new MenuItemsRepository(_context);
            NutritionalInformationRepository = new NutritionalInformationRepository(_context);
        }




        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
