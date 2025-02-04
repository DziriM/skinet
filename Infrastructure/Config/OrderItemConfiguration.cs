using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // The Order own the ItemOrdered - It has One of it
        builder.OwnsOne(x => x.ItemOrdered, o => o.WithOwner());
        
        // Configuration of the decimal
        builder.Property(x => x.Price).HasColumnType("decimal(18, 2)");
    }
}