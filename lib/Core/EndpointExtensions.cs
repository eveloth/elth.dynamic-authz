using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using static LanguageExt.Prelude;

namespace Core;

public static class EndpointExtensions
{
#if NET8_0
    public static Option<string> GetRoute(this IHttpContextAccessor hca) =>
        hca.HttpContext?.GetEndpoint()?.Metadata.GetMetadata<IRouteDiagnosticsMetadata>()?.Route;

    public static Option<string> GetRoute(this HttpContext ctx) =>
        ctx.GetEndpoint()?.Metadata.GetRequiredMetadata<IRouteDiagnosticsMetadata>()?.Route;

    public static Option<string> GetRoute(this Endpoint endpoint) =>
        endpoint.Metadata.GetMetadata<IRouteDiagnosticsMetadata>()?.Route;
#endif

#if NET6_0 || NET7_0
    public static Option<string> GetRoute(this IHttpContextAccessor hca) =>
        (hca.HttpContext?.GetEndpoint() as RouteEndpoint)?.RoutePattern.RawText;

    public static Option<string> GetRoute(this HttpContext ctx) =>
        (ctx.GetEndpoint() as RouteEndpoint)?.RoutePattern.RawText;

    public static Option<string> GetRoute(this Endpoint endpoint) =>
        (endpoint as RouteEndpoint)?.RoutePattern.RawText;
#endif

    public static Option<IReadOnlyList<string>> GetMethods(this IHttpContextAccessor hca) =>
        Optional(
            hca.HttpContext?.GetEndpoint()?.Metadata.GetMetadata<IHttpMethodMetadata>()?.HttpMethods
        );

    public static Option<IReadOnlyList<string>> GetMethods(this HttpContext ctx) =>
        Optional(ctx.GetEndpoint()?.Metadata.GetMetadata<IHttpMethodMetadata>()?.HttpMethods);

    public static Option<IReadOnlyList<string>> GetMethods(this Endpoint endpoint) =>
        Optional(endpoint.Metadata.GetMetadata<IHttpMethodMetadata>()?.HttpMethods);
}