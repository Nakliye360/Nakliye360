using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Nakliye360.Application.Mapping.Companies;
using Nakliye360.Application.Mapping.DriverManagement;
using Nakliye360.Application.Mapping.OrderManagement;
using Nakliye360.Application.Mapping.VehicleManagement;
using Nakliye360.Application.Validators.CustomerManagement;
using Nakliye360.Application.Validators.DriverManagement;
using Nakliye360.Application.Validators.OrderManagement;
using Nakliye360.Application.Validators.VehicleManagement;
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
        typeAdapterConfig.Scan(typeof(CompanyMappingConfig).Assembly);
        typeAdapterConfig.Scan(typeof(OrderMappingConfig).Assembly);
        typeAdapterConfig.Scan(typeof(VehicleMappingConfig).Assembly);
        typeAdapterConfig.Scan(typeof(DriverMappingConfig).Assembly);

        services.AddSingleton(typeAdapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateOrderDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateOrderDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateVehicleDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateVehicleDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDriverDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateDriverDtoValidator>();
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
