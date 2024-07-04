namespace NanoNexus.ViewModels
{
    public class BasketItemVm
    {

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int? DiscountPercent { get; set; }
        public int Count { get; set; }
    }
}
