using BookingService.Database;
using BookingService.Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingService.Shared.Infrastructure.Repository;

public abstract class BaseRangeRepository<TModel, TParent>(BookingServiceDbContext context) : IBaseRangeRepository<TModel>
    where TModel : class
    where TParent : class
{
    protected readonly BookingServiceDbContext Context = context;
    protected readonly DbSet<TModel> DbSet = context.Set<TModel>();
    protected readonly DbSet<TParent> ParentDbSet = context.Set<TParent>();
    protected abstract Expression<Func<TModel, bool>> ByParentId(int parentId);
    protected abstract string ParentIdPropertyName { get; }

    public virtual async Task<TModel> GetByOwnerIdAsync(int ownerId)
    {
        var parent = await ParentDbSet.FirstOrDefaultAsync(p => p
            .GetType()
            .GetProperty("OwnerId")!
            .GetValue(p)!.Equals(ownerId));
            
        if (parent == null)
            throw new NotFoundException("Parent entity not found.");

        var entity = await DbSet.FirstOrDefaultAsync(ByParentId(ownerId));
        if (entity == null)
            throw new NotFoundException("Entity not found for the given owner ID.");

        return entity;
    }

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
            throw new NotFoundException("No entities found to delete.");

        DbSet.RemoveRange(toDelete);
        await Context.SaveChangesAsync();
    }
}