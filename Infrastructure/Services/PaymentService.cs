using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastructure.Services;

public class PaymentService(IConfiguration config, ICartService cartService,
    IGenericRepository<Product> productRepo,
    IGenericRepository<DeliveryMethod> dmRepo) : IPaymentService
{
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
        
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];

        // Get the user cart
        var cart = await cartService.GetCartAsync(cartId);
        
        if (cart == null) return null;
        
        var shippingPrice = 0m; // Decimal

        // Validate the delivery information
        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await dmRepo.GetByIdAsync((int)cart.DeliveryMethodId);
            
            if (deliveryMethod == null) return null;
            
            shippingPrice = deliveryMethod.Price;
        }

        // Validate the items prices (we do not trust the price inside the cart because it comes from the client)
        foreach (var item in cart.Items)
        {
            var productItem = await productRepo.GetByIdAsync(item.ProductId);

            if (productItem == null) return null;

            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        // Create a Payment Intent
        var service = new PaymentIntentService();
        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = ["card"]

            };
            
            intent = await service.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        // Or Update the payment intent
        else
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100
            };
            intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            
        }

        // We set the updated cart and return it
        await cartService.SetCartAsync(cart);

        return cart;
    }
}