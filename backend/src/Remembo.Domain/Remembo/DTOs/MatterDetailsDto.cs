namespace Remembo.Domain.Remembo.DTOs;
public record MatterDetailsDto {
    public MatterDetailsDto(Guid matterId, string matterName, IList<ContentDetailDto> contents) {
        MatterId = matterId;
        MatterName = matterName;
        Contents = contents;
    }

    protected MatterDetailsDto(Guid matterId, string matterName) {
        MatterId = matterId;
        MatterName = matterName;
    }

    public Guid MatterId { get; set; }
    public string MatterName { get; set; }
    public IList<ContentDetailDto>? Contents { get; set; }
}