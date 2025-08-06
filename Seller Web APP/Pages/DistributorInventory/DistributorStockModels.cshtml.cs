using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Seller_Web_App.Models;
using System.Text.Json;

namespace Seller_Web_App.Pages.DistributorInventory
{
    public class DistributorStockModels : PageModel
    {
        private readonly HttpClient _httpClient;

        public DistributorStockModels(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CozyComfortAPI");
        }

        [BindProperty]
        public List<DistributorStockView> DistributorStocks { get; set; } = new List<DistributorStockView>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DistributorStock/all");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    DistributorStocks = JsonSerializer.Deserialize<List<DistributorStockView>>(content, options) ?? new List<DistributorStockView>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve distributor stocks from the API.");
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