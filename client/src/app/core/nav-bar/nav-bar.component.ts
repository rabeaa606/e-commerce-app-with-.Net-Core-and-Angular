import { Observable } from 'rxjs';
import { Basket, IBasket } from './../../shared/models/basket';
import { Component, OnInit } from '@angular/core';

import { BasketService } from './../../basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketCount: number;

  constructor(public basketService: BasketService) {
    this.basket$ = this.basketService.basket$;

    this.basket$.subscribe((res) => {
      if (res) this.basketCount = res.items.length;
    });
  }

  ngOnInit(): void {}
}
