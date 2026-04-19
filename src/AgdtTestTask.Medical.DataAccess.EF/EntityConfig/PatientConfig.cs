using AgdtTestTask.Core.DataAccess.EF.EntityConfig;
using AgdtTestTask.Medical.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgdtTestTask.Medical.DataAccess.EF.EntityConfig
{
    internal class PatientConfig
        : IdentifiableEntityConfig<Patient>
    {
        public override void Configure(
            EntityTypeBuilder<Patient> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Birthdate)
                .IsRequired();

            builder.Property(p => p.Active)
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder.HasIndex(x => x.Birthdate);

            builder.OwnsOne(p => p.Name, n =>
            {
                n.ToTable("PatientName");

                n.WithOwner()
                    .HasForeignKey("PatientId");

                n.HasIndex(n => n.Id)
                    .IsUnique();

                n.Property(n => n.Use)
                    .HasConversion<string>()
                    .IsRequired();

                n.Property(n => n.Family)
                    .HasMaxLength(200)
                    .IsRequired();

                n.OwnsMany(n => n.Given, g =>
                {
                    g.ToTable("PatientGivenName");

                    g.WithOwner()
                         .HasForeignKey("PatientId");

                    g.Property(g => g.Value)
                         .HasMaxLength(200)
                         .IsRequired();

                    g.HasKey("PatientId", "Value");
                });
            });
        }
    }
}
