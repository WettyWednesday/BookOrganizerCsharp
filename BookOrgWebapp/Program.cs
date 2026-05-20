using BookOrgWebapp.Components;
using BookOrgWebapp.Data;
using BookOrgWebapp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Adding the SQL server connection
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient<GoogleBooksService>();

// Authentication + cookies + add google user
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.ClaimActions.MapJsonKey("picture", "picture");

    options.Events.OnCreatingTicket = async context =>
    {
        var dbFactory = context.HttpContext.RequestServices
                               .GetRequiredService<IDbContextFactory<AppDbContext>>();

        await using var db = await dbFactory.CreateDbContextAsync();

        var googleId = context.Principal!.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var email    = context.Principal.FindFirst(ClaimTypes.Email)?.Value!;
        var name     = context.Principal.FindFirst(ClaimTypes.Name)?.Value ?? email;
        var avatar   = context.Principal.FindFirst("picture")?.Value;

        var user = await db.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId)
                ?? await db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            user = new User
            {
                Name      = name,
                Email     = email,
                GoogleId  = googleId,
                AvatarUrl = avatar,
            };
            db.Users.Add(user);
        }
        else
        {
            user.GoogleId  = googleId;
            user.AvatarUrl = avatar;
            user.Name      = name;
        }

        await db.SaveChangesAsync();

        var identity = (ClaimsIdentity)context.Principal.Identity!;
        identity.AddClaim(new Claim("db_user_id", user.UserID.ToString()));
    };
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Auth middleware must come before UseAntiforgery
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// Login / Logout endpoints
app.MapGet("/login", (string? returnUrl) =>
    Results.Challenge(
        new AuthenticationProperties { RedirectUri = returnUrl ?? "/" },
        [GoogleDefaults.AuthenticationScheme]
    ));

app.MapGet("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Brugt til Debugging
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); 
}

app.Run();
