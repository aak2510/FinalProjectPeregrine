using RMSProject.Models;

namespace RMSProject.Repositories.IRepository
{
    /*
     * Methods which are specific/not common across all DbSets are implemented within their own specific interfaces.
     */
    public interface INutritionalInformationRepository : IRepository<NutritionalInformation>
    {  
        /* The reason why we implement an update in individual classes is because on some of these DbSets we need to perform multiple different operations before saving. 
         * And sometimes we do indeed need to save right after making a change. 
         * So we need to manually call the update method, rather than calling it after each individual CRUD operation implementation, for example.
         */
        void Update(NutritionalInformation item);

      
    

    }
}
