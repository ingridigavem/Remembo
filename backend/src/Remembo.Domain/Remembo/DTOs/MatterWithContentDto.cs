namespace Remembo.Domain.Remembo.DTOs;
public record MatterWithContentDto {
    public MatterWithContentDto(Guid matterId, string matterName, ContentDetailDto contentReview) {
        MatterId = matterId;
        MatterName = matterName;
        ContentReview = contentReview;
    }
    public MatterWithContentDto(Guid matterId, string matterName) {
        MatterId = matterId;
        MatterName = matterName;
    }

    public Guid MatterId { get; set; }
    public string MatterName { get; set; }
    public ContentDetailDto ContentReview { get; set; }
}
