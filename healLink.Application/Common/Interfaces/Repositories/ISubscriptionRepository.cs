using HealLink.Domain.PatientDoctorSubscriptions;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(PatientDoctorSubscription subscription);
        Task<bool> ExistsActiveByPatientIdAndDoctorIdAsync(Guid patientId, Guid doctorId);

        Task<PatientDoctorSubscription?> GetSubscriptionsByPatientIdAndDoctorIdAsync(Guid patientId, Guid doctorId, bool Tracking = false);
        Task<IEnumerable<PatientDoctorSubscription>?> GetSubscriptionsByDoctorIdAsync(Guid doctorId, bool Tracking = false);
        Task<IEnumerable<PatientDoctorSubscription>?> GetSubscriptionsByPatientIdAsync(Guid patientId, bool Tracking = false);
        Task<IEnumerable<PatientDoctorSubscription>?> GetDoctorHistoryByIdAsync(Guid doctorId,bool Tracking=false);
        void UpdateSubscription (PatientDoctorSubscription subscription);
        void DeleteSubscription(PatientDoctorSubscription subscription);
        Task<PatientDoctorSubscription?> GetById(Guid subscriptionId, bool Tracking = false);
    }



}