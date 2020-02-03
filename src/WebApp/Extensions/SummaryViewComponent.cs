using System.Threading.Tasks;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Extensions {
    public class SummaryViewComponent : ViewComponent {
        private readonly INotificador _notificador;

        public SummaryViewComponent (INotificador notificador) {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync () {
            var notificacaoes = await Task.FromResult (_notificador.ObterNotificacao ());

            notificacaoes.ForEach (a => ViewData.ModelState.AddModelError (string.Empty, a.Message));
            return View ();
        }

    }
}