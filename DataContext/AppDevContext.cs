using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public partial class AppDevContext : DbContext
    {
        public AppDevContext()
        {
            //Database.SetCommandTimeout(150000);
        }

        public AppDevContext(DbContextOptions<AppDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arrest> Arrests { get; set; } = null!;
        public virtual DbSet<Victim> Victims { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arrest>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AgeGroup)
                    .HasMaxLength(50)
                    .HasColumnName("Age_Group");

                entity.Property(e => e.AgeGroupType)
                    .HasMaxLength(50)
                    .HasColumnName("Age_Group_type");

                entity.Property(e => e.Ethnicity).HasMaxLength(50);

                entity.Property(e => e.EthnicityType)
                    .HasMaxLength(50)
                    .HasColumnName("Ethnicity_type");

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.GenderType)
                    .HasMaxLength(50)
                    .HasColumnName("Gender_type");

                entity.Property(e => e.Geography).HasMaxLength(50);

                entity.Property(e => e.GeographyType)
                    .HasMaxLength(50)
                    .HasColumnName("Geography_type");

                entity.Property(e => e.Measure).HasMaxLength(50);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.NumberOfArrests).HasColumnName("Number_of_arrests");

                entity.Property(e => e.PopulationByEthnicityGenderAndPfaBasedOn2011Census)
                    .HasMaxLength(50)
                    .HasColumnName("Population_by_ethnicity_gender_and_PFA_based_on_2011_Census");

                entity.Property(e => e.ProportionOfArrestsOfThisEthnicityInThisYearOfThisGenderAndInThisPoliceForceAreaExcludesUnreported)
                    .HasMaxLength(50)
                    .HasColumnName("Proportion_of_arrests_of_this_ethnicity_in_this_year_of_this_gender_and_in_this_police_force_area_excludes_unreported");

                entity.Property(e => e.RatePer1000PopulationByEthnicityGenderAndPfa)
                    .HasMaxLength(50)
                    .HasColumnName("Rate_per_1_000_population_by_ethnicity_gender_and_PFA");

                entity.Property(e => e.Time).HasMaxLength(50);

                entity.Property(e => e.TimeType)
                    .HasMaxLength(50)
                    .HasColumnName("Time_type");
            });

            modelBuilder.Entity<Victim>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Age).HasMaxLength(50);

                entity.Property(e => e.Ethnicity).HasMaxLength(50);

                entity.Property(e => e.EthnicityType)
                    .HasMaxLength(50)
                    .HasColumnName("Ethnicity_type");

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Geography).HasMaxLength(50);

                entity.Property(e => e.GeographyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Geography_code");

                entity.Property(e => e.GeographyType)
                    .HasMaxLength(50)
                    .HasColumnName("Geography_type");

                entity.Property(e => e.HouseholdIncome)
                    .HasMaxLength(50)
                    .HasColumnName("Household_income");

                entity.Property(e => e.LowerCi).HasColumnName("Lower_CI");

                entity.Property(e => e.Measure).HasMaxLength(100);

                entity.Property(e => e.SampleSize).HasColumnName("Sample_size");

                entity.Property(e => e.SocioEconomicClassification)
                    .HasMaxLength(50)
                    .HasColumnName("Socio_economic_classification");

                entity.Property(e => e.StandardError).HasColumnName("Standard_Error");

                entity.Property(e => e.Time).HasMaxLength(50);

                entity.Property(e => e.TimeType)
                    .HasMaxLength(50)
                    .HasColumnName("Time_type");

                entity.Property(e => e.UpperCi).HasColumnName("Upper_CI");

                entity.Property(e => e.ValueType)
                    .HasMaxLength(50)
                    .HasColumnName("Value_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
