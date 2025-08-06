using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Seller_Web_App.Models
{
    // DTO for reading seller's order data from the API
    public class SellerOrderReadDTO
    {
        // Maps to the 'sellerOrderID' property in the JSON response
        [JsonPropertyName("sellerOrderID")]
        public int SellerOrderID { get; set; }

        // Maps to the 'orderDate' property in the JSON response
        [JsonPropertyName("orderDate")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        // Maps to the 'modelName' property in the JSON response
        [JsonPropertyName("modelName")]
        [Display(Name = "Blanket Model")]
        public string ModelName { get; set; } = string.Empty;

        // Maps to the 'quantity' property in the JSON response
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        // Maps to the 'price' property in the JSON response
        [JsonPropertyName("price")]
        [Display(Name = "Unit Price")]
        public decimal Price { get; set; }

        // Maps to the 'total' property in the JSON response
        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        // Maps to the 'status' property in the JSON response
        [JsonPropertyName("status")]
        [Display(Name = "Order Status")]
        public string Status { get; set; } = string.Empty;
    }
}