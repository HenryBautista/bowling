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
                frame.FirstRoll);
            isPrevFrameSpare = IsPreviousFrameSpare(
                frame.FirstRoll,
                frame.SecondRoll);
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
        int frameScore = 
            frame.FirstRoll.Value + frame.SecondRoll.Value;

        if (isPrevFrameStrike)
        {
            frameScore += frameScore;
        }
        else if (isPrevFrameSpare)
        {
            frameScore += frame.FirstRoll.Value;
        }

        return frameScore;
    }
    
    private bool IsPreviousFrameStrike(
        int? firstRoll) => firstRoll != null
    && firstRoll == BowlingConstants.MAX_FRAME_ROLL_VALUE;

    private bool IsPreviousFrameSpare(
        int? firstRoll,
        int? secondRoll) => secondRoll != null 
    && firstRoll + secondRoll == BowlingConstants.MAX_FRAME_ROLL_VALUE;

}
