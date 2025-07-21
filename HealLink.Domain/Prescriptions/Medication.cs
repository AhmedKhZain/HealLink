namespace HealLink.Domain.Prescriptions
{
    public class Medication
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public byte? TimesPerDay { get; private set; } = null!;
        public Guid PrescriptionId { get; private set; }
        public Prescription? Prescription { get; private set; } = null!;


        private Medication() { }
        public Medication(Guid prescriptionId, string name, byte timesPerDay)
        {
            Id = Guid.NewGuid();
            PrescriptionId = prescriptionId;
            Name = name;
            TimesPerDay = timesPerDay;
        }
    }

}