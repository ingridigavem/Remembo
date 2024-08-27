using Remembo.Domain.Remembo.DTOs;
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
    public async Task<Result<NextReviewDto>> ScheduleNextReviewAsync(Guid currentReviewId) {
        if (currentReviewId == Guid.Empty) return new Result<NextReviewDto>(error: ErrorsMessages.NULL_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Calculate new Schedule Date, Update Currrent Review and Schedule Next Review

        DateTime nextScheduleDate;

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
            try {
                var getCurrentReviewContent = await repository.GetContentIdAndReviewNumberByReviewIdAsync(currentReviewId) ?? throw new TransactionAbortedException(ErrorsMessages.NULL_ID_ERROR);

                if (getCurrentReviewContent.ReviewNumber >= maximumNumberReviewsReached)
                    return new Result<NextReviewDto>(error: ErrorsMessages.MAXIMUM_NUMBER_REVIEWS_REACHED, status: HttpStatusCode.BadRequest);

                var updateSucceed = await repository.UpdateCurrentReviewAsync(currentReviewId, getCurrentReviewContent.ContentId);
                if (!updateSucceed)
                    throw new TransactionAbortedException(ErrorsMessages.FAILED_TO_UPDATE_CURRENT_REVIEW_ERROR);

                nextScheduleDate = CalculateScheduleReviewDate(getCurrentReviewContent.ReviewNumber);

                var nextReview = new Review(getCurrentReviewContent.ContentId, nextScheduleDate);
                var insertSucceed = await repository.InsertNextReviewAsync(nextReview);
                if (!insertSucceed)
                    throw new TransactionAbortedException(ErrorsMessages.FAILED_TO_CREATE_NEXT_REVIEW_ERROR);

                scope.Complete();
            } catch (Exception ex) {
                return new Result<NextReviewDto>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        var result = new NextReviewDto($"{SuccessMessages.NEXT_REVIEW_DATE}{nextScheduleDate:dd/MM/yyyy}");
        return new Result<NextReviewDto>(data: result, status: HttpStatusCode.OK);
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

}