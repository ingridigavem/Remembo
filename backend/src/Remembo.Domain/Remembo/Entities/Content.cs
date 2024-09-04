using Remembo.Domain.Shared.Entities;

namespace Remembo.Domain.Remembo.Entities;
public class Content : Entity {
    public short ReviewNumber { get; set; } = 1;
    public Guid SubjectId { get; set; }
    public string Name { get; set; } = null!;
    public string? Note { get; set; }


    protected Content(Guid id, Guid subjectId, string name, string? note, short reviewNumber) : base(id) {
        Name = name;
        SubjectId = subjectId;
        ReviewNumber = reviewNumber;
        Note = note;
    }

    public Content(string name, Guid subjectId, string? note) {
        Name = name;
        SubjectId = subjectId;
        Note = note;
    }
}
