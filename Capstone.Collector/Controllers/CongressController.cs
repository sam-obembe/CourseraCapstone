using Capstone.Collector.Models;
using Capstone.Collector.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Collector.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CongressController : ControllerBase
{
    private readonly ILogger<CongressController> _logger;
    private readonly CongressApiService _congressApiService;
    public CongressController(ILogger<CongressController> logger, CongressApiService congressApiService)
    {
        _logger = logger;
        _congressApiService = congressApiService;
    }

    [HttpGet]
    public async Task<ActionResult<CongressResponseDto>> Get()
    {
       var congress = await _congressApiService.GetCongress();
       return Ok(congress);
    }
}