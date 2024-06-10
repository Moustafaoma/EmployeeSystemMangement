using EmployeeSystemMangement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.DAL.Data.Configrations
{
    public class DepartmentCongurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent API of Department
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(10, 10);

            builder.Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(d => d.Code)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
