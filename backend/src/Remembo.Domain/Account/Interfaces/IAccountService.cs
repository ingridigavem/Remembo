using Remembo.Domain.Account.DTOs;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;

namespace Remembo.Domain.Account.Interfaces;
public interface IAccountService {
    Task<Result<IdResponse>> CreateUserAsync(UserDto user);
    Task<Result<string>> LoginAsync(LoginDto login);
}
