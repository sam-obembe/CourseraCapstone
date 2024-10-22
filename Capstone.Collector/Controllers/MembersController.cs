using Capstone.Collector.Models;
using Capstone.Collector.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Collector.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly ILogger<MembersController> _logger;
    private readonly CongressApiService _congressApiService;
    private const int BATCH_SIZE = 100;
    private const int DEFAULT_SKIP = 0;
    
    public MembersController(ILogger<MembersController> logger, CongressApiService congressApiService)
    {
        _logger = logger;
        _congressApiService = congressApiService;
    }

    
    [HttpGet]
    public async Task<ActionResult<List<CongressMemberDto>>> Get(int congress, int? skip, int? take)
    {
        var members = await _congressApiService.GetMembers(skip ?? DEFAULT_SKIP, take ?? BATCH_SIZE,congress);
        if (members.Count == 0)
        {
            return NotFound();
        }
        return Ok(members);
    }
}