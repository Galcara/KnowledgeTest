using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Mappings
{
    class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("COMPANIES");

            builder.Property(e => e.CommercialName).IsRequired().HasMaxLength(50).IsUnicode(false);

            builder.HasIndex(e => e.CPNJ, "UQ_COMPANIES_CNPJ").IsUnique();
            builder.Property(e => e.CPNJ).IsRequired().HasMaxLength(14).IsUnicode(false).IsFixedLength(true);

            builder.Property(e => e.State).IsRequired();

            builder.Property(e => e.Active).IsRequired();

            builder.HasMany(e => e.Supplier).WithMany(e => e.Companies);

        }
    }
}
