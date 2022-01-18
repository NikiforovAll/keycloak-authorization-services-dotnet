namespace Api.Application.Authorization;

using MediatR;

public interface IRequestWithResourceId : IRequest
{
    string ResourceId { get; }
}
