using BookingService.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingService.Shared.Repository;

public abstract class BaseSubrepository<TModel>(BookingServiceDbContext context) : IBaseSubrepository<TModel>
    where TModel : class
{
    protected readonly BookingServiceDbContext Context = context;
    protected readonly DbSet<TModel> DbSet = context.Set<TModel>();
    protected abstract Expression<Func<TModel, bool>> ByParentId(int parentId);
    protected abstract string ParentIdPropertyName { get; }

    public virtual async Task<List<TModel>> GetAllAsync(int id)
    {
        return await DbSet
            .Where(ByParentId(id))
            .ToListAsync();
    }

    public virtual async Task AddRangeAsync(IEnumerable<TModel> entities)
    {
        var list = entities.ToList();
        if (list.Count == 0)
            return;

        await DbSet.AddRangeAsync(list);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TModel> entities)
    {
        var list = entities.ToList();
        if (list.Count == 0)
            return;
        
        var updateIds = list
            .Select(e => e
                .GetType()
                .GetProperty("Id")!
                .GetValue(e))
            .ToHashSet();
        
        var toDelete = await DbSet
            .Where(ByParentId((int) list
                .First()
                .GetType()
                .GetProperty(ParentIdPropertyName)!
                .GetValue(list.First())!))
            .Where(e => !updateIds.Contains(e.GetType().GetProperty("Id")!.GetValue(e)))
            .ToListAsync();
        
        DbSet.RemoveRange(toDelete);
        DbSet.UpdateRange(list);
        await Context.SaveChangesAsync();
    }
    
    public virtual async Task DeleteRangeAsync(int parentId)
    {
        var toDelete = await DbSet
            .Where(ByParentId(parentId))
            .ToListAsync();

        if (toDelete.Count == 0)
            return;

        DbSet.RemoveRange(toDelete);
        await Context.SaveChangesAsync();
    }
}