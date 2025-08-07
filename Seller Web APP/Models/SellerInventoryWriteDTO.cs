using System.ComponentModel.DataAnnotations;

namespace Seller_Web_App.Models
{
    public class SellerInventoryWriteDTO
    {
        [Required]
        public int Quantity { get; set; }

        public int SellerID { get; set; }

        [Required]
        public int ModelID { get; set; }
    }
}