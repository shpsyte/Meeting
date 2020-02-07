using System.Collections.Generic;
using AutoMapper;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static WebApp.Controllers.Services;

namespace WebApp.Controllers {
    public class BaseController : Controller {
        protected readonly ControllersServices _services;

        public BaseController (ControllersServices services) {
            _services = services;
        }

        public bool OperacaoValida () {
            return !_services._notificador.TemNotificacao ();
        }

        public List<Notificacao> ErrorInModel () {
            return _services._notificador.ObterNotificacao ();
        }

    }
}