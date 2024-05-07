using RMSProject.Models;

namespace RMSProject.ViewModels
{
    public class ListViewData
    {

        public IEnumerable<MenuItem> MenuItems { get; set; }

       
        public IQueryable<NutritionalInformation> NutritionalInformation { get; set; }

   
    }
}
