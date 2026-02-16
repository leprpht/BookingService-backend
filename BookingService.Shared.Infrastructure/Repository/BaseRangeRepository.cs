using BookingService.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingService.Shared.Infrastructure.Repository;

public abstract class BaseRangeRepository<TModel>(BookingServiceDbContext context) : IBaseRangeRepository<TModel>
    where TModel : class
{
    protected readonly BookingServiceDbContext Context = context;
    protected readonly DbSet<TModel> DbSet = context.Set<TModel>();
    protected abstract Expression<Func<TModel, bool>> ByParentId(int parentId);
    protected abstract string ParentIdPropertyName { get; }

    public virtual async Task AddRangeAsync(List<TModel> entities)
    {
        var list = entities.ToList();
        if (list.Count == 0)
            return;

        await DbSet.AddRangeAsync(list);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(List<TModel> entities)
    {
        if (entities.Count == 0)
            throw new ArgumentException("No entities to update.");
        
        var updateIds = entities
            .Select(e => e
                .GetType()
                .GetProperty("Id")!
                .GetValue(e))
            .ToHashSet();
        
        var toDelete = await DbSet
            .Where(ByParentId((int) entities
                .First()
                .GetType()
                .GetProperty(ParentIdPropertyName)!
                .GetValue(entities.First())!))
            .Where(e => !updateIds.Contains(e.GetType().GetProperty("Id")!.GetValue(e)))
            .ToListAsync();
        
        DbSet.RemoveRange(toDelete);
        DbSet.UpdateRange(entities);
        await Context.SaveChangesAsync();
    }
    
    public virtual async Task DeleteRangeAsync(int parentId)
    {
        var toDelete = await DbSet
            .Where(ByParentId(parentId))
            .ToListAsync();

        if (toDelete.Count == 0)
            throw new ArgumentException("No entities to delete.");

        DbSet.RemoveRange(toDelete);
        await Context.SaveChangesAsync();
    }
}