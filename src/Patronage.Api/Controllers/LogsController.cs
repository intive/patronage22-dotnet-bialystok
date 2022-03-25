using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Patronage.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Controllers
{
    [Route("api/logs")]
    [ApiController]
    [AllowAnonymous]
    public class LogsController : Controller
    {
        private readonly IBlobService _blobService;

        public LogsController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> LogsAsync(string? username, string? file)
        {
            await _blobService.GetBlobAsync("herokulogs", "logs/archive");
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