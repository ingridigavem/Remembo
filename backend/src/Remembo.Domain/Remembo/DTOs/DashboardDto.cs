namespace Remembo.Domain.Remembo.DTOs;
public class DashboardDto {
    public StatisticsDto? Statistics { get; private set; }
    public MatterDetailsDto? MatterDetailsList { get; private set; }

    public void SetStatistics(StatisticsDto statistics) {
        Statistics = statistics;
    }

    public void SetMatterDetailsList(MatterDetailsDto matterDetailsList) {
        MatterDetailsList = matterDetailsList;
    }
}
