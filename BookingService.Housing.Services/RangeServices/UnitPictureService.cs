using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Service;
using BookingServices.Housing.Data.RangeRepositories;

namespace BookingService.Housing.Services.RangeServices;

public class UnitPictureService(IUnitPictureRepository repository, IMapper mapper)
    : BaseRangeService<UnitPicture, UnitPictureCreationDto, UnitPictureUpdateDto>(repository), IUnitPictureService
{
    protected override UnitPicture MapCreate(int id, UnitPictureCreationDto dto) => dto.ToUnitPicture(id, mapper);
    protected override UnitPicture MapUpdate(int id, UnitPictureUpdateDto dto) => dto.ToUnitPicture(id, mapper);
}