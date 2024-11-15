namespace CRM_Server_API.Models.Request
{
    public class ClientRequest
    {
        public string Name { get; set; } = string.Empty; // Ім'я клієнта
        public string LastName { get; set; } = string.Empty; // Прізвище клієнта
        public string Email { get; set; } = string.Empty; // Електронна пошта клієнта
        public string PhoneNumber { get; set; } = string.Empty; // Номер телефону клієнта
        public string Address { get; set; } = string.Empty; // Адреса клієнта
        public string CompanyName { get; set; } = string.Empty; // Назва компанії клієнта
        public string? Notes { get; set; } = string.Empty;//Опис
        public bool IsActive { get; set; } = true; // Статус активності клієнта
    }
}
