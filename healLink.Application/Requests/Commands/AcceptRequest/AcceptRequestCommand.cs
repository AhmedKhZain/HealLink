using ErrorOr;
using HealLink.Application.Common.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Requests.Commands.AcceptRequest;

[Authorize(Role = "Doctor")]
public record AcceptRequestCommand
    (Guid RequestId)
    :IRequest<ErrorOr<Success>>;
