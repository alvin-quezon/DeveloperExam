using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperExam.Application.Dto;

public sealed class UserProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public string? BirthDate { get; set; }
    public string? Age { get; set; }
    public string? BodyMassIndex { get; set; }
}
