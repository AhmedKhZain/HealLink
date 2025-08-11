using ErrorOr;
using HealLink.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Admins.Commands.ChangeTheRole;

public record ChangeRoleToDoctorCommand(Guid UserId):IRequest<ErrorOr<User>>;

