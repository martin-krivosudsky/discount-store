using DiscountStore.Core.DAL;
using DiscountStore.Core.Models;
using DiscountStore.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace DiscountStore.Services
{
    public class CartService : ICartService
    {
        private readonly List<Item> _cart;
        private readonly IDiscountDAL _discountDAL;

        public CartService(IDiscountDAL discountDAL)
        {
            _cart = new List<Item>();
            _discountDAL = discountDAL ?? throw new System.ArgumentNullException(nameof(discountDAL));
        }

        public void Add(Item item)
        {
            _cart.Add(item);
        }

        public double GetTotal()
        {
            double price = 0;
            var discounts = _discountDAL.GetCurrentDiscounts();

            var groupedCart = _cart.GroupBy(c => c.ItemKey);
            foreach (var group in groupedCart)
            {
                price += GetPriceForGroup(discounts, group);
            }

            return price;
        }

        private static double GetPriceForGroup(Dictionary<string, Discount> discounts, IGrouping<string, Item> group)
        {
            int discountedPacks = 0;
            double notDiscountedPrice = group.First().Price;
            double discountedPrice = 0;
            int notDiscountedItems;

            if (discounts.ContainsKey(group.Key))
            {
                var discount = discounts[group.Key];
                discountedPrice = discount.DiscountedPrice;
                discountedPacks = group.Count() / discount.ItemCountForDiscount;
                notDiscountedItems = group.Count() % discount.ItemCountForDiscount;
            }
            else
            {
                notDiscountedItems = group.Count();
            }

            return (notDiscountedItems * notDiscountedPrice) 
                + (discountedPacks * discountedPrice);
        }

        public void Remove(Item item)
        {
            _cart.Remove(item);
        }
    }
}
