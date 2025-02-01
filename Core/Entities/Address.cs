namespace Core.Entities;

public class Address : BaseEntity
{
    // FYI : We are using the same format as Stripe as their API is using for consistency purpose
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
}