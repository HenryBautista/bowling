using Bowling.App.Services;
using Bowling.App.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BowlingManagerController : ControllerBase
{
    private readonly IBowlingManagerService BowlingManagerService;    
    private readonly ILogger<BowlingManagerController> Logger;

    public BowlingManagerController(
        IBowlingManagerService bowlingManagerService,
        ILogger<BowlingManagerController> logger)
    {
        this.BowlingManagerService = bowlingManagerService;
        this.Logger = logger;
    }

    [HttpPost("roll", Name ="Roll")]
    [ProducesResponseType(typeof(RollResultDto), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> Roll([FromBody] RollDto roll)
    {
        try
        {
            var result = await this.BowlingManagerService.RollAsync(roll);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("score/{gameId}")]
    [ProducesResponseType(typeof(RollResultDto), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetScore([FromRoute] int gameId)
    {
        try
        {
            var result = await this.BowlingManagerService.GetScoreAsync(gameId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
