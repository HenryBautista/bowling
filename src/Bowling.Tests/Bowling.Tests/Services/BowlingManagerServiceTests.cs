namespace Bowling.Tests.Services;

using AutoFixture;
using Bowling.Entities;
using Bowling.Interfaces;
using Bowling.App.Services;
using FakeItEasy;
using FluentAssertions;

public class BowlingManagerServiceTests
{

    private readonly BowlingManagerService Sut;
    private readonly IFrameService FrameService;
    private readonly IGameService GameService;
    private readonly Fixture Fixture;

    public BowlingManagerServiceTests()
    {
        this.FrameService = A.Fake<FrameService>();
        this.GameService = A.Fake<GameService>();
        this.Sut = new BowlingManagerService(
            this.FrameService,
            this.GameService);
        this.Fixture = new Fixture();
    }

}