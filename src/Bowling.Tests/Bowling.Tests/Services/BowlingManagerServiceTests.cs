namespace Bowling.Tests.Services;

using AutoFixture;
using Bowling.Entities;
using Bowling.App.Services;
using FakeItEasy;
using FluentAssertions;
using Bowling.App.Services.Dtos;

public class BowlingManagerServiceTests
{

    private readonly BowlingManagerService Sut;
    private readonly IFrameService FrameService;
    private readonly IGameService GameService;
    private readonly Fixture Fixture;

    public BowlingManagerServiceTests()
    {
        this.FrameService = A.Fake<IFrameService>();
        this.GameService = A.Fake<IGameService>();
        this.Sut = new BowlingManagerService(
            this.FrameService,
            this.GameService);
        this.Fixture = new Fixture();
    }

    [Fact]
    public async Task Roll_Should_Return_RollResultDto_When_RollSaved_Message()
    {
        // Arrange
        var gameId = 1;
        var currentFrameNumber = 1;
        char pinsDown = 'X';
        
        var game = new Game {
            Id = gameId,
            CurrentFrame = currentFrameNumber };
        var frame = new Frame {
            GameId = gameId,
            FrameNumber = currentFrameNumber };
        
        A.CallTo(() => 
            this.GameService.GetGameAsync(gameId)).Returns(game);
        A.CallTo(() => 
            this.GameService.UpdateGameAsync(A<Game>.Ignored));

        A.CallTo(() => this.FrameService.RollIntoFrameAsync(
            gameId,
            currentFrameNumber,
            pinsDown))
        .Returns(frame);

        // Act
        var rollDto = new RollDto
        {
            GameId = gameId,
            PinsDown = pinsDown
        };

        var result = await this.Sut.Roll(rollDto);

        // Assert
        result.Should().NotBeNull();
        result.GameId.Should().Be(gameId);
        result.Message.Should().Be("Roll Saved!");

        A.CallTo(() => 
            this.GameService.GetGameAsync(gameId))
        .MustHaveHappenedOnceExactly();

        A.CallTo(() => 
            this.GameService.UpdateGameAsync(game))
        .MustHaveHappenedOnceExactly();

        A.CallTo(() => 
            this.FrameService.RollIntoFrameAsync(
                gameId,
                currentFrameNumber,
                pinsDown))
        .MustHaveHappenedOnceExactly();
    }
}