using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubscriptionProvider.Data;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Factories;
using SubscriptionProvider.Models;
using System.Linq.Expressions;

namespace SubscriptionProvider.Repositories;

public class SubscriberRepository(DataContext context, ILogger<SubscriberRepository> logger)
{
    private readonly DataContext _context = context;
    private readonly ILogger<SubscriberRepository> _logger = logger;

    public async Task<ResponseResult> CreateAsync(SubscriberEntity subscriber)
    {
		try
		{
			await _context.Subscribers.AddAsync(subscriber);
			await _context.SaveChangesAsync();
			return ResponseFactory.Ok(subscriber);
		}
		catch (Exception ex)
		{
			_logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberRepository.CreateAsync() : {ex.Message}");
			return ResponseFactory.Error(ex.Message);
		}
    }
	public async Task<ResponseResult> GetAllAsync()
	{
		try
		{
			IEnumerable<SubscriberEntity> list = await _context.Subscribers.ToListAsync();
			if (!list.Any())
				return ResponseFactory.NotFound();
			return ResponseFactory.Ok(list);
		}
		catch (Exception ex)
		{
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberRepository.GetAllAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
	}
	public async Task<ResponseResult> GetAsync(Expression<Func<SubscriberEntity, bool>> predicate)
	{
		try
		{
			var entity = await _context.Subscribers.FirstOrDefaultAsync(predicate);
			if(entity == null)
				return ResponseFactory.NotFound();
			return ResponseFactory.Ok(entity);
		}
		catch (Exception ex)
		{
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberRepository.GetAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
	}
	public async Task<ResponseResult> UpdateAsync(Expression<Func<SubscriberEntity, bool>> predicate, SubscriberEntity subscriber)
	{
		try
		{
			var result = await ExistsAsync(predicate);
			if(result.StatusCode == StatusCode.EXISTS)
			{
				_context.Subscribers.Update(subscriber);
				await _context.SaveChangesAsync();
				return ResponseFactory.Ok(subscriber);
			}
			else if (result.StatusCode == StatusCode.NOT_FOUND)
				return ResponseFactory.NotFound();
			return ResponseFactory.Error();
		}
		catch (Exception ex)
		{
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberRepository.UpdateAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
	}
	public async Task<ResponseResult> DeleteAsync(Expression<Func<SubscriberEntity, bool>> predicate)
	{
		try
		{
			var result = await GetAsync(predicate);
			if(result.StatusCode == StatusCode.OK)
			{
				var entity = (SubscriberEntity)result.ContentResult!;
				_context.Subscribers.Remove(entity);
				await _context.SaveChangesAsync();
				return ResponseFactory.Ok();
			}
			else if (result.StatusCode == StatusCode.NOT_FOUND)
				return ResponseFactory.NotFound();
			return ResponseFactory.Error();
		}
		catch (Exception ex)
		{
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberRepository.DeleteAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
	}
	public async Task<ResponseResult> ExistsAsync(Expression<Func<SubscriberEntity, bool>> predicate)
	{
		try
		{
			if (await _context.Subscribers.AnyAsync(predicate))
				return ResponseFactory.Exists();
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
            _logger.LogDebug($"ERROR :: SubcriptionProvider.SubscriberRepository.ExistsAsync() : {ex.Message}");
            return ResponseFactory.Error(ex.Message);
        }
	}
}
