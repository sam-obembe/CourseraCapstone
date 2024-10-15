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
    
    public MembersController(ILogger<MembersController> logger, CongressApiService congressApiService)
    {
        _logger = logger;
        _congressApiService = congressApiService;
    }

    
    [HttpGet]
    public async Task<ActionResult<List<CongressMemberDto>>> Get()
    {
        var members = await _congressApiService.GetMembers(0, 50,118);
        if (members.Count == 0)
        {
            return NotFound();
        }
        return Ok(members);
    }
}