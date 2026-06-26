using Ardalis.SmartEnum;

namespace HealLink.Domain.MedicalHistories
{
    public class MedicalHistoryType : SmartEnum<MedicalHistoryType>
    {
        public static readonly MedicalHistoryType Medication = new("Medication", 1);
        public static readonly MedicalHistoryType ChronicDisease = new("ChronicDisease", 2);
        public static readonly MedicalHistoryType Allergy = new("Allergy", 3);
        public static readonly MedicalHistoryType MedicalExaminations = new("MedicalExaminations", 4);

        private MedicalHistoryType(string name, int value) : base(name, value) { }
    }

}
