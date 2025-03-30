namespace Domain.Dto
{
    public class AddCouponDto
    {
        public required int OrderId { get; set; }

        public required string CouponCode { get; set; }
    }
}
