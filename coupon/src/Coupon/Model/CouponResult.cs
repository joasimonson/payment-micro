using Coupon.Extensions;
using System.ComponentModel;

namespace Coupon.Model
{
    public class CouponResult
    {
        public CoupomStatus Status { get; set; }
        public string Description
        {
            get
            {
                return Status.GetAttribute<DescriptionAttribute>().Description;
            }
        }

        public enum CoupomStatus
        {
            [Description("Invalid coupon")]
            INVALID = 0,
            [Description("Valid coupon")]
            VALID = 1
        }
    }
}
