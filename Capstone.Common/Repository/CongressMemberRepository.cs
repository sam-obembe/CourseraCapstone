using Capstone.Common.Entities;
using Microsoft.Extensions.Logging;

namespace Capstone.Common.Repository;

public class CongressMemberRepository(CapstoneContext context, ILogger<CongressMemberRepository> logger)
    : IRepository<CongressMember>
{
    public Task<IEnumerable<CongressMember>> GetAsync(int? skip, int? take)
    {
        throw new NotImplementedException();
    }

    public Task<CongressMember> CreateAsync(CongressMember entity)
    {
        throw new NotImplementedException();
    }

    public CongressMember Create(CongressMember entity)
    {
        throw new NotImplementedException();
    }

    public Task<CongressMember?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CongressMember>> CreateAsync(List<CongressMember> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        logger.LogInformation("Creating new CongressMember entities : {}",entities.Count);
        //var congressMembers = entities.ToArray();
        //context.CongressMember.UpdateRange(entities);
        await context.CongressMember.AddRangeAsync(entities);
        await context.SaveChangesAsync();
        return entities;
    }

    public async Task UpdateAsync(List<CongressMember> entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        context.CongressMember.UpdateRange(entity);
        await context.SaveChangesAsync();
    }

    public List<CongressMember> GetAll()
    {
        logger.LogInformation("Getting all CongressMember entities");
        var entities = context.CongressMember.ToList();
        return entities;
    }

    public IEnumerable<CongressMember> Create(List<CongressMember> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        logger.LogInformation("Creating new CongressMember entities : {}",entities.Count);
        context.CongressMember.UpdateRange(entities);
        //context.CongressMember.AddRange(entities);
        context.SaveChanges();
        return entities;
    }
}