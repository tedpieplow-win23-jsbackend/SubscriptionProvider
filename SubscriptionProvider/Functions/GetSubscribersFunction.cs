using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Models;
using SubscriptionProvider.Services;

namespace SubscriptionProvider.Functions
{
    public class GetSubscribersFunction(ILogger<GetSubscribersFunction> logger, SubscribeService subscribeService)
    {
        private readonly ILogger<GetSubscribersFunction> _logger = logger;
        private readonly SubscribeService _subscribeService = subscribeService;

        [Function("GetSubscribersFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                
                if (body == null)
                {
                    var getAllResult = await _subscribeService.GetAllSubscribersAsync();
                    if (getAllResult.StatusCode == StatusCode.OK)
                        return new OkObjectResult((IEnumerable<SubscriberEntity>)getAllResult.ContentResult!);
                    else if (getAllResult.StatusCode == StatusCode.NOT_FOUND)
                        return new OkResult();
                }
                else if(body != null)
                //{
                //    var testResult = _subscribeService.BodyChecker(body);
                //    if(testResult.StatusCode ==  StatusCode.OK)
                //    var populateResult = _subscribeService.PopulateAndValidateBodyToSubscriberEntity(body);
                //    if (populateResult.StatusCode == StatusCode.OK)
                //    {
                //        var entity = (SubscriberEntity)populateResult.ContentResult!;
                //        var getResult = await _subscribeService.GetOneSubscriberAsync(entity.Email);
                //        if (getResult.StatusCode == StatusCode.OK)
                //            return new OkObjectResult((SubscriberEntity)getResult.ContentResult!);
                //        return new OkResult();
                //    }
                //}
                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"ERROR :: SubcriptionProvider.GetSubscribersFunction.Run() : {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
