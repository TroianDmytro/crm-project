namespace CRM_Server_API.Models.Responce
{
    public class CategoryResponceWithProduct: CategoryResponce
    {
        public List<ProductResponce> Products { get; set; }
    }
}
