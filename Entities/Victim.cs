namespace Entities
{
    public class Victim
    {
        public long Id { get; set; }
        public string Measure { get; set; } = null!;
        public string Ethnicity { get; set; } = null!;
        public string EthnicityType { get; set; } = null!;
        public string Time { get; set; } = null!;
        public string TimeType { get; set; } = null!;
        public string Geography { get; set; } = null!;
        public string GeographyType { get; set; } = null!;
        public string GeographyCode { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Age { get; set; } = null!;
        public string SocioEconomicClassification { get; set; } = null!;
        public string HouseholdIncome { get; set; } = null!;
        public double? Value { get; set; }
        public string ValueType { get; set; } = null!;
        public int? SampleSize { get; set; }
        public double? StandardError { get; set; }
        public double? LowerCi { get; set; }
        public double? UpperCi { get; set; }
    }
}
