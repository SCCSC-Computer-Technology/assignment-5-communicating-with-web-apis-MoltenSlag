using AlexGann_CPT_206_Lab_5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlexGann_CPT_206_Lab_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            clientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Students()
        {
            string uri;
            ViewData["Title"] = "All Students";
            uri = "api/StudentProfiles";
            HttpClient client = clientFactory.CreateClient(
                name: "StudentProfileWebApi");
            HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: uri );
            HttpResponseMessage response = await client.SendAsync(request);
            IEnumerable<StudentProfile>? model = await response.Content.ReadFromJsonAsync<IEnumerable<StudentProfile>>();
            return View(model);
        }
    }
}
