using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.Constants;
using Remembo.Domain.Shared.DTOs;
using Remembo.Service.Remembo.Validators;
using System.Net;

namespace Remembo.Service.Remembo;
public class SubjectService(ISubjectRepository repository) : ISubjectService {
    public async Task<Result<Subject>> CreateSubjectAsync(SubjectDto request, Guid userId) {
        #region Validate
        if (request is null) return new Result<Subject>(error: ErrorsMessages.NULL_REQUEST_ERROR, status: HttpStatusCode.BadRequest);

        var validator = new SubjectValidator();
        var validations = validator.Validate(request);

        if (!validations.IsValid) {
            var errorsList = new List<string>();
            validations.Errors?.ForEach(error => errorsList.Add(error.ErrorMessage));
            return new Result<Subject>(errors: errorsList, status: HttpStatusCode.BadRequest);
        }
        #endregion

        #region Generate Subject
        var subject = new Subject(name: request.Name, userId: userId);
        #endregion

        #region Save Data
        try {
            var success = await repository.InsertAsync(subject);
            if (!success) return new Result<Subject>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, status: HttpStatusCode.InternalServerError);
        } catch (Exception ex) {
            return new Result<Subject>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<Subject>(data: subject, status: HttpStatusCode.Created);
    }

    public async Task<Result<IList<Subject>>> GetAllSubjectsByUserIdAsync(Guid userId) {
        if (userId == Guid.Empty) return new Result<IList<Subject>>(error: ErrorsMessages.NULL_USER_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        IList<Subject> subjects;
        try {
            subjects = await repository.GetAllByUserIdAsync(userId);
        } catch (Exception ex) {
            return new Result<IList<Subject>>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<IList<Subject>>(data: subjects, status: HttpStatusCode.OK);
    }

    public async Task<Result<Subject>> GetSubjectByIdAsync(Guid subjectId, Guid userId) {
        if (subjectId == Guid.Empty) return new Result<Subject>(error: ErrorsMessages.NULL_ID_ERROR, status: HttpStatusCode.BadRequest);
        if (userId == Guid.Empty) return new Result<Subject>(error: ErrorsMessages.NULL_USER_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        Subject subject;
        try {
            subject = await repository.SelectByIdAsync(subjectId, userId);
        } catch (Exception ex) {
            return new Result<Subject>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<Subject>(data: subject, status: HttpStatusCode.OK);
    }
}
