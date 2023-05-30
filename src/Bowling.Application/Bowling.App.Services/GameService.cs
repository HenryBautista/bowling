namespace Bowling.App.Services;

using System.Collections.Generic;
using Bowling.App.Services.Validation;
using Bowling.Entities;
using Bowling.Interfaces;
using FluentValidation;
using LanguageExt.Common;

public class GameService : IGameService
{

    private readonly IGameRepository GameRepository;
    private readonly GameValidator GameValidator;

    public GameService(IGameRepository gameRepository)
    {
        this.GameRepository = gameRepository;
        this.GameValidator = new GameValidator();
    }

    public async Task<Result<Game>> CreateGameAsync(Game game)
    {
        var validationResult = await this.GameValidator.ValidateAsync(game);
        
        if (!validationResult.IsValid)
        {
            var error = new ValidationException(validationResult.Errors);
            return new Result<Game>(error);
        }

        var response = await this.GameRepository.AddAsync(game);
        return response;
    }

    public async Task<IReadOnlyList<Game>> GetAllAsync()
    {
        return await this.GameRepository.GetAllAsync();
    }

    public async Task<Result<Game>> GetGameResultAsync(int id)
    {
        var response = await this.GameRepository.GetByIdAsync(id);
        
        if(response == null)
        {
            var error = new Exception("Not found");
            return new Result<Game>(error);
        }

        return response;
    }

    public async Task<Game?> GetGameAsync(int id)
    {
        return await this.GameRepository.GetByIdAsync(id);
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        var validationResult = await this.GameValidator.ValidateAsync(game);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await this.GameRepository.UpdateAsync(game);

        return game;
    }
}
