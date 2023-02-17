using System.ComponentModel.DataAnnotations;

namespace SCEAPI.Models.DTOs
{
    public class EventDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? AlternativeName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public DateTime StartDateTime
        {
            get
            {
                return new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime?.Hour ?? 0, StartTime?.Minute ?? 0, StartTime?.Second ?? 0);
            }
        }
        public DateTime EndDateTime
        {
            get
            {
                return new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime?.Hour ?? 0, EndTime?.Minute ?? 0, EndTime?.Second ?? 0);
            }
        }

    }
}