using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Models;
using SubscriptionProvider.Services;

namespace SubscriptionProvider.Functions
{
    public class ToggleSubscriptionFunction(ILogger<ToggleSubscriptionFunction> logger, SubscribeService subscribeService)
    {
        private readonly SubscribeService _subscribeService = subscribeService;
        private readonly ILogger<ToggleSubscriptionFunction> _logger = logger;

        [Function("ToggleSubscriptionFunction")]
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
                        var toggleResult = await _subscribeService.ToggleSubscriptionAsync(entity);
                        if (toggleResult.StatusCode == StatusCode.OK)
                        {
                            return new OkResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberFunction.Run() : {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
