using DeveloperExam.Domain.Premitives;

namespace DeveloperExam.Domain.Entities;

public class RunningActivity : Entity<Guid>
{
    public RunningActivity(Guid id, Guid userProfileId, string location, DateTime start, DateTime end, double distance)
    {
        Id = id;
        UserProfileId = userProfileId;
        Location = location;
        Start = start;
        End = end;
        Distance = distance;
    }

    public RunningActivity(Guid userProfileId, string location, DateTime start, DateTime end, double distance)
    {
        UserProfileId = userProfileId;
        Location = location;
        Start = start;
        End = end;
        Distance = distance;
    }

    public RunningActivity(string location, DateTime start, DateTime end, double distance)
    {
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
    public TimeSpan Duration 
    {  
        get
        {
           return (End - Start);
        }
    }
    public double AveragePace 
    { 
        get
        {
            if (Duration.TotalHours == 0 || Distance == 0)
                return 0;

            return Duration.TotalHours / Distance;
        }
    }
}