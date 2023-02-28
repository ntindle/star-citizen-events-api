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
            get; set; }
        public DateTime EndDateTime
        {
            get; set;
        }

    }
}