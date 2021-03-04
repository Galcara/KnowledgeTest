using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Mappings
{
    class SupplierMap : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("SUPPLIERS");

            builder.HasIndex(e => e.CNPJ_CPF, "UQ_SUPPLIERS_CNPJ_CPF").IsUnique();
            builder.Property(e => e.CNPJ_CPF).IsRequired().HasMaxLength(14).IsUnicode(false).IsFixedLength(true);

            builder.HasIndex(e => e.RG, "UQ_SUPPLIERS_RG").IsUnique();
            builder.Property(e => e.RG).HasMaxLength(14).IsUnicode(false).IsFixedLength(true);

            builder.Property(e => e.PersonResponsible).IsRequired().HasMaxLength(50).IsUnicode(false);

            builder.Property(e => e.Telephone).IsRequired().HasMaxLength(12).IsUnicode();

            builder.Property(e => e.Telephone2).HasMaxLength(12).IsUnicode();

            builder.Property(e => e.DateRegistration).HasColumnType("DATETIME2");

            builder.Property(e => e.BirthDate).HasColumnType("DATETIME2");

            builder.Property(e => e.Active).IsRequired();

            builder.HasMany(e => e.Companies).WithMany(e => e.Supplier);
        }
    }
}
