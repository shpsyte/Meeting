using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers {
    public class HomeController : Controller {
        public IActionResult Index () {
            return View ();
        }

        public IActionResult Privacy () {
            return View ();
        }

        [Route ("you-get-an-error/{id:lenght(3,3)}")]
        public IActionResult Error (int id) {
            var error = new ErrorViewModel () {
                ErrorCode = id,
                Title = "Title",
                Message = "Error"

            };
            return View ("Error", error);
        }
    }
}