using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Remembo.Entities;
public class Review : Entity {
    public Guid ContentId { get; set; }
    public DateTime ScheduleReviewDate { get; set; }
    public bool? IsReviewed { get; set; } = false;

    protected Review(Guid id, Guid contentId, DateTime scheduleReviewDate, bool isReviewed) : base(id) {
        ContentId = contentId;
        ScheduleReviewDate = scheduleReviewDate;
        IsReviewed = isReviewed;
    }

    public Review(Guid contentId, DateTime scheduleReviewDate) {
        ContentId = contentId;
        ScheduleReviewDate = scheduleReviewDate;
    }
}
