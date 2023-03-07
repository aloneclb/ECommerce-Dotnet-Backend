using ETicaret.Application.Repositories.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ETicaret.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CacheExampleController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICustomerReadRepository _customerReadRepository;

    public CacheExampleController(IMemoryCache memoryCache, ICustomerReadRepository customerReadRepository)
    {
        _memoryCache = memoryCache;
        _customerReadRepository = customerReadRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        const string customerKey = "asd";
        if (_memoryCache.TryGetValue(customerKey, out object? list))
            return Ok(list);
        
        var customers = await _customerReadRepository.GetWhere(x => true).ToListAsync();
        _memoryCache.Set(customerKey, customers, new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(20),
            Priority = CacheItemPriority.Normal
        });
        
        // silmek icin tek yapmak gereken
        // _memoryCache.Remove(customerKey);

        return Ok(customers);
    }
}