using System.ComponentModel.DataAnnotations;

namespace Checkout.Data
{
    public class Payment
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CardName { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string Expiration { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        public string Coupon { get; set; }
    }
}
