namespace Bowling.App.Services;

using System.Collections.Generic;
using Bowling.App.Services.Validation;
using Bowling.Entities;
using Bowling.Interfaces;
using FluentValidation;
using LanguageExt.Common;

public class PlayerService : IPlayerService
{

    private readonly IPlayerRepository PlayerRepository;
    private readonly PlayerValidator PlayerValidator;

    public PlayerService(IPlayerRepository playerRepository)
    {
        this.PlayerRepository = playerRepository;
        this.PlayerValidator = new PlayerValidator();
    }

    public async Task<Result<Player>> CreatePlayerAsync(Player player)
    {
        var validationResult = await this.PlayerValidator.ValidateAsync(player);
        
        if (!validationResult.IsValid)
        {
            var error = new ValidationException(validationResult.Errors);
            return new Result<Player>(error);
        }

        var response = await this.PlayerRepository.AddAsync(player);
        return response;
    }

    public async Task<Result<Player>> GetPlayerAsync(int id)
    {
        var response = await this.PlayerRepository.GetByIdAsync(id);
        
        if(response == null)
        {
            var error = new Exception("Not found");
            return new Result<Player>(error);
        }

        return response;
    }
}
