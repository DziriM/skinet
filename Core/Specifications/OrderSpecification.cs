using Core.Entities.OrderAggregate;

namespace Core.Specifications;

public class OrderSpecification : BaseSpecification<Order>
{
    // To get the list of orders based on an email
    public OrderSpecification(string email) : base(x => x.BuyerEmail == email)
    {
        AddInclude(x => x.OrderItems);
        AddInclude(x => x.DeliveryMethod);
        AddOrderByDescending(x => x.OrderDate);
    }

    // To get the order based on its id and the user email
    // This assures us that an individual user
    // can only request an individual order that matches their email
    public OrderSpecification(string email, int id) : base(x=> x.BuyerEmail == email && x.Id == id)
    {
        // Just to demonstrate that we can do it with string
        // The downside of this way of doing is that we don't have the type safety of the method above
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");
    }

    public OrderSpecification(int id) : base(x=> x.Id == id)
    {
        AddInclude(x => x.OrderItems);
        AddInclude(x => x.DeliveryMethod);
    }

    public OrderSpecification(string paymentIntentId, bool isPaymentIntent) : base(x => x.PaymentIntentId == paymentIntentId)
    {
        AddInclude(x => x.OrderItems);
        AddInclude(x => x.DeliveryMethod);
    }

    public OrderSpecification(OrderSpecParams specParams) : base(x =>
         string.IsNullOrEmpty(specParams.Status) || x.Status == ParseStatus(specParams.Status))
    {
        AddInclude(x => x.OrderItems);
        AddInclude(x => x.DeliveryMethod);
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        AddOrderByDescending(x => x.OrderDate);
    }

    private static OrderStatus? ParseStatus(string status)
    {
        if (Enum.TryParse<OrderStatus>(status, true, out var result))
        {
            return result;
        }

        return null;
    }
}