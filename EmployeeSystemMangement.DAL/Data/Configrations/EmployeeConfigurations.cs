using EmployeeSystemMangement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.DAL.Data.Configrations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
                        entity
                       .Property(e => e.Name)
                       .HasColumnType("varchar")
                       .HasMaxLength(50)
                       .IsRequired();
            entity.Property(e => e.Salary).HasColumnName("decimal(12,2)");
            entity.Property(e => e.Gender)
                .HasConversion(
                (Gender) => Gender.ToString(),
                (GenderAsString)=>(Gender) Enum.Parse(typeof(Gender), GenderAsString,true)

                );
            entity.Property(e => e.EmployeeType)
              .HasConversion(
              (EmployeeType) => EmployeeType.ToString(),
              (EmployeeTypeString) => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmployeeTypeString, true)

              );

        }
    }
}
