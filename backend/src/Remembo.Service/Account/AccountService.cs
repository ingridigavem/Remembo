using Remembo.Domain.Account.DTOs;
using Remembo.Domain.Account.Entities;
using Remembo.Domain.Account.Interfaces;
using Remembo.Domain.Account.Utilities;
using Remembo.Domain.Shared.Constants;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;
using Remembo.Service.Account.Validators;
using System.Net;

namespace Remembo.Service.Account;
public class AccountService(IAccountRepository repository, ITokenService tokenService) : IAccountService {
    public async Task<Result<IdResponse>> CreateUserAsync(UserDto request) {
        #region Validate
        if (request is null) return new Result<IdResponse>(error: ErrorsMessages.NULL_REQUEST_ERROR, status: HttpStatusCode.BadRequest);

        var validator = new AccountValidator();
        var validations = validator.Validate(request);

        if (!validations.IsValid) {
            var errorsList = new List<string>();
            validations.Errors?.ForEach(error => errorsList.Add(error.ErrorMessage));
            return new Result<IdResponse>(errors: errorsList, status: HttpStatusCode.BadRequest);
        }

        try {
            if (EmailAlreadyRegistered(request.Email).Result) return new Result<IdResponse>(error: ErrorsMessages.EMAIL_ALREADY_REGISTERED_ERROR, status: HttpStatusCode.BadRequest);
        } catch (Exception ex) {
            return new Result<IdResponse>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        var user = new User(name: request.Name, email: request.Email, passwordHash: PasswordHasher.Hash(request.Password));

        #region Save Data

        try {
            var success = await repository.InsertAsync(user);
            if (!success) return new Result<IdResponse>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, status: HttpStatusCode.InternalServerError);
        } catch (Exception ex) {
            return new Result<IdResponse>(error: ErrorsMessages.FAILED_TO_PERSIST_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<IdResponse>(data: new IdResponse(user.Id), status: HttpStatusCode.Created);
    }

    public async Task<Result<string>> LoginAsync(LoginDto request) {
        #region Validate
        if (request is null) return new Result<string>(error: ErrorsMessages.NULL_REQUEST_ERROR, status: HttpStatusCode.BadRequest);

        var validator = new LoginValidator();
        var validations = validator.Validate(request);

        if (!validations.IsValid) {
            var errorsList = new List<string>();
            validations.Errors?.ForEach(error => errorsList.Add(error.ErrorMessage));
            return new Result<string>(errors: errorsList, status: HttpStatusCode.BadRequest);
        }
        #endregion

        #region Get User
        User? user;
        try {
            user = await repository.GetUserByEmailAsync(request.Email);
            if (user is null)
                return new Result<string>(error: ErrorsMessages.USER_PASSWORD_INVALID_ERROR, status: HttpStatusCode.BadRequest);
        } catch (Exception ex) {
            return new Result<string>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        #region Validate Password
        if (!PasswordHasher.Verify(request.Password, user.PasswordHash))
            return new Result<string>(error: ErrorsMessages.USER_PASSWORD_INVALID_ERROR, status: HttpStatusCode.BadRequest);
        #endregion


        #region Get Token
        string token;
        try {
            token = tokenService.GenerateToken(user);
        } catch (Exception ex) {
            return new Result<string>(error: "Authentication failed", exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<string>(data: token, status: HttpStatusCode.OK);
    }

    private async Task<bool> EmailAlreadyRegistered(string userEmail) {
        var result = await repository.CheckIfExistsUserByEmailAsync(userEmail);
        if (result > 0) return true;

        return false;
    }
}




