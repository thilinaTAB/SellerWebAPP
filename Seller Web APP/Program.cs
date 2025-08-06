using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddHttpClient("CozyComfortAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CozyComfortApi:BaseUrl") ?? string.Empty);

    var apiKey = builder.Configuration["CozyComfortApi:CozyComfortSellerKey"];

    if (!string.IsNullOrEmpty(apiKey))
    {
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
    }

    httpClient.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();