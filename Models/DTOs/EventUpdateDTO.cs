using System.ComponentModel.DataAnnotations;

namespace SCEAPI.Models.DTOs
{
    public class EventUpdateDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}