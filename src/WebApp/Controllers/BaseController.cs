using AutoMapper;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers {
    public class BaseController : Controller {
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _Looger;
        protected readonly INotificador _notificador;

        public BaseController (IMapper mapper, ILogger<BaseController> looger, INotificador notificador) {
            _mapper = mapper;
            _Looger = looger;
            _notificador = notificador;
        }

        public bool OperacaoValida () {
            return !_notificador.TemNotificacao ();
        }

    }
}