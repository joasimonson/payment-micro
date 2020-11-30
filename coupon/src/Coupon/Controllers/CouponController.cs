using Coupon.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coupon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ILogger<CouponController> _logger;

        public CouponController(ILogger<CouponController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{coupon}")]
        public ActionResult<CouponResult> Get(string coupon)
        {
            if (string.IsNullOrWhiteSpace(coupon))
            {
                return BadRequest();
            }

            CouponResult result = coupon switch
            {
                "THMPV" => new CouponResult()
                {
                    Status = CouponResult.CoupomStatus.VALID
                },
                _ => new CouponResult()
                {
                    Status = CouponResult.CoupomStatus.INVALID
                },
            };
            return Ok(result);
        }
    }
}
