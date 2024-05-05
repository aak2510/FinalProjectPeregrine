using RMSProject.Data;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using System.Linq.Expressions;

namespace RMSProject.Repositories
{
    public class NutritionalInformationRepository : Repository<NutritionalInformation>, INutritionalInformationRepository
    {
        private ModelsDbContext _context;
        public NutritionalInformationRepository(ModelsDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(NutritionalInformation item)
        {
            _context.Update(item);
        }


    }
}
