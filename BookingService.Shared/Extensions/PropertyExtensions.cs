using System.Reflection;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Extensions;

public static class PropertyExtensions
{
    public static PropertyListingDto ToPropertyListingDto(this Property property, PeriodRequest period)
    {
        return new PropertyListingDto
        {
            Id = property.Id,
            Name = property.Name,
            Address = property.Address,
            City = property.City,
            State = property.State,
            Country = property.Country,
            Description = property.Description,
            Pictures = property.Pictures
                .OrderByDescending(p => p.IsCover)
                .Select(p => p.Url)
                .ToList(),
            Units = property.Units
                .Select(u => u.ToUnitListDto(period))
                .ToList(),
            Rating = property.Reviews.Any() ? property.Reviews.Average(r => r.Rating) : 0,
            ReviewCount = property.ReviewCount
        };
    }

    public static PropertyPageDto ToPropertyPageDto(this Property property, PeriodRequest period)
    {
        return new PropertyPageDto
        {
            Id = property.Id,
            Name = property.Name,
            Address = property.Address,
            City = property.City,
            State = property.State,
            Country = property.Country,
            Price = property.Units
                .Min(u => u.Price) * period.DaysCount,
            PictureUrl = property.Pictures
                .Where(u => u.IsCover)
                .Select(p => p.Url)
                .FirstOrDefault(),
            Rating = property.AverageRating,
            ReviewCount = property.ReviewCount
        };
    }
    
    public static Property ToProperty(this PropertyCreationDto propertyCreationDto)
    {
        return new Property
        {
            Name = propertyCreationDto.Name,
            Address = propertyCreationDto.Address,
            City = propertyCreationDto.City,
            State = propertyCreationDto.State,
            Country = propertyCreationDto.Country,
            Description = propertyCreationDto.Description,
            OwnerId = propertyCreationDto.OwnerId
        };
    }

    public static Property ToProperty(this PropertyUpdateDto propertyUpdateDto)
    {
        return new Property
        {
            Id = propertyUpdateDto.Id,
            Name = propertyUpdateDto.Name,
            Address = propertyUpdateDto.Address,
            City = propertyUpdateDto.City,
            Country = propertyUpdateDto.Country,
            Description = propertyUpdateDto.Description,
            OwnerId = propertyUpdateDto.OwnerId
        };
    }

    public static PropertyReview ToPropertyReview(this PropertyReviewCreationDto review)
    {
        return new PropertyReview
        {
            PropertyId = review.PropertyId,
            GuestId = review.GuestId,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static PropertyReview ToPropertyReview(this PropertyReviewUpdateDto propertyUpdateDto)
    {
        return new PropertyReview
        {
            Id = propertyUpdateDto.Id,
            Rating = propertyUpdateDto.Rating,
            Comment = propertyUpdateDto.Comment,
            CreatedAt = propertyUpdateDto.CreatedAt,
            Response = propertyUpdateDto.Response,
            GuestId = propertyUpdateDto.GuestId,
            PropertyId = propertyUpdateDto.PropertyId
        };
    }

    public static PropertyInfoDto ToPropertyInfoDto(this Property property)
    {
        return new PropertyInfoDto
        {
            Id = property.Id,
            Name = property.Name,
            PictureUrl = property.Pictures
                .Where(p => p.IsCover)
                .Select(p => p.Url)
                .FirstOrDefault()
        };
    }
    
    public static PropertyReviewDto ToPropertyReviewDto(this PropertyReview review, Guest guest)
    {
        return new PropertyReviewDto
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            Response = review.Response,
            Guest = guest.ToUserInfoDto(),
            Property = review.Property.ToPropertyInfoDto()
        };
    }
}