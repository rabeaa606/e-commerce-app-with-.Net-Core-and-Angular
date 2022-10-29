import { BasketService } from './basket.service';
import { IBasket, IBasketItem, IBasketTotals } from './../shared/models/basket';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketTotals$: Observable<IBasketTotals>;

  constructor(private basketService: BasketService) {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
  }

  ngOnInit(): void {}

  incrementItemQuantity(item: IBasketItem) {
    this.basketService.incrementItemQouantity(item);
  }
  decrementItemQuantity(item: IBasketItem) {
    this.basketService.decrementItemQouantity(item);
  }
  removeBasketItem(item: IBasketItem) {
    this.basketService.removeItemFromBasket(item);
  }
}
