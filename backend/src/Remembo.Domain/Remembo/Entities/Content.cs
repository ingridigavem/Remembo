using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Remembo.Entities;
public class Content : Entity {
    public short ReviewNumber { get; set; } = 1;
    public Guid MatterId { get; set; }
    public string Name { get; set; } = null!;
    public string? Note { get; set; }


    protected Content(Guid id, Guid matterId, string name, string? note, short reviewNumber) : base(id) {
        Name = name;
        MatterId = matterId;
        ReviewNumber = reviewNumber;
        Note = note;
    }

    public Content(string name, Guid matterId, string? note) {
        Name = name;
        MatterId = matterId;
        Note = note;
    }
}
