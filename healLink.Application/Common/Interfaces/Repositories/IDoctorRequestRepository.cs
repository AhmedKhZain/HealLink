using HealLink.Domain.Requests;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IDoctorRequestRepository
    {

        Task AddDoctorRequestAsync(DoctorRequest doctorRequest);

        Task<IEnumerable<DoctorRequest?>> GetDoctorRequestsByPatientIdAsync(Guid patientId,bool Tracking =false);
        Task<IEnumerable<DoctorRequest?>> GetDoctorRequestsByDoctorIdAsync(Guid doctorId,bool Tracking =false);

        Task<IEnumerable<DoctorRequest?>> GetRequestsHistoryByDoctorIdAsync(Guid doctorId, bool Tracking = false);
        Task<IEnumerable<DoctorRequest?>> GetRequestsHistoryByPatientIdAsync(Guid patientId, bool Tracking = false);



        void UpdateDoctorRequest(DoctorRequest doctorRequest);
        void DeleteDoctorRequest(DoctorRequest doctorRequest);
    }



}