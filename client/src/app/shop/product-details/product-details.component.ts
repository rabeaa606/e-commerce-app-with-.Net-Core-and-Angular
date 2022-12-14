import { BreadcrumbService } from 'xng-breadcrumb';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from './../shop.service';
import { BasketService } from './../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.shopService
      .getProduct(+this.activateRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (res) => {
          this.product = res;
          this.bcService.set('@productDetails', this.product.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }
  addItemToBaket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
  decrementItemQuantity() {
    if (this.quantity > 1) this.quantity--;
  }
  incrementItemQuantity() {
    this.quantity++;
  }
}
