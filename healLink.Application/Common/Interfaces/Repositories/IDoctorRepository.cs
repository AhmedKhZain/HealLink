using HealLink.Domain.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        Task<bool> ExistsAsync(Guid doctorId);
        Task<Doctor?> GetDoctorByIdAsync(Guid doctorId, bool tracking = true);
        void UpdateDoctor(Doctor doctor);
        Task DeleteDoctorAsync(Guid doctorId);
        Task AddDoctorAsync(Doctor doctor);
    }
}
