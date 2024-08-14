using DeveloperExam.Domain.Premitives;

namespace DeveloperExam.Domain.Entities;

public class RunningActivity : Entity<Guid>
{
    public RunningActivity(Guid userProfileId, string location, DateTime start, DateTime end, double distance)
    {
        UserProfileId = userProfileId;
        Location = location;
        Start = start;
        End = end;
        Distance = distance;
    }

    public Guid UserProfileId { get; set; }
    public string Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Distance { get; set; }
    public double Duration 
    {  
        get
        {
           return (End - Start).TotalMinutes;
        }
    }
    public double AveragePace 
    { 
        get
        {
            return Duration / Distance;
        }
    }
}