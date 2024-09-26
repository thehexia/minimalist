using Microsoft.AspNetCore.Routing;

namespace MinimalistDotNet;

public interface IEndpointGroup
{
    void Map(IEndpointRouteBuilder app);
}

