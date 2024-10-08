﻿using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.Constants;
using Remembo.Domain.Shared.DTOs;
using Remembo.Service.Remembo.Validators;
using System.Net;

namespace Remembo.Service.Remembo;
public class ContentService(IContentRepository repository) : IContentService {
    public async Task<Result<DetailedContentDto?>> CreateContentAndFirstReviewAsync(ContentDto request) {
        #region Validate
        if (request is null) return new Result<DetailedContentDto?>(error: ErrorsMessages.NULL_REQUEST_ERROR, status: HttpStatusCode.BadRequest);

        var validator = new ContentValidator();
        var validations = validator.Validate(request);

        if (!validations.IsValid) {
            var errorsList = new List<string>();
            validations.Errors?.ForEach(error => errorsList.Add(error.ErrorMessage));
            return new Result<DetailedContentDto?>(errors: errorsList, status: HttpStatusCode.BadRequest);
        }
        #endregion

        #region Generate Content and First Review
        var content = new Content(request.Name, request.SubjectId, request.Note);

        var firstReviewDate = DateTime.UtcNow.AddHours(TimeForNextReview.TWENTY_FOUR_HOURS);
        var firstReview = new Review(content.Id, firstReviewDate);
        #endregion

        DetailedContentDto? contentDetailsDto;
        #region Save Data
        try {
            var success = await repository.InsertContentAndFirstReviewAsync(content, firstReview);
            if (!success) return new Result<DetailedContentDto?>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, status: HttpStatusCode.InternalServerError);

            contentDetailsDto = await repository.GetContentDetailsAsync(content.Id, firstReview.Id);
            if (contentDetailsDto is null) return new Result<DetailedContentDto?>(data: null, status: HttpStatusCode.Created);
        } catch (Exception ex) {
            return new Result<DetailedContentDto?>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<DetailedContentDto?>(data: contentDetailsDto, status: HttpStatusCode.Created);
    }

    public async Task<Result<IList<Content>>> GetAllContentsBySubjectIdAsync(Guid subjectId) {
        if (subjectId == Guid.Empty) return new Result<IList<Content>>(error: ErrorsMessages.NULL_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        IList<Content> contents;
        try {
            contents = await repository.GetAllBySubjectIdAsync(subjectId);
        } catch (Exception ex) {
            return new Result<IList<Content>>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<IList<Content>>(data: contents, status: HttpStatusCode.OK);
    }

    public async Task<Result<Content>> GetContentByIdAsync(Guid contentId) {
        if (contentId == Guid.Empty) return new Result<Content>(error: ErrorsMessages.NULL_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        Content content;
        try {
            content = await repository.SelectByIdAsync(contentId);
        } catch (Exception ex) {
            return new Result<Content>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<Content>(data: content, status: HttpStatusCode.OK);
    }
}
