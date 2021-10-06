using DiscountStore.Core.Models;
using System.Collections.Generic;

namespace DiscountStore.Core.DAL
{
    public interface IDiscountDAL
    {
        Dictionary<string, Discount> GetCurrentDiscounts();
    }
}
