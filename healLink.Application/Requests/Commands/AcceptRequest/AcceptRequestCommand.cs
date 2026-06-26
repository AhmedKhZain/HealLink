using ErrorOr;
using HealLink.Application.Common.Authorization;
using MediatR;

namespace healLink.Application.Requests.Commands.AcceptRequest;

[Authorize(Role = "Doctor")]
public record AcceptRequestCommand
    (Guid RequestId)
    :IRequest<ErrorOr<Success>>;
