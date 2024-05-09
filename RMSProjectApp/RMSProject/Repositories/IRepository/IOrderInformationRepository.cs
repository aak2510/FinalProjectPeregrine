using RMSProject.Models;

namespace RMSProject.Repositories.IRepository
{
    public interface IOrderInformationRepository : IRepository<OrderInformation>
    {
        /// <summary>
        /// Creates an Order and updates the information for it.
        /// </summary>
        /// <param name="order">TThe order to create</param>
        void Create(OrderInformation order);
    }
}
