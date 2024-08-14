namespace DeveloperExam.Application.Dto;

public sealed class RunningActivityResponse
{
    public Guid Id { get; set; }
    public Guid UserProfileId { get; set; }
    public string Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Distance { get; set; }
    public string? Duration { get; set; }
    public string? AveragePace { get; set; }
}
