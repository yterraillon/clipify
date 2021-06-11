﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Clipify.Application.Common.Behaviours
{
    public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationFailures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(x => x != null)
                .ToList();

            if (validationFailures.Any())
                throw new ValidatorException(validationFailures);

            return await next();
        }
    }
}