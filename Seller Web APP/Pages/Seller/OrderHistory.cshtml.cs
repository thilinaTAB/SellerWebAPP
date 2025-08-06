using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Seller_Web_App.Models;
using System.Text.Json;

namespace Seller_Web_App.Pages.Orders
{
    public class OrderHistoryModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public OrderHistoryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CozyComfortAPI");
        }

        public List<SellerOrderReadDTO> Orders { get; set; } = new List<SellerOrderReadDTO>();
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var sellerId = 1; // Hard-coded seller ID for demonstration

            try
            {
                var apiUrl = $"https://localhost:7175/api/SellerOrder/{sellerId}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Orders = JsonSerializer.Deserialize<List<SellerOrderReadDTO>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<SellerOrderReadDTO>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error fetching orders: {response.StatusCode} - {errorContent}";
                    Orders = new List<SellerOrderReadDTO>();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                Orders = new List<SellerOrderReadDTO>();
            }
        }
    }
}