using Business.Models;
using FluentValidation;

namespace Business.Validations {
    public class MeetingSetupValidation : AbstractValidator<MeetingSetup> {
        public MeetingSetupValidation () {
            RuleFor (p => p.Id)
                .NotNull ();

            RuleFor (p => p.Data)
                .NotEmpty ();

            RuleFor (p => p.Link)
                .NotEmpty ();

        }
    }
}