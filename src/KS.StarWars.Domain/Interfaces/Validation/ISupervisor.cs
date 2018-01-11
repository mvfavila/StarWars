using KS.StarWars.Domain.ValueObjects;

namespace KS.StarWars.Domain.Interfaces.Validation
{
    public interface ISupervisor<in TEntity>
    {
        ValidationResult Validate(TEntity entity);
    }
}
