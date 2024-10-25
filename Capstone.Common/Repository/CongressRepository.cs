using Capstone.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Capstone.Common.Repository;

public class CongressRepository(CapstoneContext context, ILogger<CongressRepository> logger):IRepository<Congress>
{
    public Task<IEnumerable<Congress>> GetAsync(int? skip, int? take)
    {
        throw new NotImplementedException();
    }

    public async Task<Congress> CreateAsync(Congress entity)
    {
        var existing = context.Congress.Where(c => c.Number == entity.Number).ToList();

        if (existing.Count != 0)
        {
            var ent = existing.First();
            ent.Name = entity.Name;
            ent.StartYear = entity.StartYear;
            ent.EndYear = entity.EndYear;
            ent.ModifiedDate = DateTime.Now;
            ent.ModifiedBy = entity.ModifiedBy;
            context.Congress.Update(ent);
        }
        else
        {
            await context.Congress.AddAsync(entity);
        }
        await context.SaveChangesAsync();
        return entity;
    }

    public Congress Create(Congress entity)
    {
        var ent = context.Congress.Update(entity);
        context.SaveChanges();
        return ent.Entity;
    }

    public Task<Congress?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Congress>> CreateAsync(List<Congress?> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(List<Congress> entities)
    {
        throw new NotImplementedException();
    }
}