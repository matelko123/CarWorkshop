namespace API.Endpoints.Internal;

internal interface IEndpoints
{
    internal static abstract void DefineEndpoints(IEndpointRouteBuilder app);
}