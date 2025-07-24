using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Common;
using HealLink.Infrastructure.Common.Authentication.PasswordHasher;
using HealLink.Infrastructure.Common.Authentication.TokenGenerator;
using HealLink.Infrastructure.Email;
using HealLink.Infrastructure.Persistence;
using HealLink.Infrastructure.Persistence.Repositories.Users;
using HealLink.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace HealLink.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthentication(configuration)
            .AddPersistence(configuration);
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<HealLinkDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.AddSingleton<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUserTokensRepository, UserTokensRepository>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        //services.AddScoped<IAdminsRepository, AdminsRepository>();
        //services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<HealLinkDbContext>());

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(JwtSettings.Section);
        services.Configure<JwtSettings>(jwtSection);

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