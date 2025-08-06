using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Seller_Web_App.Models;
using System.Text.Json;

namespace Seller_Web_App.Pages.DistributorInventory // Corrected Namespace
{
    public class MaterialsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public MaterialsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CozyComfortAPI");
        }

        public List<Material> Materials { get; set; } = new List<Material>();

        public async Task OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Material");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Materials = JsonSerializer.Deserialize<List<Material>>(content, options) ?? new List<Material>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve materials from the API. Check your API key and URL.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}