using Bowling.Entities;
using FluentValidation;

namespace Bowling.App.Services.Validation
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(game => game.Player).NotEmpty();
        }
    }
}