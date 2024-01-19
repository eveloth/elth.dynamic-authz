using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Authorization;

public class DynamicAuthorizationHandler : AuthorizationHandler<DynamicRequirement>
{
    private readonly ILogger<DynamicAuthorizationHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPolicyManagement _policyManagement;

    public DynamicAuthorizationHandler(
        ILogger<DynamicAuthorizationHandler> logger,
        IPolicyManagement policyManagement,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _logger = logger;
        _policyManagement = policyManagement;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DynamicRequirement requirement
    )
    {
        ClaimsPrincipal user = context.User;
        _logger.LogInformation("Executing dynamic authorization handler");

        var claims = context.User.Claims;
        context.Succeed(requirement);


    }



}