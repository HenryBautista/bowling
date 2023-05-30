using Bowling.Entities;
using FluentValidation;

namespace Bowling.App.Services.Validation
{
    public class FrameValidator : AbstractValidator<Frame>
    {
        public FrameValidator()
        {
            RuleFor(frame => frame.FrameNumber).NotEmpty();
        }
    }
}