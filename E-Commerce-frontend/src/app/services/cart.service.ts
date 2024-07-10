import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private baseUrl = 'http://localhost:5182/api/cartItem';

  constructor(private http: HttpClient) { }

  addToCart(cartItem: { productID: number, quantity: number }): Observable<void> {
    return this.http.post<void>(this.baseUrl, cartItem);
  }
}
