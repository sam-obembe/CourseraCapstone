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

    [HttpPut("Members")]
    public async Task<SynchronizationSummaryDto> Post(int? congress)
    {
        _logger.LogInformation("Synchronizing information from congress api service");
        var summary = new SynchronizationSummaryDto();
        
        var congressNumber = congress;
        if (congressNumber is null)
        {
            var congressSummary = await _congressApiService.SynchronizeCongress();
            congressNumber = congressSummary.Congress;
        }
        var memberSummary = await _congressApiService.SynchronizeCongressMembers((int)congressNumber);
        summary.Congress = (int)congressNumber;
        summary.CongressMemberCount = memberSummary.CongressMemberCount;
        return summary;
    }

    [HttpPut("Bills")]
    public async Task<SynchronizationSummaryDto> SynchronizeBills(int congress)
    {
        var summary = new SynchronizationSummaryDto();
        var billSummary = await _congressApiService.SynchronizeBills(congress);
        summary.Congress = congress;
        summary.Bills = billSummary.Bills;

        return summary;
    }

    [HttpPut("Congress")]
    public async Task<SynchronizationSummaryDto> SynchronizeCongress()
    {
        var summary = new SynchronizationSummaryDto();
        var congressSummary = await _congressApiService.SynchronizeCongress();
        summary.Congress = congressSummary.Congress;
        return summary;
    }
}