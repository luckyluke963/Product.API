
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Core.Entities;

namespace Product.Infrastructure.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Products>
    {
      

        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Description).HasMaxLength(128);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            
        }

       
    }
}
