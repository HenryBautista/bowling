namespace Bowling.App.Services;

using System.Threading.Tasks;
using Bowling.App.Services.Dtos;

public class BowlingManagerService : IBowlingManagerService
{
    private readonly IGameService GameService;
    private readonly IFrameService FrameService;

    public BowlingManagerService(
        IFrameService frameService,
        IGameService gameService)
    {
        this.FrameService = frameService;
        this.GameService = gameService;
    }

    public Task<object> GetScore(int gameId)
    {
        throw new NotImplementedException();
    }

    public async Task<RollResultDto> Roll(RollDto roll)
    {
        //Get game
        var game = await this.GameService.GetGameAsync(roll.GameId);

        _ = game ?? throw new Exception("Game not found");

        var result = new RollResultDto { GameId = game.Id };
        
        if(game.IsGameFinished)
        {
            return new RollResultDto { 
                GameId = game.Id,
                Message = $"Game already finished check the Score for Game: {game.Id}"};
        }
        
        //Roll into the current Frame
        var currentFrame = await this.FrameService.RollIntoFrameAsync(
            game.Id,
            game.CurrentFrame,
            roll.PinsDown);
        
        //Check if the Frame is filled to move next
        if (currentFrame.IsFilled)
        {
            game.CurrentFrame ++;
        }
        
        //Check if all frames were filled
        if(game.CurrentFrame >= 9 && currentFrame.IsFilled)
        {
            game.IsGameFinished = true;
            result.Message = "Game Finished!";
        }

        await this.GameService.UpdateGameAsync(game);

        result.Message = "Roll Saved!";

        return result;
    }
}
