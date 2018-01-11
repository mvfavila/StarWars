using KS.StarWars.Domain.Interfaces.Validation;
using KS.StarWars.Domain.ValueObjects;
using System.Collections.Generic;

namespace KS.StarWars.Domain.Validation.Base
{
    public abstract class BaseSupervisor<TEntity> : ISupervisor<TEntity> where TEntity : class
    {
        private readonly Dictionary<string, IRule<TEntity>> validations = new Dictionary<string, IRule<TEntity>>();

        public ValidationResult Validate(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var x in validations.Keys)
            {
                var rule = validations[x];
                if (!rule.Validate(entity))
                    result.AddError(new ValidationError(rule.ErrorMessage));
            }

            return result;
        }
    }
}
