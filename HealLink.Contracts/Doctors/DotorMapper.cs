using healLink.Application.Doctors.Commands.AddDoctorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Doctors
{
    public static class DotorMapper
    {
        public static UpdateDoctorDataCommand ToCommand(this DoctorDataRequest request)
        => new UpdateDoctorDataCommand(request.DoctorId, request.syndicateIdImageLink, request.nationalId, request.licenseNumber);


    }

}
