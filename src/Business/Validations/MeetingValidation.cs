using Business.Models;
using FluentValidation;

namespace Business.Validations {
    public class MeetingValidation : AbstractValidator<Meeting> {
        public MeetingValidation () {
            RuleFor (p => p.Id)
                .NotNull ();

            RuleFor (p => p.Email)
                .NotEmpty ();

        }

    }
}