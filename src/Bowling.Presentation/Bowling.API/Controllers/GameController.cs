using Bowling.App.Services;
using Bowling.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService GameService;    
    private readonly ILogger<GameController> Logger;

    public GameController(
        IGameService gameService,
        ILogger<GameController> logger)
    {
        this.GameService = gameService;
        this.Logger = logger;
    }

    [HttpGet(Name = "GetAllGames")]
    public async Task<IActionResult> Get()
    {
        var response = await this.GameService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetGame")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGame(int id)
    {
        var result = await this.GameService.GetGameResultAsync(id);

        return result.Match<IActionResult>(
            game => Ok(game),
            error => NotFound(error));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody]Game game)
    {
        var result = await this.GameService.CreateGameAsync(game);

        return result.Match<IActionResult>(
            game => CreatedAtAction(nameof(GetGame),
                new { id = game.Id},
                game),
            error => BadRequest(error));
    }

}
