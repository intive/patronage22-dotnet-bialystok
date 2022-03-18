using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Controllers
{
    public class LogDate
    {
        public string Date { get; set; }
    }

    [Route("api/logs")]
    [ApiController]
    [AllowAnonymous]
    public class LogsController : Controller
    {
        [HttpGet]
        public IActionResult Logs(string? username, string? file)
        {
            string[] fileEntries = Directory.GetFiles(@"../../logs");

            List<SelectListItem> LogDate = new List<SelectListItem>();
          
            foreach(string fileEntry in fileEntries)
            {
                LogDate.Add(new SelectListItem() { Text = $"{fileEntry.Remove(0, 11)}", Value = $"{fileEntry.Remove(0,11)}" });
         
                

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

     

        [HttpGet("no")]
        public string ChooseFile()
        {
            var file = Request.Form["number"];
            return file;
        }

        [HttpGet("{file}")]
        public string? ReadResource(string file)
        {
             string[] fileEntries = Directory.GetFiles(@"../../logs");
            // File name  
            string fileName = $@"../../logs/{file}";
            try
            {
                // Create a StreamReader  
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