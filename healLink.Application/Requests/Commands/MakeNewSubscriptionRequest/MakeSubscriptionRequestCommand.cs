using ErrorOr;
using HealLink.Application.Common.Authorization;
using MediatR;

namespace healLink.Application.Requests.Commands.MakeNewSubscriptionRequest;
[Authorize(Role = "Patient")] //Patient pP Patient pP
public record MakeSubscriptionRequestCommand
    (Guid PatientId, 
    Guid DoctorId, 
    string plan,
    string PaymentId,
    string? RequestTypename,
    Guid? subscriptionId,
    string? FileLink)
    :IRequest<ErrorOr<Guid>>;


