//namespace BlogEngine.Core.Authorization
//{
//    using Microsoft.AspNetCore.Authentication.Cookies;
//    using Microsoft.AspNetCore.Authentication.JwtBearer;
//    using Microsoft.AspNetCore.Http;
//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.Extensions.Configuration;
//    using Microsoft.Extensions.DependencyInjection;
//    using Microsoft.Extensions.DependencyInjection.Extensions;
//    using Microsoft.IdentityModel.Tokens;
//    using System;
//    using System.Text;

//    public static class IdentityBuilderExtension
//    {
//        public static IdentityBuilder AddIdentityWithCookieAndJwt<TUser, TRole>(
//            this IServiceCollection services,
//            IConfiguration config)
//            where TUser : class
//            where TRole : class
//            => services.AddIdentityWithCookieAndJwt<TUser, TRole>(setupAction: null, config: config);

//        public static IdentityBuilder AddIdentityWithCookieAndJwt<TUser, TRole>(
//            this IServiceCollection services,
//            Action<IdentityOptions> setupAction,
//            IConfiguration config)
//            where TUser : class
//            where TRole : class
//        {
//            // Services used by identity
//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
//                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
//                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//            })
//                .AddCookie(IdentityConstants.ApplicationScheme, o =>
//                {
//                    o.LoginPath = new PathString("/Account/Login");
//                    o.Events = new CookieAuthenticationEvents
//                    {
//                        OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
//                    };
//                })
//                .AddCookie(IdentityConstants.ExternalScheme, o =>
//                {
//                    o.Cookie.Name = IdentityConstants.ExternalScheme;
//                    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//                })
//                .AddCookie(IdentityConstants.TwoFactorRememberMeScheme, o =>
//                {
//                    o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme;
//                    o.Events = new CookieAuthenticationEvents
//                    {
//                        OnValidatePrincipal = SecurityStampValidator.ValidateAsync<ITwoFactorSecurityStampValidator>
//                    };
//                })
//                .AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
//                {
//                    o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
//                    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//                })
//                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateAudience = false,
//                        ValidateIssuer = false,
//                        ValidateActor = false,
//                        ValidateLifetime = true,
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetValue<string>("AppSettings:SecretKey")))
//                    };
//                });

//            // Hosting doesn't add IHttpContextAccessor by default
//            services.AddHttpContextAccessor();
//            // Identity services
//            services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
//            services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
//            services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
//            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
//            services.TryAddScoped<IRoleValidator<TRole>, RoleValidator<TRole>>();
//            // No interface for the error describer so we can add errors without rev'ing the interface
//            services.TryAddScoped<IdentityErrorDescriber>();
//            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
//            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<TUser>>();
//            services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser, TRole>>();
//            services.TryAddScoped<UserManager<TUser>>();
//            services.TryAddScoped<SignInManager<TUser>>();
//            services.TryAddScoped<RoleManager<TRole>>();

//            if (setupAction != null)
//            {
//                services.Configure(setupAction);
//            }

//            return new IdentityBuilder(typeof(TUser), typeof(TRole), services);
//        }
//    }
//}
