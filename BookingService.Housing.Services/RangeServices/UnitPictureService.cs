using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data.RangeRepositories;

namespace BookingService.Housing.Services.RangeServices;

public class UnitPictureService(IUnitPictureRepository repository, IMapper mapper)
    : BaseRangeService<UnitPicture, UnitPictureCreationDto, UnitPictureUpdateDto>(repository), IUnitPictureService
{
    protected override UnitPicture MapCreate(Guid id, UnitPictureCreationDto dto) => dto.ToUnitPicture(id, mapper);
    protected override UnitPicture MapUpdate(Guid id, UnitPictureUpdateDto dto) => dto.ToUnitPicture(id, mapper);
}