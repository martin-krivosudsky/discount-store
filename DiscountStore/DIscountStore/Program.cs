using DiscountStore.Core.DAL;
using DiscountStore.Core.Services;
using DiscountStore.Services;
using DIscountStore.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace DIscountStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDiscountDAL, DiscountDAL>()
                .AddSingleton<ICartService, CartService>()
                .BuildServiceProvider();

            var service = serviceProvider.GetService<ICartService>();
        }
    }
}
