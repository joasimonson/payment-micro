import "https://deno.land/x/dotenv/load.ts";

import { CouponResult } from "../types/coupon-status.ts";

const url_api = Deno.env.get("URL_API_COUPON") + "coupon/";

export async function verifyCoupon(coupon: string): Promise<CouponResult> {
  const url = url_api + coupon;

  const ret = await fetch(url);
  const result = await ret.json() as CouponResult;

  return result;
}
