using Ardalis.SmartEnum;

namespace HealLink.Domain.Requests
{
    public class RequestType : SmartEnum<RequestType>
    {
        public static readonly RequestType Initial = new("Initial", 1);
        public static readonly RequestType Renewal = new("Renewal", 2);

        private RequestType(string name, int value) : base(name, value) { }
    }


}
