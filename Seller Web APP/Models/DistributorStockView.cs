using System.Text.Json.Serialization;

namespace Seller_Web_App.Models
{
    public class DistributorStockView
    {
        public int DistributorStockID { get; set; }
        public int DistributorID { get; set; }
        public int Inventory { get; set; }

        [JsonPropertyName("blanketModel")]
        public BlanketModelView BlanketModel { get; set; } = new BlanketModelView();
    }

    public class BlanketModelView
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // These properties are now flattened to match your API's DTO
        public int MaterialID { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public string MaterialDescription { get; set; } = string.Empty;
    }
}