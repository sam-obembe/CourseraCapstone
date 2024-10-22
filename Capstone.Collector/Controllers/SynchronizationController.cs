using System.Configuration;
using Capstone.Collector.Models;
using Capstone.Collector.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Collector.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SynchronizationController : ControllerBase
{
  private readonly ILogger<SynchronizationController> _logger;
  private readonly CongressApiService _congressApiService;
  public SynchronizationController(ILogger<SynchronizationController> logger, CongressApiService congressApiService)
  {
    _logger = logger;
    _congressApiService = congressApiService;
  }

  [HttpPost]
  public async Task<SynchronizationSummaryDto> Post()
  {
    _logger.LogInformation("Synchronizing information from congress api service");
    var summary = new SynchronizationSummaryDto();
    var congressSummary = await _congressApiService.SynchronizeCongress();
    var memberSummary = await _congressApiService.SynchronizeCongressMembers(congressSummary.Congress);
    
    summary.Congress = congressSummary.Congress;
    summary.CongressMemberCount = memberSummary.CongressMemberCount;
   
    return summary;
  }
}