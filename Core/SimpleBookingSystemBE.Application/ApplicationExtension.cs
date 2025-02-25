using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Validators;
using System.Reflection;

namespace SimpleBookingSystemBE.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateBookingCommandValidator>();
        }
    }
}
