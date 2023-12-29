using AuthenticationAPI.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthenticationAPI.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		string apiUrl = "https://localhost:44335/api/Sample"; 
		string apiKey = "Value";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("ApiKey", apiKey);

				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					ViewBag.Content = content;
					Console.WriteLine($"API Response: {content}");
				}
				else
				{
					Console.WriteLine($"API Request Failed: {response.StatusCode}");
				}
			}
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
	}
}
