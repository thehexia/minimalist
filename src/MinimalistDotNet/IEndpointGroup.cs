using Microsoft.AspNetCore.Routing;

namespace MinimalistDotNet;

public delegate void Mapper(IEndpointRouteBuilder app);

public interface IEndpointGroup
{
    Mapper Map { get; }
}
