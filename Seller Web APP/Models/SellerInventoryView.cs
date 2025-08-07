// Seller_Web_App/Models/SellerInventoryView.cs
using System.Text.Json.Serialization;

namespace Seller_Web_App.Models
{
    public class SellerInventoryView
    {
        public int InventoryID { get; set; }
        public int Quantity { get; set; }
        public int SellerID { get; set; }
        public int ModelID { get; set; }

        public string ModelName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public string MaterialDescription { get; set; } = string.Empty;
    }
}