using ErrorOr;
using HealLink.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Authentication.Commands.CreateToken;
    public record CreateTokenCommand(string Email, string Type) : IRequest<ErrorOr<Success>>;


