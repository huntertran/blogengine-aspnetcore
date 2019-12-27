namespace BlogEngine.Api
{
    using Core.Policy;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using Repository.Data;
    using Repository.Generic;
    using Services.Identity;
    using Services.Post;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(dbContextOptionBuilder =>
            {
                dbContextOptionBuilder.UseSqlite(Configuration.GetConnectionString("SqliteConnection"));
                //dbContextOptionBuilder.UseSqlServer(Configuration.GetConnectionString("AzureConnection"));
            });

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:SecretKey")))
                    };
                });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityWithCookieAndJwt<ApplicationUser, IdentityRole>(Configuration)
            //    .AddRoleManager<RoleManager<IdentityRole>>()
            //    .AddDefaultTokenProviders()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyName.RequireAdmin.ToString(), policy => policy.RequireRole(RoleName.Admin.ToString()));
                options.AddPolicy(PolicyName.RequireWriter.ToString(), policy => policy.RequireRole(RoleName.Writer.ToString()));
            });

            services.AddCors();

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "../blogengine-client/dist";
            });

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Post>, GenericRepository<Post>>();
            services.AddScoped<IGenericRepository<PostCategory>, GenericRepository<PostCategory>>();
            services.AddScoped<IPostService, PostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();
            //app.UseMvc();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../blogengine-client";
            });
        }
    }
}
