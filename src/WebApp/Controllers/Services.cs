using System;
using AutoMapper;
using Business.Notifications;
using Business.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebApp.Hubs;
using WebApp.ViewModels;

namespace WebApp.Controllers {
    public class Services {

        public interface IControllersServices {

            MeetingViewModel NewMeettingModel ();
            MeetingSetupViewModel NewMeettingSetupModel ();

        }

        public class ControllersServices : IControllersServices {
            public IMeetingServices _meetingServices;
            public IMeetingSetupServices _meetingSetupServices;
            public IMapper _mapper;

            public ILogger<BaseController> _Looger;
            public INotificador _notificador;
            public IHubContext<newParticipantHub> _signalRContext;

            public ControllersServices (
                IMeetingServices meetingServices,
                IMeetingSetupServices meetingSetupServices,
                IMapper mapper,
                ILogger<BaseController> looger, INotificador notificador,
                IHubContext<newParticipantHub> signalRContext) {
                _meetingServices = meetingServices;
                _meetingSetupServices = meetingSetupServices;
                _mapper = mapper;
                _Looger = looger;
                _notificador = notificador;
                _signalRContext = signalRContext;

            }

            public MeetingViewModel NewMeettingModel () {
                return new MeetingViewModel () {
                    Id = Guid.NewGuid (),
                        Data = DateTime.UtcNow,
                        Active = true
                };
            }

            public MeetingSetupViewModel NewMeettingSetupModel () {
                return new MeetingSetupViewModel () {
                    Id = Guid.NewGuid (),
                        Data = DateTime.UtcNow

                };
            }
        }
    }
}