using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.CostPrice).IsRequired();
            builder.Property(x => x.ShortDescription).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ProductColorId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.TechnicalSpecs).IsRequired();
            builder.Property(x => x.BrandId).IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}
