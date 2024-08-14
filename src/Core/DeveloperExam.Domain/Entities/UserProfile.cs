using DeveloperExam.Domain.Premitives;

namespace DeveloperExam.Domain.Entities;

public class UserProfile : Entity<Guid>
{
    public UserProfile(Guid id, string name, double weight, double height, DateTime birthDate)
    {
        Id = id;
        Name = name;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
    }

    public UserProfile(string name, double weight, double height, DateTime birthDate)
    {
        Name = name;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
    }

    public string Name { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age 
    { 
        get 
        {
            int age = DateTime.Now.Year - BirthDate.Year;

            if(DateTime.Now.DayOfYear < BirthDate.DayOfYear)
                age = age - 1;

            return age;
        } 
    }

    public double BodyMassIndex
    {
        get
        {
            double heightM = Height / 100;
            return Weight / Math.Pow(heightM, 2);
        }
    }
}