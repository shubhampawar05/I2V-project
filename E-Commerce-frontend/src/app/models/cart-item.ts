// src/app/models/cart-item.ts
export interface CartItem {
  cartItemID: number;
  productID: number;
  quantity: number;
  product?: {
    productName: string;
  };
}
