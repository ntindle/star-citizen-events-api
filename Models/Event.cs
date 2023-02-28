using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCEAPI.Models
{
    public class Event
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }="";

        public string? AlternativeName { get; set; }
        public string? Description { get; set; }


        public DateTime StartDateTime
        {
            get; set;
        }
        public DateTime EndDateTime
        {
            get; set;
        }

    }
}