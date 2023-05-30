namespace Bowling.App.Services;

using System.Threading.Tasks;
using Bowling.App.Services.Dtos;

public class BowlingManagerService : IBowlingManagerService
{
    private readonly IGameService GameService;
    private readonly IFrameService FrameService;
    private readonly IScoreService ScoreService;

    public BowlingManagerService(
        IFrameService frameService,
        IGameService gameService,
        IScoreService scoreService)
    {
        this.FrameService = frameService;
        this.GameService = gameService;
        this.ScoreService = scoreService;
    }

    public async Task<ScoreDto> GetScoreAsync(int gameId)
    {
        //Get Game
        var game = await this.GameService.GetGameAsync(gameId);
        
        _ = game ?? throw new Exception("Game not found");

        //Call to a Score Service, Send the GameId
        var resultScore = await this.ScoreService.GetScoreAsync(gameId);

        return resultScore;
    }

    public async Task<RollResultDto> RollAsync(RollDto roll)
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
        if(game.CurrentFrame >= 10 && currentFrame.IsFilled)
        {
            game.IsGameFinished = true;
            result.Message = "Game Finished!";
        }

        await this.GameService.UpdateGameAsync(game);

        result.Message = "Roll Saved!";

        return result;
    }
}
