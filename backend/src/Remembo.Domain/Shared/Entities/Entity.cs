namespace Remembo.Domain.Shared.Entities;
public abstract class Entity : IEquatable<Guid> {
    protected Entity(Guid id) {
        Id = id;
    }
    protected Entity() {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }

    public bool Equals(Guid id)
           => Id == id;

    public override int GetHashCode()
        => Id.GetHashCode();
}
