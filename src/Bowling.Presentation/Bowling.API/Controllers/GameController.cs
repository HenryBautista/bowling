using Bowling.Entities;
using Bowling.Services;
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

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> Get()
    {
        var response = await this.GameService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetGame")]
    public async Task<IActionResult> GetGame(int id)
    {
        var response = await this.GameService.GetAllAsync();
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(Game game)
    {
        var response = await this.GameService.CreateGameAsync(game);
        return CreatedAtAction(
            nameof(GetGame),
            new { id = response.Id},
            response
        );
    }
}
