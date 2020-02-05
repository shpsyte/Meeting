using Business.Models;
using FluentValidation;

namespace Business.Validations {
    public class MeetingValidation : AbstractValidator<Meeting> {
        public MeetingValidation () {
            RuleFor (p => p.Id)
                .NotNull ();

            RuleFor (p => p.Email)
                .NotEmpty ();

            RuleFor (p => ValidEmail (p.Email))
                .Equal (true).WithMessage ("The Email is not a valid Email.");

        }
        public bool ValidEmail (string email) {
            try {
                var addr = new System.Net.Mail.MailAddress (email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

    }

}