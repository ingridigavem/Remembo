using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.Constants;
using Remembo.Domain.Shared.DTOs;
using System.Net;
using System.Transactions;

namespace Remembo.Service.Remembo;
public class ReviewService(IReviewRepository repository) : IReviewService {
    private const short maximumNumberReviewsReached = 3;

    public async Task<Result<Review>> ScheduleNextReviewAsync(Guid currentReviewId) {
        if (currentReviewId == Guid.Empty) return new Result<Review>(error: ErrorsMessages.NULL_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Calculate new Schedule Date, Update Currrent Review and Schedule Next Review
        Review nextReview;
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
            try {
                var getCurrentReviewContent = await repository.GetContentIdAndReviewNumberByReviewIdAsync(currentReviewId)
                                                    ?? throw new TransactionAbortedException(ErrorsMessages.NULL_ID_ERROR);

                if (AllContentReviewsCompleted(getCurrentReviewContent.ReviewNumber))
                    return new Result<Review>(status: HttpStatusCode.OK, SuccessMessages.ALL_REVIEWS_FINISHED);

                if (ReviewNumberGreaterThanMaximumNumberReviews(getCurrentReviewContent.ReviewNumber))
                    return new Result<Review>(error: ErrorsMessages.MAXIMUM_NUMBER_REVIEWS_REACHED, status: HttpStatusCode.BadRequest);

                var updateSucceed = await repository.UpdateCurrentReviewAsync(currentReviewId, getCurrentReviewContent.ContentId);
                if (!updateSucceed)
                    throw new TransactionAbortedException(ErrorsMessages.FAILED_TO_UPDATE_CURRENT_REVIEW_ERROR);

                var nextScheduleDate = CalculateScheduleReviewDate(getCurrentReviewContent.ReviewNumber);

                nextReview = new Review(getCurrentReviewContent.ContentId, nextScheduleDate);
                var insertSucceed = await repository.InsertNextReviewAsync(nextReview);
                if (!insertSucceed)
                    throw new TransactionAbortedException(ErrorsMessages.FAILED_TO_CREATE_NEXT_REVIEW_ERROR);

                scope.Complete();
            } catch (Exception ex) {
                return new Result<Review>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        return new Result<Review>(data: nextReview, status: HttpStatusCode.OK);
    }

    private DateTime CalculateScheduleReviewDate(short currentReviewNumber) {
        var scheduleDate = DateTime.UtcNow;
        switch (currentReviewNumber) {
            case 1:
                scheduleDate.AddDays(TimeForNextReview.SEVEN_DAYS);
                break;
            case 2:
                scheduleDate.AddDays(TimeForNextReview.THIRTY_DAYS);
                break;
            default:
                throw new ArgumentOutOfRangeException(ErrorsMessages.MAXIMUM_NUMBER_REVIEWS_REACHED);
        }

        return scheduleDate;
    }

    private static bool AllContentReviewsCompleted(short reviewNumber) => reviewNumber == maximumNumberReviewsReached;
    private static bool ReviewNumberGreaterThanMaximumNumberReviews(short reviewNumber) => reviewNumber > maximumNumberReviewsReached;
}