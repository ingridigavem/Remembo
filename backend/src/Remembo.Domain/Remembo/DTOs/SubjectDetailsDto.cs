namespace Remembo.Domain.Remembo.DTOs;
public record SubjectDetailsDto {
    public SubjectDetailsDto(Guid subjectId, string subjectName, IList<ContentDetailDto> contents) {
        SubjectId = subjectId;
        SubjectName = subjectName;
        Contents = contents;
    }

    protected SubjectDetailsDto(Guid subjectId, string subjectName) {
        SubjectId = subjectId;
        SubjectName = subjectName;
    }

    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; }
    public IList<ContentDetailDto>? Contents { get; set; }
}