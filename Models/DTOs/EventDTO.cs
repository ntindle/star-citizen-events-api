using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SCEAPI.Models.DTOs
{
    public class EventDTO
    {
        [SwaggerSchema("The Event Identifier", ReadOnly = true)]
        [Key]
        public int Id { get; set; }

        [SwaggerSchema("The Event Name")]
        [Required]
        public string Name { get; set; } = "";

        [SwaggerSchema("The IRL Corresponding Event Name")]
        public string? AlternativeName { get; set; }

        [SwaggerSchema("The Event's Description")]
        [Required]
        public string? Description { get; set; }

        [SwaggerSchema("Describe any known incorrect information about the event")]
        public string? KnownIncorrect { get; set; }


        [Required]
        [SwaggerSchema("The IRL Start Date Time of the Event")]
        public DateTime StartDateTime
        {
            get; set;
        }

        [SwaggerSchema("The IRL End Date Time of the Event")]
        public DateTime EndDateTime
        {
            get; set;
        }


        [SwaggerSchema("The In Game Start Date Time of the Event")]
        public DateTime IngameStartDateTime
        {
            get
            {
                return Event.GenerateIngameDateTime(StartDateTime);
            }
        }

        [SwaggerSchema("The In Game End Date Time of the Event")]

        public DateTime IngameEndDateTime
        {
            get
            {
                return Event.GenerateIngameDateTime(EndDateTime);
            }
        }

        [SwaggerSchema("The Calculated Display Name the Event")]
        public string DisplayName
        {
            get
            {
                return Event.GenerateDisplayName(Name, StartDateTime, EndDateTime);
            }
        }


    }
}