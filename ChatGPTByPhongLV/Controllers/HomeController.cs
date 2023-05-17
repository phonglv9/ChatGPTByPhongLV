using ChatGPTByPhongLV.Models;
using ChatGPTByPhongLV.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ChatGPTByPhongLV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendChatGPT([FromBody] string question)
        {
            var apiKey = "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";
            var apiUrl = "https://api.openai.com/v1/chat/completions"; //Thay thế bằng URL API ChatGPT của bạn
            var client = new ChatGPTClient(apiKey, apiUrl);
            
            var answer = await client.GetChatResponse(question);
            // Gọi API ChatGPT và nhận câu trả lời
           
            // Trả về câu trả lời dưới dạng JSON
            return Ok(new { answer });
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