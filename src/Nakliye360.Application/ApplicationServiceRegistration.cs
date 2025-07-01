using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nakliye360.Application.Mapping.CustomerManagement;
using Nakliye360.Application.Models.DTOs.CustomerManagement;
using Nakliye360.Application.Validators.CustomerManagement;
using System.Reflection;

namespace Nakliye360.Application;

public static class ApplicationServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // FluentValidation
        //services.AddScoped<IValidator<CreateCustomerDto>, CreateCustomerDtoValidator>();
        //services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();
        // Mapster
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(typeof(CustomerMappingConfig).Assembly);

        services.AddSingleton(typeAdapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();
        // Application Services
    }

    // FluentValidation

    public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
    {
        var applicationAssembly = Assembly.Load("Nakliye360.Application"); // Derleme adını kullanarak yükleyin

        //// .Net 8 
        //services.AddFluentValidation(fv =>
        //{
        //    fv.RegisterValidatorsFromAssembly(applicationAssembly);
        //    //fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();
        //});

        //services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();
        //services.AddScoped<IValidator<CreateCustomerDto>, CreateCustomerDtoValidator>();
        services.AddFluentValidationAutoValidation(); // Otomatik çalıştırma
        services.AddFluentValidationClientsideAdapters(); // İstersen


        //services.Configure<ApiBehaviorOptions>(options =>
        //{
        //    options.InvalidModelStateResponseFactory = context =>
        //    {
        //        var errors = context.ModelState
        //            .Where(e => e.Value?.Errors.Any() == true)
        //            .ToDictionary(
        //                kvp => kvp.Key,
        //                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
        //            );

        //        return new BadRequestObjectResult(new
        //        {
        //            message = "Doğrulama hatası oluştu.",
        //            errors
        //        });
        //    };

        //});
        return services;
    }
}
