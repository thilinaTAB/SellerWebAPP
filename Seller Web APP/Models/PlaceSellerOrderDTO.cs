using System.ComponentModel.DataAnnotations;

namespace Seller_Web_App.Models
{
    public class PlaceSellerOrderDTO
    {
        // This is the seller's ID, which will be populated in the controller.
        public int SellerId { get; set; }

        // This identifies the specific distributor's stock item to order from.
        [Required(ErrorMessage = "Please select a distributor's stock item.")]
        public int DistributorStockID { get; set; }

        [Required(ErrorMessage = "Please enter a quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}