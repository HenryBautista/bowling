namespace Bowling.App.Services;

using Bowling.App.Services.Constants;
using Bowling.App.Services.Dtos;
using Bowling.Entities;

public class ScoreService : IScoreService
{
    private readonly IFrameService FrameService;

    public ScoreService(IFrameService frameService)
    {
        this.FrameService = frameService;
    }

    public async Task<ScoreDto> GetScoreAsync(int gameId)
    {
        var frames = await this
            .FrameService
            .GetFramesByGameAsync(gameId);

        int totalScore = 0;
        bool isPrevFrameStrike = false;
        bool isPrevFrameSpare = false;

        foreach (var frame in frames)
        {
            var frameScore = CalculateFrameScore(
                frame,
                isPrevFrameStrike, 
                isPrevFrameSpare);

            frame.Score = frameScore;

            totalScore += frameScore;

            isPrevFrameStrike = IsPreviousFrameStrike(
                frame.Rolls.Count,
                frame.Rolls);
            isPrevFrameSpare = IsPreviousFrameSpare(
                frame.Rolls.Count,
                frame.Rolls);
        }

        return new ScoreDto 
        {
            GameId = gameId,
            TotalScore = totalScore
        };
    }

    private int CalculateFrameScore(
        Frame frame,
        bool isPrevFrameStrike, 
        bool isPrevFrameSpare)
    {
        int frameScore = frame.Rolls.Sum();

        if (isPrevFrameStrike)
        {
            frameScore += frame.Rolls.Sum();
        }
        else if (isPrevFrameSpare)
        {
            frameScore += frame.Rolls[0];
        }

        return frameScore;
    }
    
    private bool IsPreviousFrameStrike(
        int count,
        List<int> rolls) => count > 0
    && rolls[0] == BowlingConstants.MAX_FRAME_ROLL_VALUE;

    private bool IsPreviousFrameSpare(
        int count,
        List<int> rolls) => count > 1 
    && rolls[0] + rolls[1] == BowlingConstants.MAX_FRAME_ROLL_VALUE;

}
