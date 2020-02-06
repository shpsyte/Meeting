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

        // TODO:  Create a class to inject here... too much inject....
        public HomeController (IMeetingServices meetingServices,
            IMeetingSetupServices meetingSetupServices,
            IMapper mapper,
            ILogger<BaseController> looger, INotificador notificador) : base (mapper, looger, notificador) {
            _meetingServices = meetingServices;
            _meetingSetupServices = meetingSetupServices;
        }

        [Route ("confirm-to-get-link")]
        public IActionResult Index () {
            return View (new MeetingViewModel ());
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant (MeetingViewModel meetingViewModel) {

            ModelState.Remove ("Data");
            ModelState.Remove ("Id");
            var meetingSetup =
                (await _meetingSetupServices.GetAtualMeeting ()) ?? new MeetingSetup ();

            ///TODO: Refatorar, muita regra aqui
            // is this a controller or a Business Layer ?????
            if (!ModelState.IsValid) {
                return Json (new {
                    success = false, errors = ErrorInModel (), data = meetingViewModel, atualmetting = ""
                });
            }

            /// TODO: should be in Services
            if (await _meetingServices.CheckIfIsAlreadyRegistered (_mapper.Map<Meeting> (meetingViewModel))) {
                return Json (new {
                    success = true, data = meetingViewModel, atualmetting = meetingSetup
                });
            }

            await _meetingServices.Add (_mapper.Map<Meeting> (meetingViewModel));

            if (!OperacaoValida ()) return Json (new {
                success = false, errors = ErrorInModel (),
                    data = meetingViewModel,
                    atualmetting = ""
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

            // TODO: Refatorar
            // too much if here!
            // controller should have many resp... 
            // if something change in other part can broker this code

            if (meetingSetupPreviously != null) {
                meetingSetupPreviously.Link = meetingSetup.Link;
                await _meetingSetupServices.Update (meetingSetupPreviously);
            } else {
                var dataSmeetingSetup = await _meetingSetupServices.GetNewMeetingSetup ();
                dataSmeetingSetup.Link = meetingSetup.Link;
                await _meetingSetupServices.Add (dataSmeetingSetup);
            }

            if (!OperacaoValida ()) return Json (new {
                success = false, errors = ErrorInModel ()
            });

            return Json (new {
                success = true, meetingSetup = meetingSetup
            });

        }

        public async Task<JsonResult> GetParticipants () {
            return Json (new { success = true, data = (await _meetingServices.GetAllParticipantsToday ()) });
        }

        [Route ("you-get-an-error/{id}")]
        public IActionResult Error (int id) {
            // TODO: Correct this one
            var error = new ErrorViewModel () {
                ErrorCode = id,
                Title = "Title",
                Message = "Error"

            };
            return View ("Error", error);
        }
    }
}