export type CouponResult = {
  status: CouponStatus;
  description: string;
};

export enum CouponStatus {
  INVALID = 0,
  VALID = 1,
}
