using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Models;
using SubscriptionProvider.Services;

namespace SubscriptionProvider.Functions
{
    public class UpdateSubscriberFunction(ILogger<UpdateSubscriberFunction> logger, SubscribeService subscribeService)
    {
        private readonly SubscribeService _subscribeService = subscribeService;
        private readonly ILogger<UpdateSubscriberFunction> _logger = logger;

        [Function("UpdateSubscriberFunction")]
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
                        var updateResult = await _subscribeService.UpdateSubscriberAsync(entity);
                        if (updateResult.StatusCode == StatusCode.OK)
                            return new OkResult();
                        else if(updateResult.StatusCode == StatusCode.NOT_FOUND)
                            return new NotFoundResult();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"ERROR :: SubcriptionProvider.UpdateSubscriberFunction.Run() : {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
