using BookingService.Database;
using BookingService.Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Shared.Infrastructure.Repository;

public abstract class BaseRepository<T>(BookingServiceDbContext context) : IBaseRepository<T> where T : class
{
    protected readonly BookingServiceDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);
        
        if (entity == null)
            throw new NotFoundException("Entity not found.");
        
        return entity;
    }

    public virtual async Task<T> GetByOwnerIdAsync(int ownerId)
    {
        var parent = await DbSet.FirstOrDefaultAsync(p => p
            .GetType()
            .GetProperty("OwnerId")!
            .GetValue(p)!.Equals(ownerId));
        
        if (parent == null)
            throw new NotFoundException("Parent entity not found.");
        
        return parent;
    }

    public virtual async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id, int ownerId)
    {
        var entity = await GetByOwnerIdAsync(ownerId);

        if (entity == null)
            throw new NotFoundException("Entity not found");

        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
}