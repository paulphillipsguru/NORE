using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Identity.Web;
using PP.NORE.Builder;
using PP.NORE.Builder.CodeTemplates;
using PP.NORE.Builder.Compilers;
using PP.NORE.Builder.Contracts;
using PP.NORE.Portal.Blazor.Server.Services;
using PP.NORE.Shared.Contracts;
using PP.NORE.RatingEngine;
namespace PP.NORE.Portal.Blazor.Server;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();

	    services.AddScoped<ITemplateManager, TemplateManager>();
		services.AddScoped<ICodeBuilder, CodeBuilder>();
		services.AddScoped<ICodeCompiler, DefaultCompiler>();
		services.AddScoped<IProductRater, RatingProcessor>();
        
		services.AddXaf(Configuration, builder => {
            builder.UseApplication<PortalBlazorApplication>();
            builder.Modules
                .AddAuditTrailXpo()
                .AddCloningXpo()
                .AddConditionalAppearance()
                .AddFileAttachments()
                .AddNotifications()
                .AddOffice()
                .AddStateMachine(options => {
                    options.StateMachineStorageType = typeof(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine);
                })
                .AddValidation(options => {
                    options.AllowValidationDetailsAccess = false;
                })
                .AddViewVariants()
                .Add<PP.NORE.Portal.Module.PortalModule>()
            	.Add<PortalBlazorModule>();

            builder.AddMultiTenancy()
                .WithHostDatabaseConnectionString(Configuration.GetConnectionString("ConnectionString"))
#if EASYTEST
                .WithHostDatabaseConnectionString(Configuration.GetConnectionString("EasyTestConnectionString"))   
#endif
                .WithMultiTenancyModelDifferenceStore(options => {
#if !RELEASE
                    options.UseTenantSpecificModel = false;
#endif
                })
                .WithTenantResolver<TenantByEmailResolver>();

            builder.ObjectSpaceProviders
                .AddSecuredXpo((serviceProvider, options) => {
                    string connectionString = serviceProvider.GetRequiredService<IConnectionStringProvider>().GetConnectionString();
                    options.ConnectionString = connectionString;
                    options.ThreadSafe = true;
                    options.UseSharedDataStoreProvider = true;
                })
                .AddNonPersistent();
            builder.Security
                .UseIntegratedMode(options => {
                    options.Lockout.Enabled = true;

                    options.RoleType = typeof(PermissionPolicyRole);
                    // ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
                    // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
                    options.UserType = typeof(PP.NORE.Portal.Module.BusinessObjects.ApplicationUser);
                    // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
                    // If you use PermissionPolicyUser or a custom user type, comment out the following line:
                    options.UserLoginInfoType = typeof(PP.NORE.Portal.Module.BusinessObjects.ApplicationUserLoginInfo);
                    options.UseXpoPermissionsCaching();
                    options.Events.OnSecurityStrategyCreated += securityStrategy => {
                        // Use the 'PermissionsReloadMode.NoCache' option to load the most recent permissions from the database once
                        // for every Session instance when secured data is accessed through this instance for the first time.
                        // Use the 'PermissionsReloadMode.CacheOnFirstAccess' option to reduce the number of database queries.
                        // In this case, permission requests are loaded and cached when secured data is accessed for the first time
                        // and used until the current user logs out. 
                        // See the following article for more details: https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.SecurityStrategy.PermissionsReloadMode.
                        ((SecurityStrategy)securityStrategy).PermissionsReloadMode = PermissionsReloadMode.NoCache;
                    };
                })
                .AddPasswordAuthentication(options => {
                    options.IsSupportChangePassword = true;
                })
                .AddWindowsAuthentication(options => {
                    options.CreateUserAutomatically();
                })
                .AddAuthenticationProvider<PP.NORE.Portal.Module.CustomAuthenticationProvider>();
        });
        var authentication = services.AddAuthentication(options => {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });
        authentication.AddCookie(options => {
            options.LoginPath = "/LoginPage";
        });
        //Configure OAuth2 Identity Providers based on your requirements. For more information, see
        //https://docs.devexpress.com/eXpressAppFramework/402197/task-based-help/security/how-to-use-active-directory-and-oauth2-authentication-providers-in-blazor-applications
        //https://developers.google.com/identity/protocols/oauth2
        //https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-auth-code-flow
        //https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow
        authentication.AddMicrosoftIdentityWebApp(options => {
            Configuration.Bind("Authentication:AzureAd", options);
        }, openIdConnectScheme: "AzureAD", cookieScheme: null);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseXaf();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
    }
}
