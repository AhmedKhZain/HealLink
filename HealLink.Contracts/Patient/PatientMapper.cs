using healLink.Application.Patients.Commands.AddGuardian;
using healLink.Application.Patients.Commands.AddMedicalHistory;
using healLink.Application.Patients.Queries.GetAllPatientGuardian;
using healLink.Application.Patients.Queries.GetMedicalHistory;
using healLink.Application.Requests.Commands.MakeNewSubscriptionRequest;
using healLink.Application.Requests.Queries.GetRequestsByPatientId;


namespace HealLink.Contracts.Patient
{
    public static class PatientMapper
    {
        public static AddMedicalHistoryCommand  ToCommand(this AddMedicalHistoryRequest request)
        {
            return new AddMedicalHistoryCommand
            (request.PatientId,
            request.FileLink,
            request.Description,
            request.Type);
        }

        public static GetMedicalHistoryQuery ToQuery(this GetMedicalHistoryRequest request)
        {
            return new GetMedicalHistoryQuery(request.UserId,
                request.Type,
                request.PageNumber,
                request.PageSize);

        }
        public static AddGuardianCommand ToCommand(this AddGuardianRequest request)
        {
            return new AddGuardianCommand
            (request.PatientId,
            request.GuardinId, 
            request.relationship);
        }
        public static GetAllPatientGuardianQuery ToQuery(this GetPatientGuardianRequest request)
        {
            return new GetAllPatientGuardianQuery(request.userId,
                request.pageNum,
                request.pageSize,
                request.newestFirst);
        }
        public static MakeSubscriptionRequestCommand ToCommand(this MakeRequestRequest request)
        {
            return new MakeSubscriptionRequestCommand(
                request.PatientId,
                request.DoctorId,
                request.plan,
                request.PaymentId,
                request.RequestTypename,
                request.subscriptionId,
                request.FileLink
            );
        }

        public static GetPatientRequestsQuery ToQuery(this GetPatientRequest request)
        {
            return new GetPatientRequestsQuery(request.PatientId, request.newestFirst);
        }

    }
} 
