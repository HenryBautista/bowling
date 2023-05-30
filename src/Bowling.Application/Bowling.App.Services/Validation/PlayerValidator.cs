using Bowling.Entities;
using FluentValidation;

namespace Bowling.App.Services.Validation
{
    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(player => player.Name).NotEmpty();
        }
    }
}