using System.Collections.Generic;

namespace KS.StarWars.Domain.ValueObjects
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            errors = new List<ValidationError>();
        }

        private readonly List<ValidationError> errors;

        public string Message { get; set; }

        public bool IsValid { get { return errors.Count == 0; } }

        public IEnumerable<ValidationError> Errors { get { return this.errors; } }
    }
}
