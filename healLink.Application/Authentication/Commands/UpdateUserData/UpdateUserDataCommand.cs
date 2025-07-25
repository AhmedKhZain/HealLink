using ErrorOr;
using HealLink.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Authentication.Commands.UpdateUserData;

public record UpdateUserDataCommand
    (Guid Id,
    string? ShowName,
    string? FullName,
    string? PhotoPath,
    string? Email): IRequest<ErrorOr<User>>;
