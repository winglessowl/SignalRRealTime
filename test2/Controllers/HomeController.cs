using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISignalRService service;
        public HomeController(ISignalRService _service)
        {
            service = _service;
          
            
        }

        [HttpPost]
        public ActionResult ObtenerLista() 
        {
            string test = string.Empty;
            return Content(test);
        }

        public IActionResult Index()
        {
            service.Conectar();
            return View();
        }

        //[HttpPost]
        //public ActionResult GetPersonas() 
        //{
           

        //   return Json();

        //}

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
