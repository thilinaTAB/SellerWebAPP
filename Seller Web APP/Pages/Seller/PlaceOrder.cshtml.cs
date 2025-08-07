using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Seller_Web_App.Models;
using System.Text.Json;
using System.Net.Http.Json;

namespace Seller_Web_App.Pages.Order
{
    public class PlaceOrderModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public PlaceSellerOrderDTO Order { get; set; } = new PlaceSellerOrderDTO();

        public List<DistributorStockView> AvailableDistributorStocks { get; set; } = new List<DistributorStockView>();
        public SelectList DistributorStocksDropdown { get; set; }

        private int _sellerId = 1; // Hard-coded seller ID for this example

        public PlaceOrderModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CozyComfortAPI");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // You must call LoadDataAsync() here to populate the dropdown on validation failure
            await LoadDataAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Order.SellerId = _sellerId;

                var response = await _httpClient.PostAsJsonAsync("api/SellerOrder", Order);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Order placed successfully! The distributor has received your order.";
                    return RedirectToPage();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error from API: {response.StatusCode} - {errorContent}");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
                return Page();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DistributorStock/all");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    AvailableDistributorStocks = JsonSerializer.Deserialize<List<DistributorStockView>>(content, options) ?? new List<DistributorStockView>();

                    DistributorStocksDropdown = new SelectList(AvailableDistributorStocks, "DistributorStockID", "BlanketModel.ModelName");
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
        }
    }
}