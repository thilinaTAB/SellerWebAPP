using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Seller_Web_App.Models;
using Microsoft.Extensions.Configuration; // Added for IConfiguration

namespace Seller_Web_App.Pages.Seller
{
    public class SellerInventoryModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration; // Added to store the configuration

        public SellerInventoryModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("CozyComfortAPI");
            _configuration = configuration; // Store the configuration
        }

        public List<SellerInventoryView> SellerInventory { get; set; } = new List<SellerInventoryView>();

        private int _sellerId = 1;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SellerInventory/{_sellerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    SellerInventory = JsonSerializer.Deserialize<List<SellerInventoryView>>(content, options) ?? new List<SellerInventoryView>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve seller inventory.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
            }

            return Page();
        }
    }
}