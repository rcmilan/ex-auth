using App.Authorization;
using Database;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    // Exemplo B�sico
    //.Services.AddAuthentication("CookieAuth")
    //    .AddCookie("CookieAuth", config =>
    //    {
    //        config.Cookie.Name = "Chocolate.Cookie";
    //        config.LoginPath = "/Home/Authenticate";
    //    })
    .AddDatabaseModule()
    .AddAuthorization(config =>
    {
        // Exemplo de policy 1
        //var defaultAuthPolicy = new AuthorizationPolicyBuilder()
        //    .RequireAuthenticatedUser()
        //    .RequireClaim("hue.claim")
        //    .Build();

        //config.DefaultPolicy = defaultAuthPolicy;

        // Exemplo de policy 2
        //config.AddPolicy("Claim.Hu3", policyBuilder =>
        //{
        //    policyBuilder.RequireClaim("hue.claim");
        //});

        config.AddPolicy("Claim.Hu3", policyBuilder =>
        {
            policyBuilder.AddRequirements(new CustomRequireClaim("hue.claim"));
        });
    })
    .ConfigureApplicationCookie(config =>
    {
        config.Cookie.Name = "Chocolate.Cookie";
        config.LoginPath = "/Home/Login";
    })
    .AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>()
    .AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>()
    .AddControllersWithViews(config =>
    {
        //var defaultAuthPolicy = new AuthorizationPolicyBuilder()
        //    .RequireAuthenticatedUser()
        //    .RequireClaim("hue.claim")
        //    .Build();

        // global para todas as requisi��es
        //config.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));

        // para evitar o filtro global, a chamada deve ter [AllowAnonymous]
    })
    ;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error")
        .UseHsts();
}

app.UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication() // quem � o client?
    .UseAuthorization() // client tem autoriza��o?
    .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    })
    ;

app.Run();