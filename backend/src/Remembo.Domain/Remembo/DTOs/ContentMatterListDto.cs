namespace Remembo.Domain.Remembo.DTOs;
public record ContentMatterList {
    public ContentMatterList(Guid matterId, string matterName, IList<ContentDetailDto> contents) {
        MatterId = matterId;
        MatterName = matterName;
        Contents = contents;
    }

    protected ContentMatterList(Guid matterId, string matterName) {
        MatterId = matterId;
        MatterName = matterName;
    }

    public Guid MatterId { get; set; }
    public string MatterName { get; set; }
    public IList<ContentDetailDto>? Contents { get; set; }
}