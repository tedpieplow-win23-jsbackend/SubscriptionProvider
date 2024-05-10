using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Factories;
using SubscriptionProvider.Models;
using SubscriptionProvider.Services;

namespace SubscriptionProvider.Functions
{
    public class SubscribeFunction(ILogger<SubscribeFunction> logger, SubscribeService subscribeService)
    {
        private readonly SubscribeService _subscribeService = subscribeService;
        private readonly ILogger<SubscribeFunction> _logger = logger;

        [Function("SubscribeFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                if (body != null)
                {
                    var entityResult = _subscribeService.PopulateAndValidateBodyToSubscriberEntity(body);
                    if (entityResult.StatusCode == StatusCode.OK)
                    {
                        var entity = (SubscriberEntity)entityResult.ContentResult!;
                        var saveResult = await _subscribeService.SaveSubscriberAsync(entity);
                        if (saveResult.StatusCode == StatusCode.OK)
                            return new OkResult();
                        else if (saveResult.StatusCode == StatusCode.EXISTS)
                            return new ConflictResult();
                    }
                }
                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberFunction.Run() : {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
