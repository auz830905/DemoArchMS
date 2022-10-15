using Ocelot.Middleware;

namespace APIGateway.Middlewares
{
    public class OcelotJwtMiddleware
    {
        public static bool Authorize(HttpContext ctx)
        {
            if (ctx.Items.DownstreamRoute().AuthenticationOptions.AuthenticationProviderKey != null)
            {
                var Claims = ctx.User.Claims.Where(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                                            .OrderBy(claim => claim.Value)
                                            .Select(claim => claim.Value)
                                            .ToArray();

                var required = ctx.Items.DownstreamRoute().RouteClaimsRequirement;

                var Roles = required["Role"].Split(",")
                                            .Select(r => r.Trim())
                                            .OrderBy(r => r)
                                            .ToArray();

                var interception = (from claim in Claims.Intersect(Roles)
                                    select claim).ToList();

                return interception.Count != 0;
            }
            return true;            
        }
    }
}