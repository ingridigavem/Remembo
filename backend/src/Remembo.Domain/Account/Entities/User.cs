using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Account.Entities;

public class User : Entity {
    protected User(Guid id, string name, string email, string passwordHash) : base(id) {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public User(string name, string email, string passwordHash) {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }


}