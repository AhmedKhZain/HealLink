using ErrorOr;
using HealLink.Application.Common.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Patients.Commands.DeleteMedicalHistrory;
[Authorize(Role = "Patient")]
public record DeleteMedicalHistoryCommand(
    Guid MedicalHistoryId):IRequest<ErrorOr<Success>>;
