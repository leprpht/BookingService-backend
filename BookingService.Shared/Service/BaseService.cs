using BookingService.Housing.DTOs.Property;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Repository;

namespace BookingService.Shared.Service;

public abstract class BaseService<TModel, TCreateDto, TUpdateDto>(IBaseRepository<TModel> repository)
    : IBaseService<TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    public async Task CreateAsync(TCreateDto createDto)
    {
        var model = MapToModel(createDto);
        await repository.AddAsync(model);
    }

    public async Task UpdateAsync(TUpdateDto updateDto)
    {
        var model = MapToModel(updateDto);
        await repository.UpdateAsync(model);
    }

    public async Task DeleteAsync(int id)
    { 
        await repository.DeleteAsync(id);
    }

    private static TModel MapToModel(TCreateDto dto)
    {
        return dto switch
        {
            PropertyCreationDto p => (TModel) (object) p.ToProperty(),
            PropertyReviewCreationDto r => (TModel) (object) r.ToPropertyReview(),
            StayCreationDto s => (TModel) (object) s.ToStay(),
            UnitCreationDto u => (TModel) (object) u.ToUnit(),
            _ => throw new InvalidOperationException()
        };
    }
    
    private static TModel MapToModel(TUpdateDto dto)
    {
        return dto switch
        {
            PropertyUpdateDto p => (TModel) (object) p.ToProperty(),
            PropertyReviewUpdateDto r => (TModel) (object) r.ToPropertyReview(),
            StayUpdateDto s => (TModel) (object) s.ToStay(),
            UnitUpdateDto u => (TModel) (object) u.ToUnit(),
            _ => throw new InvalidOperationException()
        };
    }
} 