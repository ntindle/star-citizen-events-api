using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCEAPI.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public DateOnly StartDate { get; set; }
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