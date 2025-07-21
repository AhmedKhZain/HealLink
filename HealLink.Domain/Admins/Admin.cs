using HealLink.Domain.Doctors;
using HealLink.Domain.Users;

namespace HealLink.Domain.Admins
{
    public class Admin
    {
        public Guid Id { get; private set; }
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public User User { get; private set; } = null!;

        public ICollection<Doctor> _ApprovedDoctors = new List<Doctor>();


        public Admin(Guid Userid)
        {
            Id = Userid;
        }

        private Admin() { }
    }
}