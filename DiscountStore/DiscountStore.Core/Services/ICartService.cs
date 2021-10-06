using DiscountStore.Core.Models;

namespace DiscountStore.Core.Services
{
    public interface ICartService
    {
        void Add(Item item);
        void Remove(Item item);
        double GetTotal();
    }
}
