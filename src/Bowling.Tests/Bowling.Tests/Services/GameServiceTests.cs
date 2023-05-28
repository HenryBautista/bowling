namespace Bowling.Tests.Services;

using AutoFixture;
using FakeItEasy;
using FluentAssertions;

public class GameServiceTests
{
    private readonly IGameRepository fakeGameRepository;
    private readonly IGameService sut;
    private readonly Fixture fixture;

    public GameServiceTests()
    {
        fakeGameRepository = A.Fake<IGameRepository>();
        fixture = new Fixture();
        
    }

    [Fact]
    public async Task Create_Game_ShouldBeSuccess()
    {
        // Arrange
        var game = fixture.Create<Game>();
        A.CallTo(() => 
            fakeGameRepository.AddAsync())
        .Returns(game);

        //Act
        var response = await sut.CreateGameAsync(game);

        //Assert
        response.Should().Be(game);
    }
}