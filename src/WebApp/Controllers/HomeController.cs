using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Notifications;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.ViewModels;

namespace WebApp.Controllers {
    public class HomeController : BaseController {

        private readonly IMeetingServices _meetingServices;
        private readonly IMeetingSetupServices _meetingSetupServices;
        public HomeController (IMeetingServices meetingServices, IMeetingSetupServices meetingSetupServices, IMapper mapper, ILogger<BaseController> looger, INotificador notificador) : base (mapper, looger, notificador) {
            _meetingServices = meetingServices;
            _meetingSetupServices = meetingSetupServices;
        }

        public IActionResult Index () {
            return View (new MeetingViewModel ());
        }

        [HttpPost]

        public async Task<IActionResult> CreateParticipant (MeetingViewModel meetingViewModel) {

            ModelState.Remove ("Data");
            ModelState.Remove ("Id");

            if (!ModelState.IsValid)
                return Json (new { sucess = false, data = meetingViewModel });

            await _meetingServices.Add (_mapper.Map<Meeting> (meetingViewModel));

            if (!OperacaoValida ())
                return Json (new { sucess = false, data = meetingViewModel });

            return Json (new { sucess = true, data = meetingViewModel });

        }

        public IActionResult Privacy () {
            return View ();
        }

        [Route ("you-get-an-error/{id}")]
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