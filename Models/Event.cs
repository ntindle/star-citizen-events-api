using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCEAPI.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public string? AlternativeName { get; set; }
        public string? Description { get; set; }

        public string? KnownIncorrect { get; set; } = "";

        public DateTime StartDateTime
        {
            get; set;
        }
        public DateTime EndDateTime
        {
            get; set;
        }

        public DateTime IngameStartDateTime
        {
            get
            {
                return GenerateIngameDateTime(StartDateTime);
            }
        }

        public DateTime IngameEndDateTime
        {
            get
            {
                return GenerateIngameDateTime(EndDateTime);
            }
        }

        public string DisplayName
        {
            get
            {
                return GenerateDisplayName(Name, StartDateTime, EndDateTime);
            }
        }

        public static DateTime GenerateIngameDateTime(DateTime dateTime)
        {
            return dateTime.AddYears(930);
        }

        public static string GenerateDisplayName(string name, DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime.Year == endDateTime.Year)
            {
                return $"{name} {GenerateIngameDateTime(startDateTime).Year}";
            }
            else
            {
                return $"{name} {GenerateIngameDateTime(startDateTime).Year}-{GenerateIngameDateTime(endDateTime).Year}";
            }
        }

    }
}