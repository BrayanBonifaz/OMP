using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebPatentes.Controllers
{
    public class ErrorController : Controller
    {
        //[Route("/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404 || statusCode.Value == 500)
                {
                    ViewData["Error"] = string.Concat(statusCode.ToString(),"- Uppsss!!");
                }
            }

            return View();
        }
    }
}