using Ardalis.SmartEnum;

namespace HealLink.Domain.Requests
{
    public class DoctorSubscriptionPlan:SmartEnum<DoctorSubscriptionPlan>
    {

        public static readonly DoctorSubscriptionPlan Daily = new("Daily", 1);
        public static readonly DoctorSubscriptionPlan Weekly = new("Weekly", 7);
        public static readonly DoctorSubscriptionPlan Monthly = new("Monthly", 30);
        private DoctorSubscriptionPlan(string name, int value) : base(name, value) { }
        public override string ToString() => Name;





    }
}