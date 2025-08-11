using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Common;
using HealLink.Infrastructure.Common.Authentication.PasswordHasher;
using HealLink.Infrastructure.Common.Authentication.TokenGenerator;
using HealLink.Infrastructure.Persistence;
using HealLink.Infrastructure.Persistence.Repositories.DoctorRequests;
using HealLink.Infrastructure.Persistence.Repositories.Doctors;
using HealLink.Infrastructure.Persistence.Repositories.MedicalHistories;
using HealLink.Infrastructure.Persistence.Repositories.PatientDoctorSubscriptions;
using HealLink.Infrastructure.Persistence.Repositories.Patients;
using HealLink.Infrastructure.Persistence.Repositories.Payments;
using HealLink.Infrastructure.Persistence.Repositories.Users;
using HealLink.Infrastructure.Services.Email;
using HealLink.Infrastructure.Services.Payment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;


namespace HealLink.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthentication(configuration)
            .AddPersistence(configuration)
            .AddServices(configuration);
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<HealLinkDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserTokensRepository, UserTokensRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPatientGuardianRepository, PatientGuardianRepository>();
        services.AddScoped<IDoctorRequestRepository, DoctorRequestRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPaymentRepository,PaymentRepository>();
        services.AddScoped<ISubscriptionRepository,SubscriptionRepository>();
        services.AddScoped<IRefundRepository, RefundRepository>();
        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
        //StripeConfiguration.ApiKey = stripeSettings.SecretKey;

        services.AddScoped<IPaymentService,StripePaymentService>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(JwtSettings.Section);
        services.Configure<JwtSettings>(jwtSection);
        //services.Configure<JwtSettings>(configuration.GetSection("JwtSection"));

        var jwtSettings = jwtSection.Get<JwtSettings>();

        services.AddSingleton<ITokenGenerator, TokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.key)),
            });

        return services;
    }

}