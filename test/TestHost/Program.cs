using System.Security.Claims;
using System.Xml.Schema;
using Core.Authorization;
using Core.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TestHost;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("policy", pb =>
    {
        pb.Requirements
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/asdfas", ([AsParameters] ProtectedEndpoint endpoint) =>
{

});

app.UseRouting();

app.MapGet(
    "/endponts",
    (HttpContext ctx, IEnumerable<EndpointDataSource> eds) =>
    {
        eds.SelectMany(x => x.Endpoints)
            .Iter(x =>
            {
                x.Metadata.Iter(o => Console.WriteLine(o.GetType().Name));
                Console.WriteLine((x as RouteEndpoint)?.RoutePattern.RawText ?? "NULL");
            });
    }
).RequireAuthorization();

app.Run();