using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Remembo.Entities;
public class Subject : Entity {
    protected Subject(Guid id, string name, Guid userId) : base(id) {
        Name = name;
        UserId = userId;
    }

    public Subject(string name, Guid userId) {
        Name = name;
        UserId = userId;
    }

    public string Name { get; set; } = null!;
    public Guid UserId { get; set; }
}
