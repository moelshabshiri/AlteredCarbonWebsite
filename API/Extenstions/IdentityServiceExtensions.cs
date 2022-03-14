using System.Linq;
using System.Text;
using API.Errors;
using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builderF = services.AddIdentityCore<FarmerUser>();

            builderF = new IdentityBuilder(builderF.UserType, builderF.Services);
            builderF.AddEntityFrameworkStores<AppIdentityDbContext>();
            builderF.AddSignInManager<SignInManager<FarmerUser>>();


            var builderC = services.AddIdentityCore<CooperativeUser>();

            builderC = new IdentityBuilder(builderC.UserType, builderC.Services);
            builderC.AddEntityFrameworkStores<AppIdentityDbContext>();
            builderC.AddSignInManager<SignInManager<CooperativeUser>>();

            // services.AddAuthentication();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            var httpContext = context.HttpContext;

                            var routeData = httpContext.GetRouteData();
                            var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());


                            var result = new ObjectResult(new ApiResponse(401, "Unauthorized"));
                            await result.ExecuteResultAsync(actionContext);
                           
                        }
                    };
                });

            return services;
        }
    }
}