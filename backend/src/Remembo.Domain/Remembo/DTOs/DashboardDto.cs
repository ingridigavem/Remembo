namespace Remembo.Domain.Remembo.DTOs;
public record DashboardDto(string Statistics, IEnumerable<MatterDetailsDto> MatterDetailsList);
