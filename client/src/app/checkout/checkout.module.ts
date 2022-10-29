import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { SharedModule } from '../shared/shared.module';
import { CheckoutAddressComponent } from './checkout-address/checkout-address.component';
import { CheckoutPaymentComponent } from './checkout-payment/checkout-payment.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import { CheckoutDeliveryComponent } from './ckeckout-delivery/checkout-delivery.component';

@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutAddressComponent,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent,
    CheckoutSuccessComponent,
    CheckoutPaymentComponent,
  ],
  imports: [CommonModule, CheckoutRoutingModule, SharedModule],
  exports: [CheckoutComponent],
})
export class CheckoutModule {}
