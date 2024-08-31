namespace Remembo.Domain.Remembo.DTOs;
public class DashboardDto {
    public StatisticsDto? Statistics { get; private set; }
    public IList<MatterDetailsDto>? MatterDetailsList { get; private set; }

    public void SetStatistics(StatisticsDto statistics) {
        Statistics = statistics;
    }

    public void SetMatterDetailsList(IList<MatterDetailsDto> matterDetailsList) {
        MatterDetailsList = matterDetailsList;
    }
}
