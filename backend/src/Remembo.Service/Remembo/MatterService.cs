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

    public Task<Result<IList<Matter>>> GetAllMattersByUserIdAsync(Guid userId) {
        throw new NotImplementedException();
    }

    public Task<Result<Matter>> GetMatterByIdAsync(Guid matterId) {
        throw new NotImplementedException();
    }
}
