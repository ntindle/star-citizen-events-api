using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SCEAPI.Models.DTOs
{
    [SwaggerSchema]
    public class EventCreateDTO
    {
        [Required]
        [SwaggerSchema("The Event Name")]
        public string Name { get; set; } = "";

        [SwaggerSchema("The IRL Corresponding Event Name")]
        public string? AlternativeName { get; set; } = "";

        [SwaggerSchema("The Event's Description")]
        [Required]
        public string? Description { get; set; } = "";

        [SwaggerSchema("The IRL Start Date Time of the Event")]
        [Required]
        public DateTime StartDateTime
        {
            get; set;
        }

        [SwaggerSchema("The IRL End Date Time of the Event")]
        public DateTime EndDateTime
        {
            get; set;
        }

    }
}