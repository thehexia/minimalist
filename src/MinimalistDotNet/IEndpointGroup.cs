using Microsoft.AspNetCore.Routing;

namespace MinimalistDotNet;

public interface IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app);
}
