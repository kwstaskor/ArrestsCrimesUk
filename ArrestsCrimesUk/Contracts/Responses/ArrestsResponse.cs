namespace ArrestsCrimesUk.Contracts.Responses
{
    public class ArrestsResponse
    {
        public string Measure { get; set; } = null!;
        public string Time { get; set; } = null!;
        public string TimeType { get; set; } = null!;
        public string Ethnicity { get; set; } = null!;
        public string EthnicityType { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string GenderType { get; set; } = null!;
        public string AgeGroup { get; set; } = null!;
        public string AgeGroupType { get; set; } = null!;
        public string Geography { get; set; } = null!;
        public string GeographyType { get; set; } = null!;
        public int? NumberOfArrests { get; set; }
        public string PopulationByEthnicityGenderAndPfaBasedOn2011Census { get; set; } = null!;
        public string RatePer1000PopulationByEthnicityGenderAndPfa { get; set; } = null!;
        public string ProportionOfArrestsOfThisEthnicityInThisYearOfThisGenderAndInThisPoliceForceAreaExcludesUnreported { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
