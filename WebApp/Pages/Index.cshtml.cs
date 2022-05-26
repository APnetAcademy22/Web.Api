using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _client;
        public string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync()
        {
            var client = _client.CreateClient("ProductApi");
            Message = await client.GetStringAsync("Product/List");
        }
    }
}