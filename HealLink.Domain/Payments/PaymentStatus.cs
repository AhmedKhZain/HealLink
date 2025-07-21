using Ardalis.SmartEnum;

namespace HealLink.Domain.Payments
{
    public partial class Payment
    {
        public class PaymentStatus : SmartEnum<PaymentStatus> 
        {
            public static readonly PaymentStatus Authorized = new PaymentStatus("Authorized",1) ;
            public static readonly PaymentStatus Captured = new PaymentStatus("Captured", 1) ;
            public static readonly PaymentStatus Cancelled = new PaymentStatus("Cancelled", 1) ;
            public static readonly PaymentStatus Failed = new PaymentStatus("Failed", 1) ;

            private PaymentStatus(string name, int value) : base(name, value)
            {
            }
        }

            


    }
}