using System.ComponentModel.DataAnnotations;

namespace DeveloperExam.Domain.Premitives;

public abstract class Entity<TIdentifier>
{
    public TIdentifier Id { get; protected set; }
    public DateTime DateCreated { get; protected set; }
    public DateTime LastModified { get; protected set; }

    protected Entity(TIdentifier id) => Id = id;

    protected Entity()
    {
        DateCreated = DateTime.UtcNow;
        LastModified = DateTime.UtcNow;
    }
}