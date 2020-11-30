import { v4 } from "https://deno.land/std@0.77.0/uuid/mod.ts";

import { Payment } from "../types/payment.ts";
import { CouponStatus } from "../types/coupon-status.ts";
import { Order } from "../types/order.ts";
import { verifyCoupon } from "./CouponService.ts";

export async function createPayment(payment: Payment) {
  const order = createOrder(payment);

  try {
    const resultCoupon = await verifyCoupon(payment.Coupon);

    switch (resultCoupon.status) {
      case CouponStatus.INVALID:
        console.log("Order: ", order.id, ": invalid coupon!");
        break;

      case CouponStatus.VALID:
        console.log("Order: ", order.id, ": Processed");
        break;
    }
  } catch (error) {
    console.error("Order: ", order.id, ": could not process!");
  }
}

function createOrder(payment: Payment): Order {
  const order = {
    id: v4.generate(),
    cardNumber: payment.CardNumber,
    coupon: payment.Coupon,
  } as Order;

  return order;
}
