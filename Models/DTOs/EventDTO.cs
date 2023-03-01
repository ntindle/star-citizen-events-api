using System.ComponentModel.DataAnnotations;

namespace SCEAPI.Models.DTOs
{
    public class EventDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";

        public string? AlternativeName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]


        public DateTime StartDateTime
        {
            get; set;
        }

        [Required]
        public DateTime EndDateTime
        {
            get; set;
        }


        public DateTime IngameStartDateTime
        {
            get
            {
                return Event.GenerateIngameDateTime(StartDateTime);
            }
        }

        public DateTime IngameEndDateTime
        {
            get
            {
                return Event.GenerateIngameDateTime(EndDateTime);
            }
        }

        public string DisplayName
        {
            get
            {
                return Event.GenerateDisplayName(Name, StartDateTime, EndDateTime);
            }
        }

    }
}