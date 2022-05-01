var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllersWithViews()
    .Services.AddAuthentication("CookieAuth")
        .AddCookie("CookieAuth", config =>
        {
            config.Cookie.Name = "Chocolate.Cookie";
            config.LoginPath = "/Home/Authenticate";
        });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error")
        .UseHsts();
}

app.UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication() // quem é o client?
    .UseAuthorization() // client tem autorização?
    .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });

app.Run();
