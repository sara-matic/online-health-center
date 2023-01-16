namespace Discount.Common.DTOs
{
    public abstract class CouponBaseDTO
    {
        public string PatientId { get; set; }
        public string Specialty { get; set; }
        public int AmountInPercentage { get; set; }
    }
}
