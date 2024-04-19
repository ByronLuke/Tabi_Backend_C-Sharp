using System;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.Events
{
    public class EventsAddRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int EventTypeId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)] 
        public string Summary { get; set; }
        [Required]
        [MaxLength(4000)]               
        public string ShortDescription { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int VenueId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int EventStatusId { get; set; }
        public int ImageId { get; set; }
        [MaxLength(400)]
        public string ExternalSiteUrl { get; set; }
        [Required]
        public bool isFree { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

    }
}
