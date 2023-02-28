using System.ComponentModel.DataAnnotations;

namespace SCEAPI.Models.DTOs
{
    public class EventCreateDTO
    {
        [Required]
        public string Name { get; set; } = "";

        public string AlternativeName { get; set; } ="";
        [Required]
        public string? Description { get; set; } = "";
        [Required]
        
        public DateTime StartDateTime
        {
            get; set; }
        public DateTime EndDateTime
        {
            get; set;
        }

    }
}