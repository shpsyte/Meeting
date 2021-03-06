﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Extensions;
using Business.Models;
using Business.Notifications;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebApp.ViewModels;
using static WebApp.Controllers.Services;

namespace WebApp.Controllers {
    public class HomeController : BaseController {

        public HomeController (ControllersServices services) : base (services) { }

        [Route ("")]
        [Route ("confirm-to-get-link")]
        public IActionResult Index () => View (new MeetingViewModel ());
        [Route ("admin-class-participant")]
        public async Task<IActionResult> admin () =>
            View (_services._mapper.Map<MeetingSetupViewModel> (await _services._meetingSetupServices.GetAtualMeeting ()));

        [Route ("get-participant")]
        public async Task<JsonResult> GetParticipants () {
            return Json (new { success = true, data = (await _services._meetingServices.GetAllParticipantsToday ()) });
        }

        [Route ("you-get-an-error/{id}")]
        public IActionResult Error (int id) => View (new ErrorViewModel () {
            ErrorCode = id,
                Title = "Title",
                Message = "Error"

        });

        [HttpPost]
        [Route ("create-participant")]
        public async Task<IActionResult> CreateParticipant (MeetingViewModel meetingViewModel) {
            await _services._meetingServices.CreateOrUpdate (_services._mapper.Map<Meeting> (meetingViewModel));

            // nao deveria estar aqui... mas como é pequeno da para gerenciar;.... 
            var meetingSetup =
                (await _services._meetingSetupServices.GetAtualMeeting ()) ?? new MeetingSetup ();

            //await _services._signalRContext.Clients.All.SendAsync ("SendMessage", meetingViewModel);

            return Json (new {
                success = OperacaoValida (),
                    errors = ErrorInModel (),
                    data = meetingViewModel,
                    atualmetting = meetingSetup
            });

        }

        [HttpPost]
        [Route ("create-link-class")]
        public async Task<JsonResult> CreateLinkMeeting (MeetingSetupViewModel meetingSetup) {
            await _services._meetingSetupServices.CreateOrUpdate (_services._mapper.Map<MeetingSetup> (meetingSetup));

            //  hm, talvez criar uma classe especializada aqui....
            if (!string.IsNullOrWhiteSpace (meetingSetup.Link))
                await _services._linkHub.Clients.All.SendAsync ("UpdateLink", meetingSetup.Link);

            return Json (new {
                success = OperacaoValida (),
                    errors = ErrorInModel (),
                    meetingSetup = meetingSetup
            });

        }

    }
}