using ErrorOr;
using HealLink.Application.Common.Authorization;
using HealLink.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Requests.Queries.GetRequestsByDoctorId;
[Authorize(Role = "Doctor")]

public record GetRequestsByDoctorIdQuery(Guid DoctorId) : IRequest<ErrorOr<IEnumerable<DoctorRequest>>>;
