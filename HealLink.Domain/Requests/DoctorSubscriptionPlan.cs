using Ardalis.SmartEnum;

namespace HealLink.Domain.Requests
{
    public class DoctorSubscriptionPlan:SmartEnum<DoctorSubscriptionPlan>
    {
        public decimal Price { get; }

        public static readonly DoctorSubscriptionPlan Daily = new("Daily", 1, 10m);
        public static readonly DoctorSubscriptionPlan Weekly = new("Weekly", 7, 50m);
        public static readonly DoctorSubscriptionPlan Monthly = new("Monthly", 30, 150m);

        private DoctorSubscriptionPlan(string name, int value, decimal price) : base(name, value)
        {
            Price = price;
        }
        public override string ToString() => Name;


    }
}