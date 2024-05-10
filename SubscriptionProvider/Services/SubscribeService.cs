﻿using Microsoft.Extensions.Logging;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Factories;
using SubscriptionProvider.Helpers;
using SubscriptionProvider.Models;
using SubscriptionProvider.Repositories;

namespace SubscriptionProvider.Services;

public class SubscribeService(ILogger<SubscribeService> logger, SubscriberRepository subscriberRepository, SubscriberFactory subscriberFactory)
{
    private readonly ILogger<SubscribeService> _logger = logger;
    private readonly SubscriberRepository _repo = subscriberRepository;
    private readonly SubscriberFactory _subscriberFactory = subscriberFactory;

    public async Task<ResponseResult> CheckIfSubscribedAsync(string email)
    {
        try
        {
            var result = await _repo.ExistsAsync(x => x.Email == email);
            if (result.StatusCode == StatusCode.EXISTS)
                return ResponseFactory.Exists();
            else if (result.StatusCode == StatusCode.NOT_FOUND)
                return ResponseFactory.NotFound();
            return ResponseFactory.Error();
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscribeService.CheckIfSubscribedAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
    }
    public ResponseResult PopulateAndValidateBodyToSubscriberEntity(string body)
    {
        try
        {
            var subscriberEntity = _subscriberFactory.PopulateSubscriberEntity(body);
            var result = ValidateSubscriberEntity(subscriberEntity);
            if (result.StatusCode == StatusCode.OK)
                return ResponseFactory.Ok(subscriberEntity);
            return ResponseFactory.Error();
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscribeService.PopulateAndValidateBodyToSubscriberEntity() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
    }
    public ResponseResult ValidateSubscriberEntity(SubscriberEntity entity)
    {
        try
        {
            var validator = new SubscriberValidator();
            var validationResult = validator.Validate(entity);
            if (validationResult.IsValid)
                return ResponseFactory.Ok();
            return ResponseFactory.Error();
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscribeService.ValidateSubscriberEntity() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
    }
    public async Task<ResponseResult> SaveSubscriberAsync(SubscriberEntity entity)
    {
        try
        {
            var existsResult = await CheckIfSubscribedAsync(entity.Email);
            if (existsResult.StatusCode == StatusCode.NOT_FOUND)
            {
                var saveResult = await _repo.CreateAsync(entity);
                if (saveResult.StatusCode == StatusCode.OK)
                    return ResponseFactory.Ok();

            }
            else if (existsResult.StatusCode == StatusCode.EXISTS)
                return ResponseFactory.Exists();
            return ResponseFactory.Error();
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscribeService.SaveSubscriberAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
