using Microsoft.AspNetCore.Mvc;
using NetCoreRedisEntegration.Models;
using NetCoreRedisEntegrationBusiness.Interfaces;

namespace NetCoreRedisEntegration.Controllers
{
    [ApiController]
    [Route("api")]
    public class CacheController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;
        
        public CacheController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        [HttpPost("cache/{key}")]
        public async Task<IActionResult> GetData(string key)
        {
            return Ok(await _redisCacheService.GetValueAsync(key));
        }

        [HttpPost("cache/set")]
        public async Task<IActionResult> Set([FromBody] RedisCacheRequestModel redisCacheRequestModel)
        {
            await _redisCacheService.SetValueAsync(redisCacheRequestModel.Key, redisCacheRequestModel.Value);
            return Ok();
        }

        [HttpPost("cache/delete/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _redisCacheService.Clear(key);
            return Ok();
        }
    }
}