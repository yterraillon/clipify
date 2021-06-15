using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clipify.Application.Common.Exceptions
{
    public class ValidatorException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidatorException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(g => g.Key, g => g.ToArray());
        }
    }
}