using System.ComponentModel.DataAnnotations;

namespace Seller_Web_App.Models
{
    public class SellerInventoryView
    {
        public int SellerInventoryId { get; set; }
        public int BlanketModelId { get; set; }

        [Display(Name = "Blanket Model")]
        public string ModelName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Current Stock")]
        public int Quantity { get; set; }

        [Display(Name = "Material")]
        public string MaterialName { get; set; } = string.Empty;
    }
}