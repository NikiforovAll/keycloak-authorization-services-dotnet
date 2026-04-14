namespace Api.Application.Authorization;

using MediatR;

public interface IRequestWithResourceId : IRequest
{
    public string ResourceId { get; }
}
