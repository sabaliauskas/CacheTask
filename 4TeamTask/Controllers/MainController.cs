using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace _4TeamTask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MainController : ControllerBase
	{
		private readonly IApplicationService _appService;
		private readonly IDistributedCache _cache;

		public MainController(IApplicationService applicationService, IDistributedCache cache)
		{
			_appService = applicationService;
			_cache = cache;
		}

		[HttpGet("RefreshCacheForUser")]
		public async Task<IActionResult> RefreshCacheForUser(long userID)
		{
			try
			{
				var cacheResult = await _cache.GetRecordAsync<UserModel>(userID.ToString());

				if (cacheResult != null)
					await _cache.RemoveAsync(userID.ToString());

				var userData = await _appService.GetUser(userID);

				await _cache.SetRecordAsync<UserModel>(userID.ToString(), userData);

				return Ok($"User: {userID} data has been update in cache");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error refreshing user: {userID}. \n Exception: {ex.Message}");
			}
		}

		[HttpGet("ReturnUserData")]
		public async Task<IActionResult> ReturnUserData(long userID)
		{
			try
			{
				var cacheResult = await _cache.GetRecordAsync<UserModel>(userID.ToString());

				if (cacheResult != null)
				{
					return Ok(cacheResult);
				}
				else
				{
					await RefreshCacheForUser(userID);

					var result = await _cache.GetRecordAsync<UserModel>(userID.ToString());

					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return NotFound($"Error getting user: {userID} data. \n Exception: {ex.Message}");
			}
		}
	}
}