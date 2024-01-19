using Core.Models;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;

namespace Core;

public class EndpointWatcher : BackgroundService
{
    private readonly ILogger<EndpointWatcher> _logger;
    private readonly IServiceProvider _sp;

    public EndpointWatcher(IServiceProvider sp, ILogger<EndpointWatcher> logger)
    {
        _sp = sp;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _sp.CreateScope();

        var eds = scope.ServiceProvider.GetServices<EndpointDataSource>();
        var endpoints = eds.SelectMany(x => x.Endpoints);




    }

    private IEnumerable<ProtectedEndpoint> GetProtectedEndpoints(IEnumerable<Endpoint> endpoints)
    {
        #if NET8_0
        var route = ()
        #endif
    }

    private Option<ProtectedEndpoint> GetProtectedEndpoint(Endpoint endpoint)
    {
        var isMarkedForDynamicAuthz =
            endpoint.Metadata.GetMetadata<IAuthorizeData>()?.Policy is DynamicAuthorizationConstants.DynamicPolicy;

        if (!isMarkedForDynamicAuthz)
        {
            return None;
        }

        var route = endpoint.GetRoute();
        var method =
    }
}