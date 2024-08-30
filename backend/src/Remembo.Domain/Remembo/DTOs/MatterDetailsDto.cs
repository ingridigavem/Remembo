namespace Remembo.Domain.Remembo.DTOs;
public record MatterDetailsDto(IEnumerable<ContentMatterList> Matters);

public record ContentMatterList(Guid Id, string Name, IEnumerable<ContentDetailDto> Contents);
