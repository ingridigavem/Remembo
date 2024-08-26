using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Remembo.Entities;
public class Matter : Entity {
    protected Matter(Guid id, string name, Guid userId) : base(id) {
        Name = name;
        UserId = userId;
    }

    public Matter(string name, Guid userId) {
        Name = name;
        UserId = userId;
    }

    public string Name { get; set; } = null!;
    public Guid UserId { get; set; }
}
