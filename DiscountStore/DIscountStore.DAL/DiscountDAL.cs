using DiscountStore.Core.DAL;
using DiscountStore.Core.Models;
using System.Collections.Generic;
using DiscountStore.Core;

namespace DIscountStore.DAL
{
    public class DiscountDAL : IDiscountDAL
    {
        public Dictionary<string, Discount> GetCurrentDiscounts()
        {
            return Mocks.DiscountDALMockedData;
        }
    }
}
