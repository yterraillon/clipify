using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Clipify.Application.Common.Behaviours;
using FluentValidation;

namespace Clipify.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));

            return services;
        }
    }
}