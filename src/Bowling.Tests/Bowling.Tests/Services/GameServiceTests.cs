namespace Bowling.Tests.Services;

using AutoFixture;
using Bowling.Entities;
using Bowling.Interfaces;
using Bowling.App.Services;
using FakeItEasy;
using FluentAssertions;

public class GameServiceTests
{
    private readonly IGameRepository FakeGameRepository;
    private readonly GameService Sut;
    private readonly Fixture Fixture;

    public GameServiceTests()
    {
        this.FakeGameRepository = A.Fake<IGameRepository>();
        this.Sut = new GameService(this.FakeGameRepository);

        this.Fixture = new Fixture();
        
    }

    [Fact]
    public async Task CreateGameAsync_NewGame_ShouldBeSuccess()
    {
        // Arrange
        var game = this.Fixture.Create<Game>();
        A.CallTo(() => 
            this.FakeGameRepository.AddAsync(game))
        .Returns(game);

        // Act
        var result = await this.Sut.CreateGameAsync(game);

        // Assert

        A.CallTo(() => this.FakeGameRepository.AddAsync(A<Game>._))
            .MustHaveHappenedOnceExactly();

        result.Should().Be(game);
    }
}