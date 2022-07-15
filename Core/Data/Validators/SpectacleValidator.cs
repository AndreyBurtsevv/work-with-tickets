using BoxOffice.Core.Dto;
using FluentValidation;

namespace BoxOffice.Core.Data.Validators
{
    public class CreateSpectacleValidator : AbstractValidator<CreateSpectacle>
    {
        public CreateSpectacleValidator()
        {
            RuleFor(x => x.StartTime)
                .NotNull().GreaterThan((ulong)0).WithMessage("Please input start time.");   
            
            RuleFor(x => x.EndTime)
                .NotNull().GreaterThan((ulong)0).WithMessage("Please input start time.");

            RuleFor(x => x.TotalTicket)
                .NotNull().GreaterThan((uint)0).WithMessage("Enter the exact number of tickets.");

            RuleFor(x => x).Must(x => x.StartTime < x.EndTime)
                .WithMessage("The time is incorrect.");
        }
    }  
    
    public class SpectacleDtoValidator : AbstractValidator<SpectacleDto>
    {
        public SpectacleDtoValidator()
        {
            RuleFor(x => x.StartTime)
                .NotNull().GreaterThan((ulong)0).WithMessage("Please input start time.");

            RuleFor(x => x.EndTime)
                .NotNull().GreaterThan((ulong)0).WithMessage("Please input start time.");

            RuleFor(x => x.TotalTicket)
                .NotNull().GreaterThan((uint)0).WithMessage("Enter the exact number of tickets.");

            RuleFor(x => x).Must(x => x.StartTime < x.EndTime)
                .WithMessage("The time is incorrect.");
        }
    }
}
