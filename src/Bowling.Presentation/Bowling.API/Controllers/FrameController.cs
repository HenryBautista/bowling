using Bowling.App.Services;
using Bowling.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FrameController : ControllerBase
{
    private readonly IFrameService FrameService;    
    private readonly ILogger<FrameController> Logger;

    public FrameController(
        IFrameService frameService,
        ILogger<FrameController> logger)
    {
        this.FrameService = frameService;
        this.Logger = logger;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> Get()
    {
        var response = await this.FrameService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetFrame")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFrame(int id)
    {
        var response = await this.FrameService.GetFrameAsync(id);

        return response.Match<IActionResult>(
            game => Ok(game),
            error => NotFound(error));
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(Frame frame)
    {
        var result = await this.FrameService.CreateFrameAsync(frame);
        
        return result.Match<IActionResult>(
            frame => CreatedAtAction(nameof(GetFrame),
                new { id = frame.Id},
                frame),
            error => BadRequest(error));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchFrame(
        [FromRoute] int id,
        [FromBody] Frame frame)
    {
        var result = await this.FrameService.UpdateFrameAsync(frame);

        return result.Match<IActionResult>( 
            frame => Ok(result),
            error => BadRequest(error));
    }   
}
