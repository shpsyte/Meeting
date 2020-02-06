﻿using System;
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
            var meetingSetup = await _meetingSetupServices.GetAtualMeeting ();

            ///TODO: Refatorar, muita regra aqui

            if (!ModelState.IsValid) {
                meetingSetup.Link = "";
                return Json (new {
                    success = false, errors = ErrorInModel (), data = meetingViewModel, atualmetting = ""
                });
            }

            if (await _meetingServices.CheckIfIsAlreadyRegistered (_mapper.Map<Meeting> (meetingViewModel))) {
                return Json (new {
                    success = true, data = meetingViewModel, atualmetting = meetingSetup
                });
            }

            await _meetingServices.Add (_mapper.Map<Meeting> (meetingViewModel));

            if (!OperacaoValida ()) return Json (new {
                success = false, errors = ErrorInModel (), data = meetingViewModel, atualmetting = ""
            });

            return Json (new {
                success = true, data = meetingViewModel, atualmetting = meetingSetup
            });

        }

        public async Task<IActionResult> Adm () {
            return View (_mapper.Map<MeetingSetupViewModel> (await _meetingSetupServices.GetAtualMeeting ()));
        }

        [HttpPost]
        public async Task<JsonResult> CreateLinkMeeting (MeetingSetupViewModel meetingSetup) {

            var meetingSetupPreviously = await _meetingSetupServices.GetAtualMeeting ();

            if (meetingSetupPreviously != null) {
                meetingSetupPreviously.Link = meetingSetup.Link;
                await _meetingSetupServices.Update (meetingSetupPreviously);
            } else {
                var dataSmeetingSetup = new MeetingSetup () {
                    Link = meetingSetup.Link
                };
                await _meetingSetupServices.Add (dataSmeetingSetup);
            }

            if (!OperacaoValida ()) return Json (new {
                success = false, errors = ErrorInModel ()
            });

            return Json (new {
                success = true, meetingSetup = meetingSetup
            });

        }

        [HttpGet]
        public async Task<JsonResult> GetParticipants () {
            var listOfParticipants = await _meetingServices.GetAllParticipantsToday ();
            return Json (new { success = true, data = listOfParticipants });
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