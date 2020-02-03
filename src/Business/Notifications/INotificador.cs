using System.Collections.Generic;

namespace Business.Notifications {
  public interface INotificador {

    bool TemNotificacao ();
    List<Notificacao> ObterNotificacao ();
    void Handle (Notificacao notificacao);
  }
}