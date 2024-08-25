using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.Constants;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;
using Remembo.Service.Remembo.Validators;
using System.Net;

namespace Remembo.Service.Remembo;
public class MatterService(IMatterRepository repository) : IMatterService {
    public async Task<Result<IdResponse>> CreateMatterAsync(MatterDto request, Guid userId) {
        #region Validate
        if (request is null) return new Result<IdResponse>(error: ErrorsMessages.NULL_REQUEST_ERROR, status: HttpStatusCode.BadRequest);

        var validator = new MatterValidator();
        var validations = validator.Validate(request);

        if (!validations.IsValid) {
            var errorsList = new List<string>();
            validations.Errors?.ForEach(error => errorsList.Add(error.ErrorMessage));
            return new Result<IdResponse>(errors: errorsList, status: HttpStatusCode.BadRequest);
        }
        #endregion

        #region Generate Matter
        var matter = new Matter(name: request.Name, userId: userId);
        #endregion

        #region Save Data
        try {
            var success = await repository.InsertAsync(matter);
            if (!success) return new Result<IdResponse>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, status: HttpStatusCode.InternalServerError);
        } catch (Exception ex) {
            return new Result<IdResponse>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<IdResponse>(data: new IdResponse(matter.Id), status: HttpStatusCode.Created);
    }

    public async Task<Result<IList<Matter>>> GetAllMattersByUserIdAsync(Guid userId) {
        if (userId == Guid.Empty) return new Result<IList<Matter>>(error: ErrorsMessages.NULL_USER_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        IList<Matter> matters;
        try {
            matters = await repository.GetAllByUserIdAsync(userId);
            if (!matters.Any()) return new Result<IList<Matter>>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, status: HttpStatusCode.InternalServerError);

        } catch (Exception ex) {
            return new Result<IList<Matter>>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<IList<Matter>>(data: matters, status: HttpStatusCode.OK);
    }

    public async Task<Result<Matter>> GetMatterByIdAsync(Guid matterId, Guid userId) {
        if (matterId == Guid.Empty) return new Result<Matter>(error: ErrorsMessages.NULL_ID_ERROR, status: HttpStatusCode.BadRequest);
        if (userId == Guid.Empty) return new Result<Matter>(error: ErrorsMessages.NULL_USER_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        Matter matter;
        try {
            matter = await repository.SelectByIdAsync(matterId, userId);
            if (matter is null) return new Result<Matter>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, status: HttpStatusCode.InternalServerError);

        } catch (Exception ex) {
            return new Result<Matter>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<Matter>(data: matter, status: HttpStatusCode.OK);
    }
}
