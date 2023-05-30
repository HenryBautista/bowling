namespace Bowling.App.Services;

using System.Collections.Generic;
using Bowling.App.Services.Constants;
using Bowling.App.Services.Converters;
using Bowling.App.Services.Validation;
using Bowling.Entities;
using Bowling.Interfaces;
using FluentValidation;
using LanguageExt.Common;

public class FrameService : IFrameService
{

    private readonly IFrameRepository FrameRepository;
    private readonly FrameValidator FrameValidator;
    private readonly RollConverter RollConverter;

    public FrameService(IFrameRepository frameRepository)
    {
        this.FrameRepository = frameRepository;
        this.FrameValidator = new FrameValidator();
        this.RollConverter = new RollConverter();

    }

    public async Task<Result<Frame>> CreateFrameAsync(Frame frame)
    {
        var validationResult = await this.FrameValidator.ValidateAsync(frame);
        
        if (!validationResult.IsValid)
        {
            var error = new ValidationException(validationResult.Errors);
            return new Result<Frame>(error);
        }

        var response = await this.FrameRepository.AddAsync(frame);
        return response;
    }

    public async Task<IReadOnlyList<Frame>> GetAllAsync()
    {
        return await this.FrameRepository.GetAllAsync();
    }

    public async Task<Result<Frame>> GetFrameAsync(int id)
    {
        var response = await this.FrameRepository.GetByIdAsync(id);
        
        if(response == null)
        {
            var error = new Exception("Not found");
            return new Result<Frame>(error);
        }

        return response;
    }

    public async Task RemoveFrame(int id)
    {
        await this.FrameRepository.DeleteAsync(id);
    }

    public async Task<Frame> RollIntoFrameAsync(
        int gameId,
        int currentFrameNumber,
        char pinsDown)
    {
        var isCreated = false;
        var frame = await this.FrameRepository.GetFrameByGameAndNumberAsync(
            gameId,
            currentFrameNumber);

        if (frame == null)
        {
            frame = new Frame {
                GameId = gameId,
                FrameNumber = currentFrameNumber};
            isCreated = true;
        }
        
        var points = this.RollConverter.ConvertToInteger(pinsDown, frame);
        SavePointsIntoFrame(frame, points);

        if (isCreated)
        {
            return await this.FrameRepository.AddAsync(frame);
        }

        await this.FrameRepository.UpdateAsync(frame);

        return frame;
    }

    private void SavePointsIntoFrame(Frame frame, int points)
    {
        var currentRoll = frame.Rolls.Count();

        if (frame.IsFilled)
        {
            return;
        }

        if(currentRoll == 0)
        {
            frame.Rolls.Add(points);
            if (points == BowlingConstants.MAX_FRAME_ROLL_VALUE)
            {
                // This is a Strike
                frame.IsFilled = true;
            }
        } 
        else if (currentRoll == 1)
        {
            var previousRoll = frame.Rolls.First();
            var totalFrame = points + previousRoll;

            if (totalFrame >  BowlingConstants.MAX_FRAME_ROLL_VALUE)
            {
                throw new ValidationException("Invalid Points, are greater than 10!");
            }

            frame.Rolls.Add(points);
            frame.IsFilled = true;
        }
        else
        {
            throw new InvalidOperationException("Frame already filled.");
        }
    }

    public async Task<Result<Frame>> UpdateFrameAsync(Frame frame)
    {
        var validationResult = await this.FrameValidator.ValidateAsync(frame);

        if(!validationResult.IsValid)
        {
            var error = new ValidationException(validationResult.Errors);
            return new Result<Frame>(error);
        }

        await this.FrameRepository.UpdateAsync(frame);
        return frame;
    }

    
}
