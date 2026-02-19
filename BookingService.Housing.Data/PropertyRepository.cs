using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class PropertyRepository(BookingServiceDbContext context)
    : BaseRepository<Property>(context), IPropertyRepository
{
    public override async Task AddAsync(Property property)
    {
        property.UpdateRating();
        await base.AddAsync(property);
    }

    public override async Task UpdateAsync(Property property)
    {
        property.UpdateRating();
        await base.UpdateAsync(property);
    }

    public async Task UpdateNameAsync(Guid propertyId, Guid ownerId, string name)
    {
        var property = await DbSet.SingleOrDefaultAsync(p => p.Id == propertyId);

        if (property == null)
            throw new NotFoundException();
        
        if (property.OwnerId != ownerId)
            throw new ForbidException();
        
        property.Name = name;
        await Context.SaveChangesAsync();
    }

    public async Task UpdateDescriptionAsync(Guid propertyId, Guid ownerId, string description)
    {
        var property = await DbSet.SingleOrDefaultAsync(p => p.Id == propertyId);
        
        if (property == null)
            throw new NotFoundException();
        
        if (property.OwnerId != ownerId)
            throw new ForbidException();
        
        property.Description = description;
        await Context.SaveChangesAsync();
    }

    public async Task UpdateTagsAsync(Guid id, Guid ownerId, List<Guid> tagIds)
    {
        var property = await DbSet
            .Include(p => p.Tags)
            .SingleOrDefaultAsync(p => p.Id == id);
        
        if (property == null)
            throw new NotFoundException();
        
        if (property.OwnerId != ownerId)
            throw new ForbidException();
        
        var tags = await Context.Tags.Where(t => tagIds.Contains(t.Id)).ToListAsync();
        property.Tags.Clear();
        
        foreach (var tag in tags)
        {
            property.Tags.Add(tag);
        }
        await Context.SaveChangesAsync();
    }

    public override async Task DeleteAsync(Guid id, Guid ownerId)
    {
        await Context.Units
            .Where(u => u.PropertyId == id)
            .ExecuteDeleteAsync();

        await Context.PropertyReviews
            .Where(r => r.PropertyId == id)
            .ExecuteDeleteAsync();

        await Context.PropertyPictures
            .Where(p => p.PropertyId == id)
            .ExecuteDeleteAsync();

        await base.DeleteAsync(id, ownerId);
    }
}
