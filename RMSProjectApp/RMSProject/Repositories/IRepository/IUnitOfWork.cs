namespace RMSProject.Repositories.IRepository
{


    /*
     * Handles Database operations.
     * 
     * This interface is for the db context, whereas the individual repositories are there to deal with individual DbSets.
     * The reason why we implement a save in either concrete implementation of the interfaces or the Unit of work class is because
     * on some of these DbSets we need to perform multiple different operations before saving and other times we need to save right after making a change. 
     * So we need to manually call the update method, rather than calling it after each individual CRUD operation implementation, for example.
     * Plus, these methods are applied on a global scale. i.e. you call save or update on the database context rather than the DbSet. 
     * Hence we use the unit of work class.interface for this.
     * 
     * A Unit of work class also allows us to inject repositories in one place, rather than calling them in the constructor of the controller.
     */
    public interface IUnitOfWork
    {
        // We want to get and use all the methods. Not change the, Hence we use a get prop
        IMenuItemsRepository MenuItemsRepository { get; }

        INutritionalInformationRepository NutritionalInformationRepository { get; }

        IShoppingCartRepository ShoppingCartRepository { get; }

        IOrderInformationRepository OrderInformationRepository {  get; }

        void SaveChanges();

    }
}
