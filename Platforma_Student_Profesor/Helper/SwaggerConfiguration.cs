using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Helper
{
    public class SwaggerConfiguration
    {
        public static void ConfigureSwaggerOptions(SwaggerGenOptions options)
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "Please enter token",
                In = ParameterLocation.Header,
                Name = "JWT Authentication",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.Http,

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            var securityRequirment = new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            };

            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(securityRequirment);
        }
    }
}
