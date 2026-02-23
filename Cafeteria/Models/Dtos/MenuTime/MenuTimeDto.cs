namespace Cafeteria.Models.Dtos.MenuTime
{
    public class MenuTimeDto
    {
        public int MenuTimeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public TimeOnly TimeStart { get; set; }

        public TimeOnly TimeEnd { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
   
    }
}