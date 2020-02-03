using Business.Models;
using Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services {
    public abstract class BaseServices {

        private readonly INotificador _notificador;

        protected BaseServices (INotificador notifiador) {
            _notificador = notifiador;
        }

        protected void Notificar (ValidationResult validationResult) {
            foreach (var error in validationResult.Errors) {
                Notificar (error.ErrorMessage);

            }
        }

        protected void Notificar (string message) {
            _notificador.Handle (new Notificacao (message));

        }

        protected bool ExecutarValidacao<T, E> (T validacao, E entidade) where T : AbstractValidator<E> where E : Entity {
            var validator = validacao.Validate (entidade);

            if (validator.IsValid) return true;

            Notificar (validator);
            return false;

        }

    }
}