using Capstone.Common.Entities;
using Microsoft.Extensions.Logging;

namespace Capstone.Common.Repository;

public class BillRepository(CapstoneContext dbContext, ILogger<BillRepository> logger):IRepository<Bill>
{

    public Task<IEnumerable<Bill>> GetAsync(int? skip, int? take)
    {
        throw new NotImplementedException();
    }

    public Task<Bill> CreateAsync(Bill entity)
    {
        throw new NotImplementedException();
    }

    public Bill Create(Bill entity)
    {
        throw new NotImplementedException();
    }

    public Task<Bill?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Bill>> CreateAsync(List<Bill> entities)
    {
        logger.LogInformation("Creating bills");
        dbContext.Bill.AddRange(entities);
        await dbContext.SaveChangesAsync();
        
        return entities;
    }

    public async Task UpdateAsync(List<Bill> entities)
    {
        dbContext.Bill.UpdateRange(entities);
        await dbContext.SaveChangesAsync();
    }
}