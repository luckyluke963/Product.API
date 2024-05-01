
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

            builder.HasData(new Products { Id = 1, Name = "Product 1", Description = "Description 1", Price=100, CategoryId=1 },
                           new Products { Id = 2, Name = "Product 2", Description = "Description 2", Price=200, CategoryId=2 },
                           new Products { Id = 3, Name = "Product 3", Description = "Description 3", Price=300, CategoryId=3 });
        }

       
    }
}
