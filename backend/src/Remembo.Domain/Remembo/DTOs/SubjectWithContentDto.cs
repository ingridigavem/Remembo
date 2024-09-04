namespace Remembo.Domain.Remembo.DTOs;
public record SubjectWithContentDto {
    public SubjectWithContentDto(Guid subjectId, string subjectName, ContentDetailDto contentReview) {
        SubjectId = subjectId;
        SubjectName = subjectName;
        ContentReview = contentReview;
    }
    public SubjectWithContentDto(Guid subjectId, string subjectName) {
        SubjectId = subjectId;
        SubjectName = subjectName;
    }

    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; }
    public ContentDetailDto? ContentReview { get; set; }
}
