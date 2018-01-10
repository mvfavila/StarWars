using KS.StarWars.Domain.ValueObjects;

namespace KS.StarWars.Domain.Interfaces.Validation
{
    /// <summary>
    /// Contract for Self validated entities.
    /// </summary>
    public interface ISelfValidator
    {
        ValidationResult ValidationResult { get; }

        bool IsValid();
    }
}
