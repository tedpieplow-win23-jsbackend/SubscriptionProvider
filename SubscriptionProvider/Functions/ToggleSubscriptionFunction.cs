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
                    var modelResult = _subscribeService.PopulateAndValidateToggleSubscriberModel(body);
                    if (modelResult.StatusCode == StatusCode.OK)
                    {
                        var model = (ToggleSubscriberModel)modelResult.ContentResult!;
                        var toggleResult = await _subscribeService.ToggleSubscriptionAsync(model);
                        if (toggleResult.StatusCode == StatusCode.OK)
                            return new OkObjectResult((SubscriberEntity)toggleResult.ContentResult!);
                        else if (toggleResult.StatusCode == StatusCode.EXISTS) 
                            return new ConflictObjectResult((SubscriberEntity)toggleResult.ContentResult!);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"ERROR :: SubcriptionProvider.ToggleSubscriptionFunction.Run() : {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
