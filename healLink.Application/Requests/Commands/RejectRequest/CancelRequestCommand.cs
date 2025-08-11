using ErrorOr;
using HealLink.Application.Common.Authorization;
using MediatR;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Requests.Commands.RejectRequest;
[Authorize(Role = "Doctor")]
public record RejectRequestCommand(Guid requestId):IRequest<ErrorOr<Success>>;
