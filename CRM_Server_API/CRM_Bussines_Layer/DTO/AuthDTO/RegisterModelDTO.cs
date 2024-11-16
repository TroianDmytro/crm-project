namespace CRM_Business_Layer.DTO.AuthDTO
{
    public class RegisterModelDTO
    {
        // Ім'я 
        public string Name { get; set; } = string.Empty;

        // Прізвище
        public string LastName { get; set; } = string.Empty;

        //Отчество
        public string? Patronymic { get; set; } = string.Empty;

        //Login
        public string? UserName { get; set; }

        // Адреса 
        public string? Address { get; set; } = string.Empty;

        //День рождения
        public DateTime DateOfBirth { get; set; }

        //Дата начала работы
        public DateTime? HireDate { get; set; }

        //Должность
        public string? Position { get; set; }

        public string? Department { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
