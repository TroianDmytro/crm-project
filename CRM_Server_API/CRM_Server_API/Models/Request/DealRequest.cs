namespace CRM_Server_API.Models.Request
{
    public class DealRequest
    {
        public string Title { get; set; }
        public Guid ClientId { get; set; }// Связь с клиентом
    }
}
