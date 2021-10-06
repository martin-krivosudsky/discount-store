namespace DiscountStore.Core.Models
{
    public class Discount
    {
        public string ItemKey { get; set; }
        public int ItemCountForDiscount { get; set; }
        public double DiscountedPrice { get; set; }
    }
}
