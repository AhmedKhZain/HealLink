using HealLink.Domain.Requests;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IDoctorRequestRepository
    {

        Task AddDoctorRequestAsync(DoctorRequest doctorRequest);
        Task<bool> ExistsByIdAsync(Guid requestId);
        Task<IEnumerable<DoctorRequest>?> GetDoctorRequestsByPatientIdAsync(Guid patientId,bool Tracking =false);
        Task<IEnumerable<DoctorRequest>?> GetDoctorRequestsByDoctorIdAsync(Guid doctorId,bool Tracking =false);

        Task<IEnumerable<DoctorRequest>?> GetRequestsHistoryByDoctorIdAsync(Guid doctorId, int PageNum = 1, int PageSize = 12, bool Tracking = false);
        Task<IEnumerable<DoctorRequest>?> GetRequestsHistoryByPatientIdAsync(Guid patientId, int PageNum = 1, int PageSize = 12, bool Tracking = false);
        Task<DoctorRequest?> GetById(Guid requestId);



        void UpdateDoctorRequest(DoctorRequest doctorRequest);
        void DeleteDoctorRequest(DoctorRequest doctorRequest);
    }



}