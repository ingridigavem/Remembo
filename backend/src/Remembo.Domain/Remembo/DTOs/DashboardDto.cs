namespace Remembo.Domain.Remembo.DTOs;
public class DashboardDto {
    public StatisticsDto? Statistics { get; private set; }
    public IList<SubjectDetailsDto>? SubjectDetailsList { get; private set; }

    public void SetStatistics(StatisticsDto statistics) {
        Statistics = statistics;
    }

    public void SetSubjectDetailsList(IList<SubjectDetailsDto> subjectDetailsList) {
        SubjectDetailsList = subjectDetailsList;
    }
}
