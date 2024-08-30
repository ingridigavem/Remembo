namespace Remembo.Domain.Remembo.DTOs;
public record ReviewDetailDto(System.Guid ReviewId, System.DateTime ScheduleReviewDate, bool? IsReviewed);