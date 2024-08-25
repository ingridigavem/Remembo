using Remembo.Domain.Account.Entities;

namespace Remembo.Domain.Account.Interfaces;
public interface IAccountRepository {
    Task<bool> InsertAsync(User entity);
    Task<User?> GetUserByEmailAsync(string email);
    Task<int> CheckIfExistsUserByEmailAsync(string email);
}
