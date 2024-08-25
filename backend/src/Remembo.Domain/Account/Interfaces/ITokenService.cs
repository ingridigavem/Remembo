using Remembo.Domain.Account.Entities;

namespace Remembo.Domain.Account.Interfaces;
public interface ITokenService {
    string GenerateToken(User user);
}
