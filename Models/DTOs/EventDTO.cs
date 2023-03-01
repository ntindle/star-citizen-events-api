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
                return StartDateTime.AddYears(930);
            }
        }

        public DateTime IngameEndDateTime
        {
            get
            {
                return EndDateTime.AddYears(930);
            }
        }

        public string DisplayName
        {
            get
            {
                if (IngameStartDateTime.Year == IngameEndDateTime.Year)
                {
                    return $"{Name} {IngameStartDateTime.Year}";
                }
                else
                {
                    return $"{Name} {IngameStartDateTime.Year}-{IngameEndDateTime.Year}";
                }
            }
        }

    }
}