using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // The Order own the ShippingAddress - It has One of it
        builder.OwnsOne(x => x.ShippingAddress, o => o.WithOwner());
        
        // The Order own the PaymentSummary - It has One of it
        builder.OwnsOne(x => x.PaymentSummary, o => o.WithOwner());
        
        // To get the enum value from the OrderStatus
        builder.Property(x => x.Status).HasConversion(
            o => o.ToString(),
            o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
        );
        
        // Configuration of the decimal
        builder.Property(x => x.SubTotal).HasColumnType("decimal(18, 2)");
        
        // One Order can have MANY OrderItems - If we delete the Order, it will delete the OrderItems in that Order
        builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        
        // DateTime UTC Configuration because it get weird in the browser, because of SQL Server behaviour
        builder.Property(x => x.OrderDate).HasConversion(
            d => d.ToUniversalTime(),
            d => DateTime.SpecifyKind(d, DateTimeKind.Utc)
        );
    }
}