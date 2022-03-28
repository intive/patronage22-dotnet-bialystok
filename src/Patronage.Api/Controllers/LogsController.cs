using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Patronage.Api.MediatR.AzureBlobs.Commands;

namespace Patronage.Api.Controllers
{
    [Route("api/logs")]
    [ApiController]
    [AllowAnonymous]
    public class LogsController : Controller
    {
        private readonly IMediator _mediator;

        public LogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LogsAsync(string? username, string? file)
        {
            await _mediator.Send(new DownloadBlobsCommand("herokulogs", "logs/archive"));
            string[] fileEntries = Directory.GetFiles(@"./logs/archive");

            List<SelectListItem> LogDate = new List<SelectListItem>();

            foreach (string fileEntry in fileEntries)
            {
                LogDate.Add(new SelectListItem() { Text = $"{fileEntry.Split("/").Last()}", Value = $"{fileEntry.Split("/").Last().Split(@"\").Last()}" });
            };

            ViewBag.CategoryList = LogDate;
            return View("Views/Logs.cshtml");
        }

        [HttpPost]
        public IActionResult LogsDate()
        {
            var file = Request.Form["LogDate"];
            return Redirect($"/api/logs/{file}");
        }

        [HttpGet("{file}")]
        public string? ReadResource(string file)
        {
            string fileName = $@"./logs/archive/{file}";
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string fileReadings;
                    fileReadings = reader.ReadToEnd();
                    return fileReadings;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
            return default;
        }
    }
}