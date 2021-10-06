using DiscountStore.Core.Models;
using System.Collections.Generic;

namespace DiscountStore.Core
{
    public static class Mocks
    {
        private static readonly Dictionary<string, Discount> discountDALMockedData = new()
        {
            {
                "Big mug",
                new Discount
                {
                    ItemKey = "Big mug",
                    DiscountedPrice = 1.5,
                    ItemCountForDiscount = 2
                }
            },
            {
                "Napkins pack",
                new Discount
                {
                    ItemKey = "Napkins pack",
                    DiscountedPrice = 0.9,
                    ItemCountForDiscount = 3
                }
            }
        };

        public static Dictionary<string, Discount> DiscountDALMockedData { get => discountDALMockedData; }
    }
}
