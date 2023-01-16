namespace SIEG_API.DTO
{
    public class D_GradeSellDTO
    {
        public int BuyerId { get; set; } //訂單
        public int SellerId { get; set; } //訂單
        public string? State { get; set; } //訂單
        public int? Price { get; set; } //訂單
        public int BuyerGrade { get; set; } //會員
        public int SellerGrade { get; set; } //會員
        public int? Count { get; set; } //會員優惠券
    }
}
