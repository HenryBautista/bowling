using Bowling.App.Services;
using Bowling.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService PlayerService;    
    private readonly ILogger<PlayerController> Logger;

    public PlayerController(
        IPlayerService playerService,
        ILogger<PlayerController> logger)
    {
        this.PlayerService = playerService;
        this.Logger = logger;
    }

    [HttpGet("{id}", Name = "GetPlayer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayer(int id)
    {
        var result = await this.PlayerService.GetPlayerAsync(id);

        return result.Match<IActionResult>(
            player => Ok(player),
            error => NotFound(error));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody]Player player)
    {
        var result = await this.PlayerService.CreatePlayerAsync(player);

        return result.Match<IActionResult>(
            player => CreatedAtAction(nameof(GetPlayer),
                new { id = player.Id},
                player),
            error => BadRequest(error));
    }

}
