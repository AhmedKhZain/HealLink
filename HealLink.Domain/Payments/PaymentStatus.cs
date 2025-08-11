using Ardalis.SmartEnum;

namespace HealLink.Domain.Payments
{

    public class PaymentStatus : SmartEnum<PaymentStatus> 
    {
        public static readonly PaymentStatus Authorized = new PaymentStatus("Authorized",1) ;
        public static readonly PaymentStatus Captured = new PaymentStatus("Captured", 2) ;
        public static readonly PaymentStatus Cancelled = new PaymentStatus("Cancelled", 3) ;
        public static readonly PaymentStatus Failed = new PaymentStatus("Failed", 4) ;
        public static readonly PaymentStatus FaildToCancel = new PaymentStatus("FaildToCancel", 5);
        public static readonly PaymentStatus Refunded = new PaymentStatus("Refunded", 6);

        private PaymentStatus(string name, int value) : base(name, value)
        {
        }
    }

            


}