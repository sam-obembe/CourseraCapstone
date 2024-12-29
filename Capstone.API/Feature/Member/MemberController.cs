using Capstone.Common.Entities;
using Capstone.Common.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.API.Feature.Member;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    
    private CongressMemberRepository memRepo;

    private ILogger<MemberController> _logger;
    public MemberController(ILogger<MemberController> logger, CongressMemberRepository repo)
    {
        this._logger = logger;
        this.memRepo = repo;
    }

    
    [HttpGet]
    public async Task<IEnumerable<CongressMember>> Search([FromQuery]string searchTerm)
    {
        _logger.LogInformation("Searching for member");
        var member =  memRepo.Search(searchTerm);
        return member;
    }
}