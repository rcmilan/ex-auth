using Database;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllersWithViews()
    //.Services.AddAuthentication("CookieAuth")
    //    .AddCookie("CookieAuth", config =>
    //    {
    //        config.Cookie.Name = "Chocolate.Cookie";
    //        config.LoginPath = "/Home/Authenticate";
    //    })
    .Services.AddDatabaseModule()
    .ConfigureApplicationCookie(config =>
    {
        config.Cookie.Name = "Chocolate.Cookie";
        config.LoginPath = "/Home/Login";
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
