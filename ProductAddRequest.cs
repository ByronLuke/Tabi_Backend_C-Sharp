using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Products
{
    public class ProductAddRequest
    {
        [Required]
        [MaxLength(50)]
        public string SKU { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; }

        [Required]
        [Range (0, int.MaxValue)]    
        public int ProductTypeId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int VenueId { get; set; }

        [Required]
        public Boolean isVisible { get; set; }

        [Required]
        public Boolean isActive { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int PrimaryImageId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CreatedBy { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ModifiedBy { get; set; }        

    }
}
