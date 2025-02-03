import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, of } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private cartService = inject(CartService);
  private accountService = inject(AccountService);

  init() {
    const cartId = localStorage.getItem('cart_id');
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    // ForkJoin is an rxjs fonction used to combine the result of multiple http requests and only emits
    // the results only when they are completed
    return forkJoin({
      cart: cart$,
      user: this.accountService.getUserInfo(),
    });
  }
}
