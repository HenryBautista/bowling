namespace Bowling.Tests.Services;

using AutoFixture;
using Bowling.Entities;
using Bowling.Interfaces;
using Bowling.App.Services;
using FakeItEasy;
using FluentAssertions;

public class FrameServiceTests
{
    private readonly IFrameRepository FakeFrameRepository;
    private readonly FrameService Sut;
    private readonly Fixture Fixture;

    public FrameServiceTests()
    {
        this.FakeFrameRepository = A.Fake<IFrameRepository>();
        this.Sut = new FrameService(this.FakeFrameRepository);
        this.Fixture = new Fixture();
    }

    [Fact]
    public async Task RollIntoFrameAsync_NewFrame_CreatesFrameAndReturnsIt()
    {
        // Arrange
        int gameId = 1;
        int currentFrameNumber = 1;
        int pinsDown = 5;

        Frame frame = null;
        A.CallTo(() => this.FakeFrameRepository.GetFrameByGameAndNumberAsync(
            gameId,
            currentFrameNumber))
        .Returns(frame);

        Frame addedFrame = this.Fixture.Create<Frame>();
        A.CallTo(() => this.FakeFrameRepository.AddAsync(A<Frame>._))
            .Returns(addedFrame);

        // Act
        var result = await this.Sut.RollIntoFrameAsync(
            gameId,
            currentFrameNumber,
            pinsDown);

        // Assert
        A.CallTo(() => this.FakeFrameRepository.GetFrameByGameAndNumberAsync(
            gameId,
            currentFrameNumber))
        .MustHaveHappenedOnceExactly();

        A.CallTo(() => this.FakeFrameRepository.AddAsync(A<Frame>._))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => this.FakeFrameRepository.UpdateAsync(A<Frame>._))
            .MustNotHaveHappened();
        
        result.Should().BeEquivalentTo(addedFrame);
    }

    [Fact]
    public async Task RollIntoFrameAsync_ExistingFrame_UpdatesFrameAndReturnsIt()
    {
        // Arrange
        int gameId = 1;
        int currentFrameNumber = 1;
        int pinsDown = 5;

        Frame frame = this.Fixture.Create<Frame>();
        A.CallTo(() => this.FakeFrameRepository.GetFrameByGameAndNumberAsync(
            gameId,
            currentFrameNumber))
        .Returns(frame);

        // Act
        var result = await this.Sut.RollIntoFrameAsync(
            gameId,
            currentFrameNumber,
            pinsDown);

        // Assert
        A.CallTo(() => this.FakeFrameRepository.GetFrameByGameAndNumberAsync(
            gameId,
            currentFrameNumber))
        .MustHaveHappenedOnceExactly();
        
        A.CallTo(() => this.FakeFrameRepository.AddAsync(A<Frame>._))
            .MustNotHaveHappened();

        A.CallTo(() => this.FakeFrameRepository.UpdateAsync(frame))
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalentTo(frame);
    }
}