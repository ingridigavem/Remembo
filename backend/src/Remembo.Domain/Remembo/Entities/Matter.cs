using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Remembo.Entities;
public class Matter(string name, Guid userId) : Entity {
    public string Name { get; set; } = name;
    public Guid UserId { get; set; } = userId;
}
