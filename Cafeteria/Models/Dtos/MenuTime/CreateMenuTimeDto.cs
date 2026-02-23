namespace Cafeteria.Models.Dtos.MenuTime
{
    public class CreateMenuTimeDto
    {
        public string Name { get; set; } = string.Empty;

        public TimeOnly TimeStart { get; set; }

        public TimeOnly TimeEnd { get; set; }
   
    }
}