using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Name).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.IsManager).IsRequired();
#warning add decomal representation
            builder.Property(x => x.Salary).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(30).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();

            builder.OwnsOne(x => x.PhoneNumber, x =>
            {
                x.Property(pp => pp.Phone)
                    .IsRequired()
                    .HasColumnName("Phone")
                    .HasMaxLength(20);
                x.Property(pp => pp.MobilePhone)
                    .IsRequired()
                    .HasColumnName("MobilePhone")
                    .HasMaxLength(40);
            });
        }
    }
}
